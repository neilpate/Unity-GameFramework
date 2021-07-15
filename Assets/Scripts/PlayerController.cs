using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerSettings PlayerSettings;

    [SerializeField]
    InputActionAsset InputActions;

    public GameObject Player;

    InputAction rotate;

    [ShowNonSerializedField]
    Rotatation rotation;

    enum Rotatation
    {
        None,
        Clockwise,
        CounterClockwise
    }

    // Start is called before the first frame update
    void Start()
    {
        var inputActionsMap = InputActions.FindActionMap("Player");

        rotate = inputActionsMap.FindAction("Rotate");
        rotate.performed += OnRotate;



    }

    private void OnRotate(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();

        if (value == 0)
        {
            rotation = Rotatation.None;
        }

        if (value > 0)
        {
            rotation = Rotatation.Clockwise;
        }

        if (value < 0)
        {
            rotation = Rotatation.CounterClockwise;
        }




    }

    // Update is called once per frame
    void Update()
    {

        //The Player reference is set by the GameController when the Player is spwawned
        //The game might not have started, only process if the Player exists
        if (Player != null)
        {


            var rangleToRotate = 0f;
            switch (rotation)
            {
                case Rotatation.None:
                    break;

                case Rotatation.Clockwise:
                    rangleToRotate = PlayerSettings.RotationSpeedDegreesPerSecond * Time.deltaTime;
                    break;

                case Rotatation.CounterClockwise:
                    rangleToRotate = -PlayerSettings.RotationSpeedDegreesPerSecond * Time.deltaTime;
                    break;


            }

            //Rotate the Player around the y-axis
            Player.transform.Rotate(0, rangleToRotate, 0, Space.Self);

        }


    }
}
