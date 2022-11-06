using TMPro;
using UnityEngine;

public sealed class ScoreView : MonoBehaviour, IUpdatableText
{
    [SerializeField] private TextMeshProUGUI countPointText;

    public void UpdateText(string text)
    {
        countPointText.text = text;
    }
}
