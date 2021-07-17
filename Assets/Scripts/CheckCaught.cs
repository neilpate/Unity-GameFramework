using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCaught : MonoBehaviour
{
    public delegate void Caught();
    public event Caught CaughtEvent;

    int enemyLayer;

    void Start()
    {
        //Do the lookup once
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == enemyLayer)
        {
            CaughtEvent();
        }

    }
}
