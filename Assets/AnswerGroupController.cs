using System.Collections.Generic;
using UnityEngine;

public class AnswerGroupController : MonoBehaviour
{
    AnswerController[] answersCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        answersCtrl = GetComponentsInChildren<AnswerController>();
        Debug.Log($"count: {answersCtrl.Length}");
    }

    public void UpdateAnswers(List<Answer> answers)
    {
        for (var i = 0; i < answersCtrl.Length; ++i)
        {
            if (i < answers.Count)
            {
                answersCtrl[i].UpdateText(answers[i].answer, answers[i].cost);
            }
            else
            {
                answersCtrl[i].Clear();
            }
        }
        
    }
}
