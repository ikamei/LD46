using TMPro;
using UnityEngine;

public class AnswerGUIControl : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    public TextMeshProUGUI costText;

    public void UpdateText(string answer, float cost)
    {
        answerText.text = answer;
        costText.text = $"{cost}";
    }

    public void Clear()
    {
        answerText.text = "";
        costText.text = "";
    }
}
