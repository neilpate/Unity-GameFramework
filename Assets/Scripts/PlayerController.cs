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
    InputAction move;


    [ShowNonSerializedField]
    Rotatation rotation;

    [ShowNonSerializedField]
    Movement movement;

    enum Rotatation
    {
        None,
        Clockwise,
        CounterClockwise
    }

    enum Movement
    {
        None,
        Forwards,
        Backwards
    }

    // Start is called before the first frame update
    void Start()
    {
        var inputActionsMap = InputActions.FindActionMap("Player");

        rotate = inputActionsMap.FindAction("Rotate");
        rotate.performed += OnRotate;

        move = inputActionsMap.FindAction("Move");
        move.performed += OnMove;

    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();

        if (value == 0)
        {
            movement = Movement.None;
        }

        if (value >  0)
        {
            movement = Movement.Forwards;
        }

        if (value <  0)
        {
            movement = Movement.Backwards;
        }


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
            var rotationAngleDelta = 0f;
            switch (rotation)
            {
                case Rotatation.None:
                    break;

                case Rotatation.Clockwise:
                    rotationAngleDelta = PlayerSettings.RotationSpeedDegreesPerSecond * Time.deltaTime;
                    break;

                case Rotatation.CounterClockwise:
                    rotationAngleDelta = -PlayerSettings.RotationSpeedDegreesPerSecond * Time.deltaTime;
                    break;
            }

            //Rotate the Player around the y-axis
            Player.transform.Rotate(0, rotationAngleDelta, 0, Space.Self);

            var movementDelta = 0f;
            switch (movement)
            {
                case Movement.None:
                    break;

                case Movement.Forwards:
                    movementDelta = PlayerSettings.ForwardSpeedMetersPerSecond * Time.deltaTime;
                    break;

                case Movement.Backwards:
                    movementDelta = -PlayerSettings.ForwardSpeedMetersPerSecond * Time.deltaTime;
                    break;
            }

            //Move the Player in the z axis
            Player.transform.Translate(new Vector3(0, 0, movementDelta), Space.Self);




        }


    }
}
