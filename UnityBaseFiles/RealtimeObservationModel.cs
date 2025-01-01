using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicPixels.UnityBaseFiles
{
    public enum SyncType
    {
        Position,
        Rotation,
        Scale,
        ActiveState
    }
    [Serializable]
    public class SyncingMessagePart
    {
        public string guid;
        public SyncType type;
        public Vector3 vector;
    }

    [Serializable]
    public class SyncingVariable
    {
        public string guid;
        public string fieldName;
        public string data;
    }

    [Serializable]
    public class InstantiationModel
    {
        public bool inScene;
        public string objectName;
        public string guid;
        public Vector3 position;
        public Vector3 rotation;
    }
    [Serializable]
    public class RealtimeObservationModel
    {
        public List<SyncingMessagePart> messageParts = new List<SyncingMessagePart>();
        public List<SyncingVariable> variables = new List<SyncingVariable>();
        public List<InstantiationModel> instantiations = new List<InstantiationModel>();
        public List<string> destroys = new List<string>();
        public int lastPacketObservation;
    }
}
