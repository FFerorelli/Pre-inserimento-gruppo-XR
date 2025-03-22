using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIClient : MonoBehaviour
{
    // Imposta a true per simulare le risposte API; false per chiamate reali.
    public bool simulateCalls = true;

    // Base URL per le chiamate reali (modifica in base al tuo endpoint)
    public string baseUrl = "http://endpoint.com/api";

    private void Start()
    {

    }

    // Metodo pubblico per ottenere il dettaglio del componente
    public void GetComponentById(int id)
    {
        if (simulateCalls)
            StartCoroutine(GetComponentByIdSimulated(id));
        else
            StartCoroutine(GetComponentByIdCoroutine(id));
    }

    // Chiamata reale usando UnityWebRequest
    private IEnumerator GetComponentByIdCoroutine(int id)
    {
        string url = $"{baseUrl}/components/{id}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore nella chiamata API Component: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Risposta API Component: " + jsonResponse);

            // Parsing della risposta JSON
            ComponentData component = JsonUtility.FromJson<ComponentData>(jsonResponse);
            Debug.Log("Component Name: " + component.name);
        }
    }

    // Chiamata simulata per il componente
    private IEnumerator GetComponentByIdSimulated(int id)
    {
        yield return new WaitForSeconds(1f);

        string simulatedJson = $@"{{
        ""id"": {id},
        ""componentTypeDescription"": ""Extinguisher"",
        ""name"": ""EXT_1"",
        ""guid"": ""8c900b13-d10e-4124-8f4a-97271e1b1704"",
        ""properties"": {{
            ""data_scadenza"": ""2025-12-31"",
            ""data_ultima_manutenzione"": ""2025-01-31""
        }}
    }}";

        Debug.Log("Risposta Simulata API Component: " + simulatedJson);
        ComponentData component = JsonUtility.FromJson<ComponentData>(simulatedJson);
        Debug.Log("Component Name (simulato): " + component.name);
    }

    // Metodo pubblico per ottenere il dettaglio del device
    public void GetDeviceById(int id)
    {
        if (simulateCalls)
            StartCoroutine(GetDeviceByIdSimulated(id));
        else
            StartCoroutine(GetDeviceByIdCoroutine(id));
    }

    // Chiamata reale per il device
    private IEnumerator GetDeviceByIdCoroutine(int id)
    {
        string url = $"{baseUrl}/devices/{id}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore nella chiamata API Device: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Risposta API Device: " + jsonResponse);

            // Parsing della risposta JSON
            DeviceData device = JsonUtility.FromJson<DeviceData>(jsonResponse);
            Debug.Log("Device Name: " + device.name);
        }
    }

    // Chiamata simulata per il device
    private IEnumerator GetDeviceByIdSimulated(int id)
    {
        yield return new WaitForSeconds(1f);

        string simulatedJson = $@"{{
        ""id"": {id},
        ""deviceClassTypeDescription"": ""Meter"",
        ""name"": ""DEV_0"",
        ""guid"": ""01946477-b9ec-7d57-9399-f83c74caea70"",
        ""properties"": {{
            ""tipologia"": ""Temperatura"",
            ""unita_misura"": ""°C""
        }}
    }}";

        Debug.Log("Risposta Simulata API Device: " + simulatedJson);
        DeviceData device = JsonUtility.FromJson<DeviceData>(simulatedJson);
        Debug.Log("Device Name (simulato): " + device.name);
    }
}
