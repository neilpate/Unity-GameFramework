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

    // Start is called before the first frame update
    void Start()
    {
        controller.StateChangeEvent += OnGameStateChange;

        timer = HUD.GetComponentInChildren<TextMeshProUGUI>();
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
        timer.text = $"{controller.ElapsedTime:F2} s";
    }
}
