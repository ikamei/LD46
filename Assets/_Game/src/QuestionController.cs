using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionController : MonoBehaviour
{
    public TextAsset data;

    public TextMeshProUGUI questionText;
    public AnswerGroupController answersGroup;

    List<Question> questions;
    
    void Start()
    {
        Parse();
    }
    
    void OnGUI() {
        if (GUILayout.Button("Ask Question"))
            AskQuestion();
    }

    void Parse()
    {
        var parsed = JsonUtility.FromJson<Questions>(data.text);
        questions = parsed.questions;
    }

    void AskQuestion()
    {
        var question = Pick();
        UpdateGUI(question);
    }

    void UpdateGUI(Question question)
    {
        questionText.text = question.question;
        answersGroup.UpdateAnswers(question.answers);
    }

    Question Pick()
    {
        return questions[Random.Range(0, questions.Count)];
    }
}

[Serializable]
public class Questions
{
    public List<Question> questions;
}

[Serializable]
public class Question
{
    public string question;
    public List<Answer> answers;
}

[Serializable]
public class Answer
{
    public string answer;
    public float cost;
    public float score;
}
