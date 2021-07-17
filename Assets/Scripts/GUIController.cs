using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField]
    GameController controller;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject HUD;

    [SerializeField]
    GameObject gameOver;

    TextMeshProUGUI timer;

    TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        controller.StateChangeEvent += OnGameStateChange;

        var timerGameObject = HUD.transform.Find("Timer");
        timer = timerGameObject.GetComponent<TextMeshProUGUI>();

        var scoreGameObject = HUD.transform.Find("Score");
        score = scoreGameObject.GetComponent<TextMeshProUGUI>();

    }

    private void OnGameStateChange(GameController.State state)
    {
        switch (state)
        {
            case (GameController.State.NotStarted):
                mainMenu.SetActive(true);
                HUD.SetActive(false);
                gameOver.SetActive(false);
                break;

            case (GameController.State.Starting):
                mainMenu.SetActive(false);
                HUD.SetActive(false);
                gameOver.SetActive(false);
                break;

            case (GameController.State.Running):
                HUD.SetActive(true);
                break;

            case (GameController.State.GameOver):
                HUD.SetActive(false);
                gameOver.SetActive(true);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Monospace the elapsed time
        timer.text = $"<mspace=0.5em>{controller.ElapsedTime:F2} s";

        score.text = $"<mspace=0.5em>Score: {controller.Score}";
    }
}
