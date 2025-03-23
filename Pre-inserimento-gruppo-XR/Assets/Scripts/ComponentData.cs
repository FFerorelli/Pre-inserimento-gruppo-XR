using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class ComponentProperties
    {
        public string data_scadenza;
        public string data_ultima_manutenzione;
    }

    [Serializable]
    public class ComponentData
    {
        public int id;
        public string componentTypeDescription;
        public string name;
        public string guid;
        public ComponentProperties properties;
    }
}
