using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NumberCountdown : MonoBehaviour
{
    public float maxTime;
    public TextMeshProUGUI fill;
    public UnityEvent onTrigger;

    float currentTime;
    bool idle = true;
    
    void Update()
    {
        fill.text = $"{Mathf.FloorToInt(currentTime + .8f)}";
        
        if (!idle)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                idle = true;
                transform.DOMove(new Vector3(0, 600, 0), .5f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
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
