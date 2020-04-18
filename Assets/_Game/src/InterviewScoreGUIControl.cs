using UnityEngine;
using UnityEngine.UI;

public class InterviewScoreGUIControl : MonoBehaviour
{
    public FloatValue value;
    public Slider slider;
    
    void Update()
    {
        slider.value = value.value;
    }
}
