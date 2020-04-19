using UnityEngine;
using UnityEngine.UI;

public class InterviewScoreGUIControl : MonoBehaviour
{
    public FloatValue value;
    public Image slider;

    public int maxValue;
    
    void Update()
    {
        slider.fillAmount = value.value / maxValue;
    }
}
