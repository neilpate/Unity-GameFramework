using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsettingCheckerboardController : MonoBehaviour
{
    Material material;

    public float Angle;

    [SerializeField]
    float Speed;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Angle += Speed * Time.deltaTime;

        Angle = Angle % 360;
        
        material.SetFloat("Angle", Angle);
    }
}
