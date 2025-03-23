using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class APIClient : MonoBehaviour
{
    // Identificatore dell'oggetto (impostabile in Inspector)
    public int objectId;

    // Flag per distinguere se l'oggetto rappresenta un component (true) o un device (false)
    public bool isComponent = true;

    // Flag per scegliere se usare chiamate simulate o reali
    public bool simulateCalls = true;

    // Base URL per le chiamate reali (modifica in base al tuo endpoint)
    public string baseUrl = "http://tuo-endpoint.com/api";

    // Metodo chiamato (ad esempio, dall'interazione dell'utente)
    public void GetData()
    {
        // Mostra il pannello Spinner e nascondi il pannello InfoPanel
        ShowSpinner();

        // Avvia la chiamata API in base al tipo di oggetto
        if (isComponent)
        {
            if (simulateCalls)
                StartCoroutine(GetComponentByIdSimulated(objectId));
            else
                StartCoroutine(GetComponentByIdCoroutine(objectId));
        }
        else
        {
            if (simulateCalls)
                StartCoroutine(GetDeviceByIdSimulated(objectId));
            else
                StartCoroutine(GetDeviceByIdCoroutine(objectId));
        }
    }

    // Mostra il pannello Spinner e nasconde InfoPanel
    private void ShowSpinner()
    {
        // Attiva il pannello Spinner
        Transform spinnerTransform = transform.Find("Spinner");
        if (spinnerTransform != null)
        {
            spinnerTransform.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Spinner panel non trovato in " + gameObject.name);
        }

        // Nascondi il pannello InfoPanel
        Transform infoPanelTransform = transform.Find("InfoPanel");
        if (infoPanelTransform != null)
        {
            infoPanelTransform.gameObject.SetActive(false);
        }
    }

    // Aggiorna l'InfoPanel con il testo finale e nasconde il pannello Spinner
    private void UpdatePanelWithText(string text)
    {
        // Nascondi il pannello Spinner
        Transform spinnerTransform = transform.Find("Spinner");
        if (spinnerTransform != null)
        {
            spinnerTransform.gameObject.SetActive(false);
        }

        // Trova il pannello InfoPanel
        Transform infoPanelTransform = transform.Find("InfoPanel");
        if (infoPanelTransform == null)
        {
            Debug.LogWarning("InfoPanel non trovato in " + gameObject.name);
            return;
        }
        GameObject infoPanel = infoPanelTransform.gameObject;
        infoPanel.SetActive(true);

        // Aggiorna il componente TMP_Text all'interno di InfoPanel
        TMP_Text tmpText = infoPanel.GetComponentInChildren<TMP_Text>(true);
        if (tmpText != null)
        {
            tmpText.text = text;
        }
        else
        {
            Debug.LogWarning("TMP_Text non trovato in InfoPanel di " + gameObject.name);
        }
    }

    private void UpdatePanelWithError(string error)
    {
        UpdatePanelWithText("Errore: " + error);
    }

    // --- Chiamate API per il Component (Estintore) ---

    private IEnumerator GetComponentByIdCoroutine(int id)
    {
        string url = $"{baseUrl}/components/{id}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore nella chiamata API Component: " + request.error);
            UpdatePanelWithError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Risposta API Component: " + jsonResponse);
            ComponentData component = JsonUtility.FromJson<ComponentData>(jsonResponse);

            string infoText = $"Component:\n" +
                              $"ID: {component.id}\n" +
                              $"Name: {component.name}\n" +
                              $"Type: {component.componentTypeDescription}\n" +
                              $"Scadenza: {component.properties.data_scadenza}\n" +
                              $"Ultima manutenzione: {component.properties.data_ultima_manutenzione}";
            UpdatePanelWithText(infoText);
        }
    }

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

        Debug.Log("Risposta simulata API Component: " + simulatedJson);
        ComponentData component = JsonUtility.FromJson<ComponentData>(simulatedJson);

        string infoText = $"Component:\n" +
                          $"ID: {component.id}\n" +
                          $"Name: {component.name}\n" +
                          $"Type: {component.componentTypeDescription}\n" +
                          $"Scadenza: {component.properties.data_scadenza}\n" +
                          $"Ultima manutenzione: {component.properties.data_ultima_manutenzione}";
        UpdatePanelWithText(infoText);
    }

    // --- Chiamate API per il Device (Sensore Ambientale) ---

    private IEnumerator GetDeviceByIdCoroutine(int id)
    {
        string url = $"{baseUrl}/devices/{id}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore nella chiamata API Device: " + request.error);
            UpdatePanelWithError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Risposta API Device: " + jsonResponse);
            DeviceData device = JsonUtility.FromJson<DeviceData>(jsonResponse);

            string infoText = $"Device:\n" +
                              $"ID: {device.id}\n" +
                              $"Name: {device.name}\n" +
                              $"Type: {device.deviceClassTypeDescription}\n" +
                              $"Tipologia: {device.properties.tipologia}\n" +
                              $"Unità di misura: {device.properties.unita_misura}";
            UpdatePanelWithText(infoText);
        }
    }

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

        Debug.Log("Risposta simulata API Device: " + simulatedJson);
        DeviceData device = JsonUtility.FromJson<DeviceData>(simulatedJson);

        string infoText = $"Device:\n" +
                          $"ID: {device.id}\n" +
                          $"Name: {device.name}\n" +
                          $"Type: {device.deviceClassTypeDescription}\n" +
                          $"Tipologia: {device.properties.tipologia}\n" +
                          $"Unità di misura: {device.properties.unita_misura}";
        UpdatePanelWithText(infoText);
    }
}
