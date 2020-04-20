using System;
using System.Collections;
using System.Collections.Generic;
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

    public DialogueControl questionDialog;
    public AnswerGroupControl answersGroup;

    [SerializeField]
    List<Question> questions;
    List<Answer> currentAnswers;
    // System.DateTime m_start_tick;
    // System.DateTime m_start_answer_tick;
    // Animator m_master_animator = null;
    // int m_next_action_state;
    MasterAI m_master_ai;
    void Awake()
    {
        GameObject master_go = GameObject.Find("Master");
        m_master_ai = master_go.GetComponent<MasterAI>();
        // m_next_action_state = MyConst.ACTION_STATE_UNKNOWN;
        // if( null==m_master_animator )
        // {
        //     GameObject master_go = GameObject.Find("Master");
        //     m_master_animator = master_go.GetComponentInChildren<Animator>();
        // }
        Parse();
    }

    void Update()
    {
        if (questionDialog.idle)
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
        }

        // System.TimeSpan span = System.DateTime.Now - m_start_tick;
        // if( null==m_master_animator )
        // {
        //     GameObject master_go = GameObject.Find("Master");
        //     m_master_animator = master_go.GetComponentInChildren<Animator>();
        // }
        // if( span.TotalMilliseconds > 2000 )
        // {
        //     if( MyConst.ACTION_STATE_WAIT_QUESTION == m_next_action_state )
        //     {
        //         m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_WAIT_QUESTION );
        //         m_next_action_state = MyConst.ACTION_STATE_UNKNOWN;
        //     }
        //     else if( MyConst.ACTION_STATE_ASK_QUESTION == m_next_action_state )
        //     {
        //         m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_ASK_QUESTION );
        //         m_next_action_state = MyConst.ACTION_STATE_UNKNOWN;
        //         // next question
        //         Debug.Log("ask next question when answer");
        //         stopWatch.StartTheWatch();
        //         AskQuestion();

        //     }
        // }
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
                answersGroup.HideWithoutAnswer(i);
                questionDialog.idle = false;
                Debug.Log($"execute answer: {currentAnswers[i].answer}");
                // currentAnswers[i].Execute(currentMengNanValue, currentInterviewScore);
                audioSource.clip = answerSFX;
                audioSource.Play();
                
                // m_start_tick = System.DateTime.Now;
                // m_next_action_state = MyConst.ACTION_STATE_ASK_QUESTION;
                // m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_MASTER_AGREE ); 

                // MengNanValue : currentAnswers[i].cost
                // InterviewValue : currentAnswers[i].score
                // Reaction : currentAnswers[i].reaction
                
                m_master_ai.incr_mengnan_value( -1 * currentAnswers[i].cost );
                m_master_ai.incr_score( currentAnswers[i].score );
                if( false == currentAnswers[i].isMacho )
                    m_master_ai.set_isMacho( false );
                // m_master_ai.incr_score( -100 );
                if( 0 == currentAnswers[i].reaction )
                {
                   m_master_ai.set_state( MyConst.ACTION_STATE_MASTER_DISAGREE ); 
                }
                else if( 1 == currentAnswers[i].reaction )
                {
                    m_master_ai.set_state( MyConst.ACTION_STATE_REVIEW_ANSWER );
                }
                else if( 2 == currentAnswers[i].reaction )
                {
                    m_master_ai.set_state( MyConst.ACTION_STATE_MASTER_AGREE );                    
                }
                return;
            }
        }
        Debug.Log($"unable to execute answer");
        audioSource.clip = unableToAnswerSFX;
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
        answersGroup.gameObject.SetActive(false);
        
        Debug.Log("question asked");
        var question = Pick();
        currentAnswers = question.answers;
        UpdateGUI(question);

        StartCoroutine(WaitQuestion());

        // m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_ASK_QUESTION );
        // m_start_tick = System.DateTime.Now;
        // m_next_action_state = MyConst.ACTION_STATE_WAIT_QUESTION;
    }

    IEnumerator WaitQuestion()
    {
        while (questionDialog.idle == false) yield return null;
        answersGroup.gameObject.SetActive(true);
    }

    void UpdateGUI(Question question)
    {
        questionDialog.ShowText(question.question);
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
    public int reaction;
    public bool isMacho;
}
