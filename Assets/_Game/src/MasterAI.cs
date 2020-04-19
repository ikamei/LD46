using UnityEngine;
using System.Collections.Generic;
using System;

public class MasterAI : MonoBehaviour
{
    public FloatValue currentInterviewScore;

    int current_state;
    bool m_before_event_callback_enable;
    System.DateTime m_start_tick;
    QuestionController m_question_controller;
    StopWatch m_stopwatch;
    Animator m_master_animator;
    void Start()
    {
        m_before_event_callback_enable = false;
        GameObject go = GameObject.Find("Question Panel");
        m_question_controller = go.GetComponent<QuestionController>();
        go = GameObject.Find("StopWatch");
        m_stopwatch = go.GetComponent<StopWatch>();
        go = GameObject.Find("Master");
        m_master_animator = go.GetComponentInChildren<Animator>();

        current_state = MyConst.ACTION_STATE_UNKNOWN;
        set_state( MyConst.ACTION_STATE_ASK_QUESTION );
    }

    void Update()
    {
        if( true == m_before_event_callback_enable )
        {
            System.TimeSpan span = System.DateTime.Now - m_start_tick;
            if( span.TotalMilliseconds > 2000 )
            {
                OnBeforeEvent();
            }
        }
    }

    void OnBeforeEvent()
    {
        if( false == m_before_event_callback_enable )
            return;

        m_before_event_callback_enable = false;

        if( MyConst.ACTION_STATE_ASK_QUESTION == current_state )
        {
            m_stopwatch.StartTheWatch();
            set_state( MyConst.ACTION_STATE_WAIT_QUESTION );
        }
        else if( MyConst.ACTION_STATE_WAIT_QUESTION == current_state )
        {
        }
        else if( MyConst.ACTION_STATE_REVIEW_ANSWER == current_state )
        {
            set_state( MyConst.ACTION_STATE_ASK_QUESTION );
        }
        else if( MyConst.ACTION_STATE_MASTER_AGREE == current_state )
        {
            set_state( MyConst.ACTION_STATE_ASK_QUESTION );
        }
        else if( MyConst.ACTION_STATE_MASTER_DISAGREE == current_state )
        {
            set_state( MyConst.ACTION_STATE_ASK_QUESTION );
        }
        else if( MyConst.ACTION_STATE_ANSWER_TIMEOUT == current_state )
        {
            set_state( MyConst.ACTION_STATE_ASK_QUESTION );
        }
    }

    public void set_state( int state )
    {
        if( state == current_state )
            return;

        if( MyConst.ACTION_STATE_UNKNOWN == current_state )
        {
            if( MyConst.ACTION_STATE_ASK_QUESTION == state )
            {
                m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_ASK_QUESTION );
                m_question_controller.AskQuestion();
                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
        }
        else if( MyConst.ACTION_STATE_ASK_QUESTION == current_state )
        {
            if( MyConst.ACTION_STATE_WAIT_QUESTION == state )
            {
                m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_WAIT_QUESTION );
                // m_before_event_callback_enable = true;
                // m_start_tick = System.DateTime.Now;
            }
        }
        else if( MyConst.ACTION_STATE_WAIT_QUESTION == current_state )
        {
            if( MyConst.ACTION_STATE_REVIEW_ANSWER == state || MyConst.ACTION_STATE_MASTER_AGREE == state || MyConst.ACTION_STATE_MASTER_DISAGREE == state )
            {
                m_master_animator.SetInteger( "action", state );
                m_stopwatch.StopTheWatch();
                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
            if( MyConst.ACTION_STATE_ANSWER_TIMEOUT == state )
            {
                m_master_animator.SetInteger( "action", MyConst.ACTION_STATE_REVIEW_ANSWER );
                m_stopwatch.StopTheWatch();
                currentInterviewScore.value -= 10;

                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
        }
        else if( MyConst.ACTION_STATE_REVIEW_ANSWER == current_state )
        {
            if( MyConst.ACTION_STATE_ASK_QUESTION == state )
            {
                m_master_animator.SetInteger( "action", state );
                m_question_controller.AskQuestion();
                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
        }
        else if( MyConst.ACTION_STATE_MASTER_AGREE == current_state )
        {
            if( MyConst.ACTION_STATE_ASK_QUESTION == state )
            {
                m_master_animator.SetInteger( "action", state );
                m_question_controller.AskQuestion();
                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
        }
        else if( MyConst.ACTION_STATE_MASTER_DISAGREE == current_state )
        {
            if( MyConst.ACTION_STATE_ASK_QUESTION == state )
            {
                m_master_animator.SetInteger( "action", state );
                m_question_controller.AskQuestion();
                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
        }
        else if( MyConst.ACTION_STATE_ANSWER_TIMEOUT == current_state )
        {
            if( MyConst.ACTION_STATE_ASK_QUESTION == state )
            {
                m_master_animator.SetInteger( "action", state );
                m_question_controller.AskQuestion();
                m_before_event_callback_enable = true;
                m_start_tick = System.DateTime.Now;
            }
        }
        current_state = state;

    }

}
