using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public AudioSource bgm;
    public float lowerVolume = .2f;
    public NumberCountdown countdown;
    
    public const int STATE_START   = 0;
    public const int STATE_INTRO   = 1;
    public const int STATE_LOADING = 2;
    public const int STATE_GAMING  = 3;
    public const int STATE_RESTART = 4;

    int current_state;
    GameObject start_page_go;
    GameObject intro_page_go;
    GameObject loading_page_go;
    GameObject gaming_page_go;
    GameObject restart_page_go;

    List<GameObject> pages_goes;
    MasterAI m_master_ai;



    void Start()
    {
        GameObject master_go = GameObject.Find("Master");
        m_master_ai = master_go.GetComponent<MasterAI>();

        current_state = STATE_START;
        start_page_go   = GameObject.Find("StartPage");
        intro_page_go   = GameObject.Find("IntroPage");
        loading_page_go = GameObject.Find("LoadingPage");
        gaming_page_go  = GameObject.Find("GamingPage");
        restart_page_go = GameObject.Find("RestartPage");

        pages_goes = new List<GameObject>();
        pages_goes.Add( start_page_go );
        pages_goes.Add( intro_page_go );
        pages_goes.Add( loading_page_go );
        pages_goes.Add( gaming_page_go );
        pages_goes.Add( restart_page_go );

        for( int j=0; j<pages_goes.Count; ++j )
            pages_goes[j].active = false;
        pages_goes[STATE_START].active = true;
        ResetRect(pages_goes[STATE_START]);
    }

    void Update()
    {
    }

    void ResetRect(GameObject view)
    {
        Debug.Log("rect reset");
        var rect = view.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
    }

    public void SetState( int state )
    {
        if( state == current_state )
            return;

        if( STATE_START == current_state )
        {
            if( STATE_INTRO == state || STATE_LOADING == state || STATE_GAMING == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
                ResetRect(pages_goes[state]);

                bgm.volume = lowerVolume;
            }
        }
        else if( STATE_INTRO == current_state )
        {
            if( STATE_LOADING == state || STATE_GAMING == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
                ResetRect(pages_goes[state]);

                bgm.Stop();
                // game start
                countdown.StartCountdown();
            }
            // if( STATE_GAMING == state )
            // {
            //     m_master_ai.round_start();
            // }
        }
        else if( STATE_LOADING == current_state )
        {
            if( STATE_INTRO == state || STATE_GAMING == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
                ResetRect(pages_goes[state]);
            }
        }
        else if( STATE_GAMING == current_state )
        {
            if( STATE_RESTART == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
                ResetRect(pages_goes[state]);
                GameObject go = GameObject.Find("RestartPageBackground");
                ScoreRank sr = go.GetComponent<ScoreRank>();
                sr.updateGUI();
            }
        }
        else if( STATE_RESTART == current_state )
        {
            if( STATE_LOADING==state || STATE_GAMING==state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
                ResetRect(pages_goes[state]);
            }
        }
        current_state = state;        
    }
}

