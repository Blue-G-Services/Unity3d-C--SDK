using System;
using System.Collections.Generic;
using DynamicPixels.UnityBaseFiles;
using DynamicPixelsInitializer;
using UnityEngine;
using UnityEngine.Serialization;

namespace DynamicPixels.Services.MultiPlayer.Realtime
{
    /// <summary>
    /// Represents a dynamic object in the multiplayer environment that can synchronize its position, rotation, and scale.
    /// </summary>
    public class DynamicObject : DynamicWrapper
    {
        /// <summary>
        /// Indicates whether the object's position should be synchronized.
        /// </summary>
        [SerializeField] private bool syncPosition;

        /// <summary>
        /// Indicates whether the object's rotation should be synchronized.
        /// </summary>
        [SerializeField] private bool syncRotation;

        /// <summary>
        /// Indicates whether the object's scale should be synchronized.
        /// </summary>
        [SerializeField] private bool syncScale;
        
        /// <summary>
        /// Indicates whether the game object's Enable/Disable state should be synchronized.
        /// </summary>
        [SerializeField] private bool syncGameObjectActiveState;

        private bool _init;
        private new void Start()
        {
            base.Start();
            _init = false;
            
        }
        private new void OnDestroy()
        {
            base.OnDestroy();
            RealtimeObserversManager.Instance.GetObserver(observerId).UnTrackObject(this);
        }

        private new void Update()
        {
            base.Update();
            if (_init) return;
            RealtimeObserversManager.Instance.GetObserver(observerId).TrackObject(this);
            _init = true;
        }

        /// <summary>
        /// Collects the synchronization data for the object's position, rotation, and scale.
        /// </summary>
        /// <returns>A list of synchronization message parts describing the object's state.</returns>
        public List<SyncingMessagePart> GetMessageParts()
        {
            var result = new List<SyncingMessagePart>();
            if (syncPosition)
                result.Add(new SyncingMessagePart() { guid = guid, vector = transform.position, type = SyncType.Position });
            if (syncRotation)
                result.Add(new SyncingMessagePart() { guid = guid, vector = transform.rotation.eulerAngles, type = SyncType.Rotation });
            if (syncScale)
                result.Add(new SyncingMessagePart() { guid = guid, vector = transform.localScale, type = SyncType.Scale });
            if (syncGameObjectActiveState)
                result.Add(new SyncingMessagePart() { guid = guid, vector = gameObject.activeSelf ? Vector3.one : Vector3.zero, type = SyncType.ActiveState });
            return result;
        }
    }
}
