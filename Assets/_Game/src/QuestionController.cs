using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionController : MonoBehaviour
{
    public TextAsset data;
    public AudioClip unableToAnswerSFX;
    public AudioClip answerSFX;
    public AudioSource audioSource;
    public MengNanValue currentMengNanValue;
    public FloatValue currentInterviewScore;
    public StopWatch stopWatch;

    public TextMeshProUGUI questionText;
    public AnswerGroupControl answersGroup;

    [SerializeField]
    List<Question> questions;
    List<Answer> currentAnswers;
    // Animator m_master_animator;        
    System.DateTime m_start_tick;
    // System.DateTime m_start_answer_tick;
    Animator m_master_animator = null;
    int m_next_action_state;
    
    void Awake()
    {
        m_next_action_state = MyConst.ACTION_STATE_UNKNOWN;
        if( null==m_master_animator )
        {
            GameObject master_go = GameObject.Find("Master");
            m_master_animator = master_go.GetComponentInChildren<Animator>();
        }
        Parse();
    }

    void Start()
    {
        // GameObject master_go = GameObject.Find("Master");
        // m_master_animator = master_go.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectAnswer(0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SelectAnswer(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SelectAnswer(2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SelectAnswer(3);
        }
        System.TimeSpan span = System.DateTime.Now - m_start_tick;
        if( null==m_master_animator )
        {
            GameObject master_go = GameObject.Find("Master");
            m_master_animator = master_go.GetComponentInChildren<Animator>();
        }
        if( span.TotalMilliseconds > 2000 )
        {
            if( MyConst.ACTION_STATE_WAIT_QUESTION == m_next_action_state )
            {
                m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_WAIT_QUESTION );
                m_next_action_state = MyConst.ACTION_STATE_UNKNOWN;
            }
            else if( MyConst.ACTION_STATE_ASK_QUESTION == m_next_action_state )
            {
                m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_ASK_QUESTION );
                m_next_action_state = MyConst.ACTION_STATE_UNKNOWN;
                // next question
                Debug.Log("ask next question when answer");
                stopWatch.StartTheWatch();
                AskQuestion();

            }
        }
        // span = System.DateTime.Now - m_start_answer_tick;
        // if( span.TotalMilliseconds > 2000 )
        // {
        //     m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_ASK_QUESTION );
        // }
    }

    void SelectAnswer(int i)
    {
        var mengNan = currentMengNanValue.GetValue();
        if (currentAnswers.Count > i)
        {
            if (currentAnswers[i].cost <= mengNan)
            {
                Debug.Log($"execute answer: {currentAnswers[i].answer}");
                currentAnswers[i].Execute(currentMengNanValue, currentInterviewScore);
                audioSource.clip = unableToAnswerSFX;
                audioSource.Play();
                
                m_start_tick = System.DateTime.Now;
                m_next_action_state = MyConst.ACTION_STATE_ASK_QUESTION;
                m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_REVIEW_ANSWER ); 
                
                return;
            }
        }
        Debug.Log($"unable to execute answer");
        audioSource.clip = answerSFX;
        audioSource.Play();

    }

    void Parse()
    {
        // Debug.Log(data.text);
        var parsed = JsonUtility.FromJson<Questions>(data.text);
        questions = parsed.questions;
    }

    public void AskQuestion()
    {
        Debug.Log("question asked");
        var question = Pick();
        currentAnswers = question.answers;
        UpdateGUI(question);

        m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_ASK_QUESTION );
        m_start_tick = System.DateTime.Now;
        m_next_action_state = MyConst.ACTION_STATE_WAIT_QUESTION;
    }

    void UpdateGUI(Question question)
    {
        questionText.text = question.question;
        answersGroup.UpdateAnswers(question.answers);
    }

    Question Pick()
    {
        Debug.Log($"{questions.Count}");
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
    public int cost;
    public int score;

    public void Execute(MengNanValue mengNanValue, FloatValue interviewScore)
    {
        var mengNan = mengNanValue.GetValue();
        mengNanValue.SetValue((int)(mengNan - cost));
        interviewScore.value += score;
    }
}
