using UnityEngine;
using System;
using System.Collections;
using API;
using Models;

namespace API
{
    public class DeviceAPIClient : MonoBehaviour
    {
        public string baseUrl = "http://tuo-endpoint.com/api";
        public bool simulateCalls = true;

        public void GetDeviceById(int id, Action<DeviceData> onSuccess, Action<string> onError)
        {
            if (simulateCalls)
            {
                StartCoroutine(SimulateDeviceCall(id, onSuccess, onError));
            }
            else
            {
                string url = $"{baseUrl}/devices/{id}";
                APIService.Instance.GetRequest(url, (response) => {
                    DeviceData device = JsonUtility.FromJson<DeviceData>(response);
                    onSuccess?.Invoke(device);
                }, onError);
            }
        }

        private IEnumerator SimulateDeviceCall(int id, Action<DeviceData> onSuccess, Action<string> onError)
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
            DeviceData device = JsonUtility.FromJson<DeviceData>(simulatedJson);
            onSuccess?.Invoke(device);
        }
    }
}
