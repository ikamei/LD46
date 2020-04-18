﻿using System.Collections.Generic;
using UnityEngine;

public class AnswerGroupControl : MonoBehaviour
{
    AnswerGUIControl[] answersCtrl;

    // Start is called before the first frame update
    void Start()
    {
        answersCtrl = GetComponentsInChildren<AnswerGUIControl>();
        Debug.Log($"count: {answersCtrl.Length}");
    }

    public void UpdateAnswers(List<Answer> newAnswers)
    {
        for (var i = 0; i < answersCtrl.Length; ++i)
        {
            if (i < newAnswers.Count)
            {
                answersCtrl[i].UpdateText(newAnswers[i].answer, newAnswers[i].cost);
            }
            else
            {
                answersCtrl[i].Clear();
            }
        }
        
    }
}