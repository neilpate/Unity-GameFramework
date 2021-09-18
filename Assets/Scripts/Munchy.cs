using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munchy
{
    MunchySettings settings;

    public Action<MunchySettings> Eaten;

    GameObject gameObject;

    public Munchy(GameObject munchyTemplate, MunchySettings settings, GameObject munchyContainer)
    {
        var randomXPosition = UnityEngine.Random.Range(-25 / 2 + 1, 25 / 2 - 1);
        var randomZPosition = UnityEngine.Random.Range(-25 / 2 + 1, 25 / 2 - 1);
        var initialPosition = new Vector3(randomXPosition, 2, randomZPosition);

        gameObject = UnityEngine.Object.Instantiate(munchyTemplate, initialPosition, Quaternion.identity);

        gameObject.transform.parent = munchyContainer.transform;

        gameObject.GetComponent<AutoDestroy>().LifeTimeInSeconds = settings.LifeTimeInSeconds;

        this.settings = settings;

        var shader = Shader.Find("Shader Graphs/Border");
        var material = new Material(shader);
        material.SetColor("Colour", settings.Color);

        gameObject.GetComponentInChildren<MeshRenderer>().material = material;

        gameObject.GetComponent<MunchyEatenDetection>().munchy = this;

    
    }

    public void EatenDetected()
    {
        //The MunchEaten MonoBehaviour will call this method
        //So here we can raise the event that 

        Eaten(settings);

        UnityEngine.Object.Destroy(gameObject);
    }






}
