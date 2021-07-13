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

    [SerializeField]
    GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        controller.StateChangeEvent += OnGameStateChange;
    }

    private void OnGameStateChange(GameController.State state)
    {
        switch (state)
        {
            case (GameController.State.NotStarted):
                mainMenu.SetActive(true);
                gameOver.SetActive(false);
                break;

            case (GameController.State.Starting):
                mainMenu.SetActive(false);
                gameOver.SetActive(false);
                break;

            case (GameController.State.Running):
                break;

            case (GameController.State.GameOver):
                gameOver.SetActive(true);
                break;

        }
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
