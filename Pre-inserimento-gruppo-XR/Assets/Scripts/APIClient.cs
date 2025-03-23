using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class APIClient : MonoBehaviour
{
    // Identificatore dell'oggetto (impostabile da Inspector)
    public int objectId;

    // Indica se l'oggetto rappresenta un componente (true) oppure un device (false)
    public bool isComponent = true;

    // Flag per utilizzare chiamate simulate o reali
    public bool simulateCalls = true;

    // Base URL per le chiamate reali (modifica in base al tuo endpoint)
    public string baseUrl = "http://tuo-endpoint.com/api";

    // Metodo che può essere chiamato (ad es. da un evento di interazione)
    public void GetData()
    {
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

    // ------------------------------
    // Chiamate per il Component (estintore)
    // ------------------------------

    private IEnumerator GetComponentByIdCoroutine(int id)
    {
        string url = $"{baseUrl}/components/{id}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore nella chiamata API Component: " + request.error);
            UpdatePanelWithText("Errore API Component: " + request.error);
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

        Debug.Log("Risposta Simulata API Component: " + simulatedJson);
        ComponentData component = JsonUtility.FromJson<ComponentData>(simulatedJson);

        string infoText = $"Component:\n" +
                          $"ID: {component.id}\n" +
                          $"Name: {component.name}\n" +
                          $"Type: {component.componentTypeDescription}\n" +
                          $"Scadenza: {component.properties.data_scadenza}\n" +
                          $"Ultima manutenzione: {component.properties.data_ultima_manutenzione}";
        UpdatePanelWithText(infoText);
    }

    // ------------------------------
    // Chiamate per il Device (sensore ambientale)
    // ------------------------------

    private IEnumerator GetDeviceByIdCoroutine(int id)
    {
        string url = $"{baseUrl}/devices/{id}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Errore nella chiamata API Device: " + request.error);
            UpdatePanelWithText("Errore API Device: " + request.error);
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

        Debug.Log("Risposta Simulata API Device: " + simulatedJson);
        DeviceData device = JsonUtility.FromJson<DeviceData>(simulatedJson);

        string infoText = $"Device:\n" +
                          $"ID: {device.id}\n" +
                          $"Name: {device.name}\n" +
                          $"Type: {device.deviceClassTypeDescription}\n" +
                          $"Tipologia: {device.properties.tipologia}\n" +
                          $"Unità di misura: {device.properties.unita_misura}";
        UpdatePanelWithText(infoText);
    }

    // ------------------------------
    // Metodi per aggiornare il pannello figlio
    // ------------------------------

    private void UpdatePanelWithText(string text)
    {       
        Transform panelTransform = transform.Find("InfoPanel");
        if (panelTransform == null)
        {
            Debug.LogWarning("InfoPanel non trovato come figlio di " + gameObject.name);
            return;
        }

        GameObject panel = panelTransform.gameObject;
        panel.SetActive(true);
        
        TMP_Text tmpText = panel.GetComponentInChildren<TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = text;
        }
        else
        {
            Debug.LogWarning("Componente TMP_Text non trovato nel pannello " + panel.name);
        }
    }
}
