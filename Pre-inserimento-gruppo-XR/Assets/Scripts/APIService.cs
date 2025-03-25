using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

namespace API
{
    // Servizio centrale per le chiamate HTTP (singleton)

    public class APIService : MonoBehaviour
    {
        public static APIService Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Metodo generico per una GET request
        public void GetRequest(string url, Action<string> onSuccess, Action<string> onError)
        {
            StartCoroutine(GetRequestCoroutine(url, onSuccess, onError));
        }

        private IEnumerator GetRequestCoroutine(string url, Action<string> onSuccess, Action<string> onError)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    onError?.Invoke(request.error);
                }
                else
                {
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
            }
        }
    }
}
