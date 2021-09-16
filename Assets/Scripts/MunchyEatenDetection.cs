using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The purpose of this MonoBehaviour is just to be able to capture
/// the OnTriggerEnter event. It is immediately then handed back to
/// the pure Munchy object.
/// </summary>
public class MunchyEatenDetection : MonoBehaviour
{
    int playerLayer;

    public Munchy munchy;

    // Start is called before the first frame update
    void Start()
    {
        //Do the lookup once
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            munchy.EatenDetected();
        }
    }
}
