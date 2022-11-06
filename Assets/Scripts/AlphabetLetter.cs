using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class AlphabetLetter : MonoBehaviour,IShowableText
{
    public event Action<string> ClickedButton;
    public event Action<AlphabetLetter> DisablingLetter;
    
    [SerializeField] private Button letterButton;
    [SerializeField] private TextMeshProUGUI letterText;

    private void OnEnable()
    {
        letterButton.onClick.AddListener(ClickButton);
    }

    private void OnDisable()
    {
        letterButton.onClick.RemoveListener(ClickButton);
    }

    private void ClickButton()
    {
        Hide();
        ClickedButton?.Invoke($"{letterText.text[0]}");
       
    }
    
    public void Show()
    {
        letterButton.gameObject.SetActive(true);
    }

    public void Hide()
    {
        DisablingLetter?.Invoke(this);
    }
    
    public void UpdateText(string text)
    {
        letterText.text = text;
    }

    public void Interactable(bool isInteractable)
    {
        letterButton.interactable = isInteractable;
    }
}
