using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class GUIStartPage : MonoBehaviour {
    public GameObject start_button_go;
    public GameObject exit_button_go;
    GameController game_controller;

    void Start()
    {
        Button start_button = start_button_go.GetComponent<Button>();
        start_button.onClick.AddListener( OnStartButtonClick );

        Button exit_button = exit_button_go.GetComponent<Button>();
        exit_button.onClick.AddListener( OnExitButtonClick );

        GameObject game_controller_go = GameObject.Find( "GameController" );
        game_controller = game_controller_go.GetComponent<GameController>();
    }
    
    void OnStartButtonClick()
    {
        game_controller.SetState( GameController.STATE_GAMING );
    }

    void OnExitButtonClick()
    {
        Application.Quit(); 
    }
}
