using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using DynamicPixels.UnityBaseFiles;
using DynamicPixelsInitializer;
using Newtonsoft.Json;
using UnityEngine;

namespace DynamicPixels.Services.MultiPlayer.Realtime
{
    /// <summary>
    /// Represents a MonoBehaviour that manages synchronization and tracking of dynamic variables
    /// in a real-time multiplayer environment.
    /// Every class that contains a dynamic variable must inherit from this class.
    /// Also note that you should not override the Start, OnDestroy and Update function unless you call the base.Start(), base.OnDestroy() and base.Update() functions in them.
    /// </summary>
    public class DynamicWrapper : MonoBehaviour
    {
        /// <summary>
        /// A unique identifier for the object, used for synchronization purposes.
        /// </summary>
        [HideInInspector] [SerializeField] protected string guid;

        /// <summary>
        /// The ID of the observer associated with this object.
        /// </summary>
        [SerializeField] protected int observerId;
        
         private bool _initialized = false;

        /// <summary>
        /// Ensures the GUID is valid and generates a new one if empty during the Unity editor's validation phase.
        /// </summary>
        private void OnValidate()
        {
            if (String.IsNullOrEmpty(guid))
            {
                guid = Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// Resets the GUID and assigns a new value.
        /// </summary>
        public void ResetGuid()
        {
            guid = "";
            SetGuid();
        }

        /// <summary>
        /// Initializes the object, registers it with the observer, and initializes all dynamic variables.
        /// </summary>
        public void Start()
        {
            if (String.IsNullOrEmpty(guid))
                guid = Guid.NewGuid().ToString();

            FieldInfo[] fields =
                GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.FieldType.IsSubclassOf(typeof(DynamicVariableBase)))
                {
                    DynamicVariableBase value = (DynamicVariableBase)field.GetValue(this);
                    value.Initialize(RealtimeObserversManager.Instance.GetObserver(observerId), field.Name);
                }
            }
        }

        public void Update()
        {
            if(_initialized) return;
            RealtimeObserversManager.Instance.GetObserver(observerId).AddDynamicWrapper(this);
            _initialized = true;
        }

        /// <summary>
        /// Cleans up the object by unregistering it from the observer and stopping the tracking of dynamic variables.
        /// </summary>
        public void OnDestroy()
        {
            RealtimeObserversManager.Instance.GetObserver(observerId).RemoveDynamicWrapper(this);

            FieldInfo[] fields =
                GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.FieldType.IsSubclassOf(typeof(DynamicVariableBase)))
                {
                    DynamicVariableBase value = (DynamicVariableBase)field.GetValue(this);
                    value.StopTracking();
                }
            }
        }

        /// <summary>
        /// Gets the GUID of the object.
        /// </summary>
        /// <returns>The GUID as a string.</returns>
        public string GetGuid()
        {
            return guid;
        }

        /// <summary>
        /// Sets the GUID of the object. If no GUID is provided, a new one is generated.
        /// </summary>
        /// <param name="guidToSet">The GUID to set. If empty, a new GUID is generated.</param>
        public void SetGuid(string guidToSet = "")
        {
            guid = String.IsNullOrEmpty(guidToSet) ? Guid.NewGuid().ToString() : guidToSet;
        }
    }

    /// <summary>
    /// Base class for dynamic variables that support synchronization and tracking.
    /// </summary>
    public class DynamicVariableBase
    {
        /// <summary>
        /// The observer responsible for managing the dynamic variable.
        /// </summary>
        protected RealtimeObserver Observer;

        /// <summary>
        /// The name of the field associated with the dynamic variable.
        /// </summary>
        protected string FieldName;

        /// <summary>
        /// Initializes the dynamic variable with the specified observer and field name.
        /// </summary>
        /// <param name="observer">The observer managing the variable.</param>
        /// <param name="fieldName">The name of the field.</param>
        public virtual void Initialize(RealtimeObserver observer, string fieldName)
        {
        }

        /// <summary>
        /// Sets the variable value from a deserialized string representation.
        /// </summary>
        /// <param name="value">The serialized string representation of the value.</param>
        public virtual void SetValueByDeserializedString(string value)
        {
        }

        /// <summary>
        /// Starts tracking the dynamic variable.
        /// </summary>
        public virtual void StartTracking()
        {
        }

        /// <summary>
        /// Gets the synchronized data representation of the variable.
        /// </summary>
        /// <returns>A <see cref="SyncingVariable"/> object representing the variable.</returns>
        public virtual SyncingVariable GetVariablePart()
        {
            return null;
        }

        /// <summary>
        /// Stops tracking the dynamic variable.
        /// </summary>
        public virtual void StopTracking()
        {
        }
    }

    /// <summary>
    /// A generic dynamic variable that supports type-safe value synchronization and change tracking.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    [Serializable]
    public class DynamicVariable<T> : DynamicVariableBase
    {
        /// <summary>
        /// The current value of the dynamic variable.
        /// </summary>
        private T _value;

        /// <summary>
        /// The parent <see cref="DynamicWrapper"/> that owns this variable.
        /// </summary>
        private DynamicWrapper _parentGameObject;

        /// <summary>
        /// Event triggered when the value of the variable changes by the observer or a serialized string.
        /// </summary>
        public Action<T, T> OnValueChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVariable{T}"/> class.
        /// </summary>
        /// <param name="parentGameObject">The parent <see cref="DynamicWrapper"/>.</param>
        public DynamicVariable(DynamicWrapper parentGameObject)
        {
            _parentGameObject = parentGameObject;
        }

        /// <inheritdoc />
        public override void Initialize(RealtimeObserver observer, string fieldName)
        {
            Observer = observer;
            FieldName = fieldName;
        }

        /// <inheritdoc />
        public override void SetValueByDeserializedString(string value)
        {
            var temp = _value;
            _value = JsonConvert.DeserializeObject<T>(value);
            OnValueChanged?.Invoke(temp, _value);
        }

        /// <inheritdoc />
        public override void StartTracking()
        {
            Observer.TrackVariable(_parentGameObject.GetGuid(), FieldName, this);
        }

        /// <inheritdoc />
        public override void StopTracking()
        {
            Observer.UnTrackVariable(_parentGameObject.GetGuid(), FieldName, this);
        }

        /// <summary>
        /// Changes the value of the dynamic variable.
        /// This function won't invoke the OnValueChanged action.
        /// </summary>
        /// <param name="value">The new value to set.</param>
        public void ChangeValue(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the current value of the dynamic variable.
        /// </summary>
        /// <returns>The current value.</returns>
        public T GetValue()
        {
            return _value;
        }

        /// <inheritdoc />
        public override SyncingVariable GetVariablePart()
        {
            return new SyncingVariable
            {
                data = JsonConvert.SerializeObject(_value),
                fieldName = FieldName,
                guid = _parentGameObject.GetGuid()
            };
        }
    }
}