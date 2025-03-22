using System;
using UnityEngine;

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
