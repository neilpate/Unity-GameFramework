using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWheels : MonoBehaviour
{
    [SerializeField]
    List<GameObject> wheels;

    [SerializeField]
    float rotationMultiplier = 10;

    public float speed;

    public Transform root;

    Vector3 previousPostion;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaPosition = root.position - previousPostion;

        previousPostion = transform.position;

        var distanceTravelledInLastFrame = deltaPosition.magnitude;

        speed = distanceTravelledInLastFrame * Time.deltaTime;

        //Rotation the wheel around just the x axis
        var rotation = new Vector3(0, 0, speed * rotationMultiplier);

        foreach (var wheel in wheels)
        {
        //    wheel.transform.RotateAroundLocal(new Vector3(1, 0, 0), speed * rotationMultiplier);
         //   wheel.transform.Rotate(new Vector3(1, 0, 0), speed * rotationMultiplier);

            wheel.transform.Rotate(rotation, Space.Self);
        }

    }
}
