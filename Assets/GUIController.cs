using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField]
    GameController controller;

    [SerializeField]
    GameObject mainMenu;

 //   event GameController.StateChange gameStateChange;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //  gameStateChange += OnGameStateChange();
        GameController.StateChangeEvent += OnGameStateChange;

    }

    private void OnGameStateChange(GameController.State state)
    {
        switch (state)
        {
            case (GameController.State.NotStarted):
                mainMenu.SetActive(true);
                break;

            case (GameController.State.Starting):
                mainMenu.SetActive(false);
                break;

            case (GameController.State.Running):
                break;

            case (GameController.State.GameOver):
                mainMenu.SetActive(true);
                break;

        }
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
