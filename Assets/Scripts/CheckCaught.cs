using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCaught : MonoBehaviour
{
    public delegate void Caught();
    public event Caught CaughtEvent;


 public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        CaughtEvent();
       // gameController.GameOver = true;
    }
}
