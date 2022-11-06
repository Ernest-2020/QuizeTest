using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class WordLetter : MonoBehaviour,IShowableText
{
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI letterText;

    private void OnEnable()
    {
        Hide();
    }
    public void Show()
    {
        blockImage.gameObject.SetActive(false);
    }

    public void Hide()
    {
        blockImage.gameObject.SetActive(true);
    }
    
    public void UpdateText(string text)
    {
        letterText.text = text;
    }
}
