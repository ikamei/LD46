using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class GameController : MonoBehaviour {
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

    void Start()
    {
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
    }

    void Update()
    {
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
            }
        }
        else if( STATE_INTRO == current_state )
        {
            if( STATE_LOADING == state || STATE_GAMING == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
            }
        }
        else if( STATE_LOADING == current_state )
        {
            if( STATE_INTRO == state || STATE_GAMING == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
            }
        }
        else if( STATE_GAMING == current_state )
        {
            if( STATE_RESTART == state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
            }
        }
        else if( STATE_RESTART == current_state )
        {
            if( STATE_LOADING==state || STATE_GAMING==state )
            {
                for( int j=0; j<pages_goes.Count; ++j )
                    pages_goes[j].active = false;
                pages_goes[state].active = true;
            }
        }
        current_state = state;        
    }
}

