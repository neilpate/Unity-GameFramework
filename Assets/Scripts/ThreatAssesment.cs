using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatAssesment : MonoBehaviour
{
    [SerializeField]
    GameObject Head;

    [SerializeField]
    GameObject Exclamations;

    [SerializeField]
    Color UnthreatenedColour;

    [SerializeField]
    Color ThreatenedColour;

    private bool threatened;

    private Material headMaterial;

    // Start is called before the first frame update
    void Start()
    {
        threatened = false;
        headMaterial = Head.GetComponent<Renderer>().material;

        //var comps = Head.GetComponents

        if (!headMaterial)
        {
            Debug.Log("Head material not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTheatened();
    }

    private void UpdateTheatened()
    {
        if (headMaterial)
        {
            if (threatened)
            {
                headMaterial.color = ThreatenedColour;
            }
            else
            {
                headMaterial.color = UnthreatenedColour;
            }
        }

        if (Exclamations)
        {
            Exclamations.SetActive(threatened);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //  Debug.Log("Entered");
        threatened = true;
    }

    private void OnTriggerExit(Collider other)
    {
        threatened = false;

        //Debug.Log("Exited");
    }


}
