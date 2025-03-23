using UnityEngine;
using TMPro;

namespace UI
{
    public class PanelManager : MonoBehaviour
    {
        [Header("Panel References")]
        public GameObject spinnerPanel;   // Pannello che contiene lo spinner
        public GameObject infoPanel;      // Pannello che contiene il Canvas con il TMP_Text

        private TMP_Text infoText;

        private void Awake()
        {
            if (infoPanel != null)
            {
                infoText = infoPanel.GetComponentInChildren<TMP_Text>(true);
            }
        }

        public void ShowSpinner()
        {
            if (spinnerPanel != null)
                spinnerPanel.SetActive(true);
            if (infoPanel != null)
                infoPanel.SetActive(false);
        }

        public void ShowInfo(string text)
        {
            if (spinnerPanel != null)
                spinnerPanel.SetActive(false);
            if (infoPanel != null)
            {
                infoPanel.SetActive(true);
                if (infoText != null)
                    infoText.text = text;
            }
        }

        public void ShowError(string error)
        {
            ShowInfo("Errore: " + error);
        }
    }
}
