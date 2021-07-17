using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float LifeTimeInSeconds;
    float aliveTimeInSeconds;


    // Start is called before the first frame update
    void Start()
    {
        aliveTimeInSeconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTimeInSeconds += Time.deltaTime;

        if (aliveTimeInSeconds > LifeTimeInSeconds)
        {
            Destroy(gameObject);
        }
    }
}
