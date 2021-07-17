using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCaught : MonoBehaviour
{
    public delegate void Caught();
    public event Caught CaughtEvent;

    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            CaughtEvent();
        }

    }
}
