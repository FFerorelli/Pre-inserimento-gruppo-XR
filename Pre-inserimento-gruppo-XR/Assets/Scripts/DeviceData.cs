using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class DeviceProperties
    {
        public string tipologia;
        public string unita_misura;
    }

    [Serializable]
    public class DeviceData
    {
        public int id;
        public string deviceClassTypeDescription;
        public string name;
        public string guid;
        public DeviceProperties properties;
    }
}
