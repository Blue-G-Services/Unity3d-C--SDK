using System;
using System.Collections;
using System.Collections.Generic;
using DynamicPixels.GameService.Models;
using DynamicPixels.GameService.Services.MultiPlayer.Room;
using DynamicPixels.GameService.Services.User.Models;
using DynamicPixels.UnityBaseFiles;
using DynamicPixelsInitializer;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace DynamicPixels.Services.MultiPlayer.Realtime
{
    /// <summary>
    /// Manages real-time synchronization of dynamic wrappers and dynamic objects in a multiplayer environment. 
    /// Ensure that observers in a same scene have different observerIds
    /// </summary>
    public class RealtimeObserver : MonoBehaviour
    {
        /// <summary>
        /// Settings for controlling real-time synchronization.
        /// </summary>
        [SerializeField] private RealtimeSetting settings;

        /// <summary>
        /// Unique identifier for this observer.
        /// </summary>
        [SerializeField] private int observerId;

        /// <summary>
        /// Flag to skip processing outdated network packets.
        /// With the boolean being true the outdated packet will be ignored on receive.
        /// </summary>
        [SerializeField] private bool skipOutdatedPackets;

        private int _lastPacketNumber;
        private Dictionary<string, DynamicWrapper> _dynamicWrappers;
        private Dictionary<string, DynamicObject> _trackedObjects;
        private Dictionary<Tuple<string, string>, DynamicVariableBase> _trackedVariables;
        private List<InstantiationModel> _objectsToInstantiate;
        private List<string> _objectsToDestroy;
        private List<Action> _mainThreadActions;
        private User _user;
        private Dictionary<Room, Coroutine> _roomCoroutines;
        private void Awake()
        {
            _trackedObjects = new Dictionary<string, DynamicObject>();
            _objectsToDestroy = new List<string>();
            _objectsToInstantiate = new List<InstantiationModel>();
            _trackedVariables = new Dictionary<Tuple<string, string>, DynamicVariableBase>();
            _dynamicWrappers = new Dictionary<string, DynamicWrapper>();
            _roomCoroutines = new Dictionary<Room, Coroutine>();
            _mainThreadActions = new List<Action>();
            _lastPacketNumber = 0;
        }
        private void Start()
        {
            RealtimeObserversManager.Instance.AddObserver(this, observerId);
        }

        /// <summary>
        /// Starts the synchronization process for a specific room and user.
        /// </summary>
        /// <param name="room">The room to synchronize.</param>
        /// <param name="user">The local user.</param>
        /// <param name="isSender">True if this observer sends updates; false otherwise.</param>
        public void StartSync(Room room, User user, bool isSender)
        {
            _user = user;
            room.OnMessageReceived += MessageReceived;
            if (isSender)
                _roomCoroutines.Add(room, StartCoroutine(StartSyncing(room)));
        }

        /// <summary>
        /// Stops the synchronization process for a specific room.
        /// </summary>
        /// <param name="room">The room to stop syncing.</param>
        public void StopSync(Room room)
        {
            StopCoroutine(_roomCoroutines[room]);
            _roomCoroutines.Remove(room);
        }
        private void Update()
        {
            if (_mainThreadActions.Count > 0)
            {
                foreach (var action in _mainThreadActions)
                {
                    action();
                }
                _mainThreadActions.Clear();
            }
        }
        private void MessageReceived(object sender, Request e)
        {
            if (e.SenderId == _user.Id)
                return;

            var intermediatePayload = JsonConvert.DeserializeObject<string>(e.Payload);
            var data = JsonConvert.DeserializeObject<RealtimeObservationModel>(intermediatePayload);

            if (skipOutdatedPackets && _lastPacketNumber >= data.lastPacketObservation)
                return;

            foreach (var instantiationModel in data.instantiations)
            {
                _mainThreadActions.Add(() =>
                {
                    var temp = instantiationModel.inScene
                        ? Instantiate(_dynamicWrappers[instantiationModel.objectName], instantiationModel.position,
                            Quaternion.Euler(instantiationModel.rotation))
                        : Instantiate(Resources.Load<DynamicWrapper>(instantiationModel.objectName),
                            instantiationModel.position, Quaternion.Euler(instantiationModel.rotation));
                    temp.SetGuid(instantiationModel.guid);
                });
            }

            foreach (var part in data.messageParts)
            {
                switch (part.type)
                {
                    case SyncType.Position:
                        _mainThreadActions.Add(() => _trackedObjects[part.guid].transform.position = part.vector);
                        break;
                    case SyncType.Rotation:
                        _mainThreadActions.Add(() => _trackedObjects[part.guid].transform.rotation = Quaternion.Euler(part.vector));
                        break;
                    case SyncType.Scale:
                        _mainThreadActions.Add(() => _trackedObjects[part.guid].transform.localScale = part.vector);
                        break;
                    case SyncType.ActiveState:
                        _mainThreadActions.Add(() => _trackedObjects[part.guid].gameObject.SetActive(part.vector == Vector3.one));
                        break;
                }
            }

            foreach (var part in data.variables)
                _mainThreadActions.Add(() => _trackedVariables[new Tuple<string, string>(part.guid, part.fieldName)]
                    .SetValueByDeserializedString(part.data));

            foreach (var part in data.destroys)
                _mainThreadActions.Add(() => Destroy(_dynamicWrappers[part].gameObject));

            _lastPacketNumber = data.lastPacketObservation;
        }
        private IEnumerator StartSyncing(Room room)
        {
            while (true)
            {
                var temp = new RealtimeObservationModel
                {
                    instantiations = new List<InstantiationModel>(_objectsToInstantiate),
                    destroys = new List<string>(_objectsToDestroy),
                    lastPacketObservation = _lastPacketNumber++
                };

                _objectsToInstantiate.Clear();
                _objectsToDestroy.Clear();

                foreach (var obj in _trackedObjects.Values)
                {
                    temp.messageParts.AddRange(obj.GetMessageParts());
                }

                foreach (var variable in _trackedVariables.Values)
                {
                    temp.variables.Add(variable.GetVariablePart());
                }

                room.Broadcast(JsonConvert.SerializeObject(temp));
                yield return new WaitForSecondsRealtime(1f / settings.dataTransferRate);
            }
        }

        /// <summary>
        /// Tracks a dynamic object for synchronization.
        /// </summary>
        /// <param name="dynamicObject">The dynamic object to track.</param>
        public void TrackObject(DynamicObject dynamicObject)
        {
            _trackedObjects.Add(dynamicObject.GetGuid(), dynamicObject);
        }

        /// <summary>
        /// Stops tracking a dynamic object.
        /// </summary>
        /// <param name="dynamicObject">The dynamic object to untrack.</param>
        public void UnTrackObject(DynamicObject dynamicObject)
        {
            _trackedObjects.Remove(dynamicObject.GetGuid());
        }

        /// <summary>
        /// Adds a dynamic wrapper for synchronization.
        /// </summary>
        /// <param name="dynamicObject">The dynamic wrapper to add.</param>
        public void AddDynamicWrapper(DynamicWrapper dynamicObject)
        {
            _dynamicWrappers.Add(dynamicObject.GetGuid(), dynamicObject);
        }

        /// <summary>
        /// Removes a dynamic wrapper from synchronization.
        /// </summary>
        /// <param name="dynamicObject">The dynamic wrapper to remove.</param>
        public void RemoveDynamicWrapper(DynamicWrapper dynamicObject)
        {
            _dynamicWrappers.Remove(dynamicObject.GetGuid());
        }

        /// <summary>
        /// Tracks a variable for synchronization.
        /// </summary>
        /// <param name="guid">The GUID of the object owning the variable.</param>
        /// <param name="fieldName">The field name of the variable.</param>
        /// <param name="variable">The variable to track.</param>
        public void TrackVariable(string guid, string fieldName, DynamicVariableBase variable)
        {
            _trackedVariables.Add(new Tuple<string, string>(guid, fieldName), variable);
        }

        /// <summary>
        /// Stops tracking a variable.
        /// </summary>
        /// <param name="guid">The GUID of the object owning the variable.</param>
        /// <param name="fieldName">The field name of the variable.</param>
        /// <param name="variable">The variable to untrack.</param>
        public void UnTrackVariable(string guid, string fieldName, DynamicVariableBase variable)
        {
            _trackedVariables.Remove(new Tuple<string, string>(guid, fieldName));
        }

        /// <summary>
        /// Instantiates a dynamic wrapper from the scene and tracks it.
        /// </summary>
        /// <param name="objectToInstantiate">The object to instantiate.</param>
        /// <param name="position">The position for the new object.</param>
        /// <param name="rotation">The rotation for the new object.</param>
        /// <returns>The instantiated dynamic wrapper.</returns>
        public DynamicWrapper InstantiateFromScene(DynamicWrapper objectToInstantiate, Vector3 position, Quaternion rotation)
        {
            var tempObject = Instantiate(objectToInstantiate, position, rotation);
            tempObject.ResetGuid();
            var dataobject = new InstantiationModel
            {
                inScene = true,
                objectName = objectToInstantiate.GetGuid(),
                guid = tempObject.GetGuid(),
                position = position,
                rotation = rotation.eulerAngles
            };
            _objectsToInstantiate.Add(dataobject);
            return tempObject;
        }

        /// <summary>
        /// Instantiates a dynamic wrapper from resources and tracks it.
        /// </summary>
        /// <param name="objectToInstantiate">The name of the object to instantiate.</param>
        /// <param name="position">The position for the new object.</param>
        /// <param name="rotation">The rotation for the new object.</param>
        /// <returns>The instantiated dynamic wrapper or null if not found.</returns>
        public DynamicWrapper InstantiateFromResources(string objectToInstantiate, Vector3 position, Quaternion rotation)
        {
            DynamicWrapper wrapperToInstantiate = Resources.Load<DynamicWrapper>(objectToInstantiate);
            if (!wrapperToInstantiate)
            {
                Debug.LogError("[DynamicPixels] Object not found in resources");
                return null;
            }

            var tempObject = Instantiate(wrapperToInstantiate, position, rotation);
            tempObject.ResetGuid();
            var dataobject = new InstantiationModel
            {
                inScene = false,
                objectName = objectToInstantiate,
                guid = tempObject.GetGuid(),
                position = position,
                rotation = rotation.eulerAngles
            };
            _objectsToInstantiate.Add(dataobject);
            return tempObject;
        }

        /// <summary>
        /// Destroys a tracked dynamic wrapper and synchronizes the destruction.
        /// </summary>
        /// <param name="guid">The GUID of the wrapper to destroy.</param>
        public void DestroyDynamicWrapper(string guid)
        {
            if (!_dynamicWrappers.ContainsKey(guid))
                return;

            var dataobject = guid;
            _objectsToDestroy.Add(dataobject);
            Destroy(_dynamicWrappers[guid].gameObject);
        }
    }
}
