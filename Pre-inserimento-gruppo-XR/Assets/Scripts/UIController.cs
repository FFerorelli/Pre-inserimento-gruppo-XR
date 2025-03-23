using UnityEngine;
using API;
using Models;
using UI;

public class UIController : MonoBehaviour
{
    [Header("Configuration")]
    public bool isComponent = true; // true: Component, false: Device
    public int objectId = 1;        // ID dell'oggetto

    [Header("API Clients")]
    public ComponentAPIClient componentClient;  // Assegnalo tramite Inspector se l'oggetto rappresenta un componente
    public DeviceAPIClient deviceClient;        // Assegnalo se l'oggetto rappresenta un device

    [Header("UI")]
    public PanelManager panelManager;           // Riferimento al PanelManager (lo puoi trovare come figlio)

    // Metodo chiamato dall'interazione dell'utente
    public void RequestData()
    {
        if (panelManager != null)
            panelManager.ShowSpinner();

        if (isComponent)
        {
            if (componentClient != null)
            {
                componentClient.GetComponentById(objectId, OnComponentSuccess, OnError);
            }
            else
            {
                OnError("ComponentAPIClient non assegnato.");
            }
        }
        else
        {
            if (deviceClient != null)
            {
                deviceClient.GetDeviceById(objectId, OnDeviceSuccess, OnError);
            }
            else
            {
                OnError("DeviceAPIClient non assegnato.");
            }
        }
    }

    private void OnComponentSuccess(ComponentData data)
    {
        if (panelManager != null)
        {
            string infoText = $"Component:\n" +
                              $"ID: {data.id}\n" +
                              $"Name: {data.name}\n" +
                              $"Type: {data.componentTypeDescription}\n" +
                              $"Scadenza: {data.properties.data_scadenza}\n" +
                              $"Ultima manutenzione: {data.properties.data_ultima_manutenzione}";
            panelManager.ShowInfo(infoText);
        }
    }

    private void OnDeviceSuccess(DeviceData data)
    {
        if (panelManager != null)
        {
            string infoText = $"Device:\n" +
                              $"ID: {data.id}\n" +
                              $"Name: {data.name}\n" +
                              $"Type: {data.deviceClassTypeDescription}\n" +
                              $"Tipologia: {data.properties.tipologia}\n" +
                              $"Unità di misura: {data.properties.unita_misura}";
            panelManager.ShowInfo(infoText);
        }
    }

    private void OnError(string error)
    {
        if (panelManager != null)
            panelManager.ShowError(error);
    }
}
