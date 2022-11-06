using TMPro;
using UnityEngine;

public sealed class AttemptsView : MonoBehaviour,IUpdatableText
{
    [SerializeField] private TextMeshProUGUI countAttemptsText;
    
    public void UpdateText(string text)
    {
        countAttemptsText.text = text;
    }
}
    
