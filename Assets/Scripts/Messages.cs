using TMPro;
using UnityEngine;

    public sealed class Messages:MonoBehaviour, IShowableText
    {
        [SerializeField] private TextMeshProUGUI messageText;
        
        public void UpdateText(string text)
        {
            messageText.text = text;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
