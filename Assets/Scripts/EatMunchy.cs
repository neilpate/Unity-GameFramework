using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMunchy : MonoBehaviour
{
    [SerializeField]
    AudioSource AudioSource;
    
    public delegate void Munched(int value);
    public event Munched MunchedEvent;

    int munchyLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Do the lookup once
        munchyLayer = LayerMask.NameToLayer("Munchy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == munchyLayer)
        {
            AudioSource.Play();
            
            MunchedEvent(100);
            
            //It would be nice to trigger some kind of destroy animation or sound
            Destroy(other.gameObject);
        }


    }
}
