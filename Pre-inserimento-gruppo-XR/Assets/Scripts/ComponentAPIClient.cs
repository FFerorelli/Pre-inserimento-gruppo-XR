using UnityEngine;
using System;
using System.Collections;
using API;
using Models;

namespace API
{
    public class ComponentAPIClient : MonoBehaviour
    {
        public string baseUrl = "http://tuo-endpoint.com/api";
        public bool simulateCalls = true;

        public void GetComponentById(int id, Action<ComponentData> onSuccess, Action<string> onError)
        {
            if (simulateCalls)
            {
                StartCoroutine(SimulateComponentCall(id, onSuccess, onError));
            }
            else
            {
                string url = $"{baseUrl}/components/{id}";
                APIService.Instance.GetRequest(url, (response) => {
                    ComponentData component = JsonUtility.FromJson<ComponentData>(response);
                    onSuccess?.Invoke(component);
                }, onError);
            }
        }

        private IEnumerator SimulateComponentCall(int id, Action<ComponentData> onSuccess, Action<string> onError)
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
            ComponentData component = JsonUtility.FromJson<ComponentData>(simulatedJson);
            onSuccess?.Invoke(component);
        }
    }
}
