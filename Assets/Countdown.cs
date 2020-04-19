using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float maxTime;
    public Image fill;
    public UnityEvent onTrigger;

    float currentTime;
    bool idle = true;
    
    void Update()
    {
        fill.fillAmount = currentTime / maxTime;
        if (!idle)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                idle = true;
                onTrigger?.Invoke();
            }
        }
    }

    public void StartCountdown()
    {
        currentTime = maxTime;
        idle = false;
    }

    public void Terminate()
    {
        currentTime = 0;
        idle = true;
    }
}
