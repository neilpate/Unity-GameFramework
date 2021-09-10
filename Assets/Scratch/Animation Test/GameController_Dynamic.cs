using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Dynamic : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerTemplate;

    GameObject player;

    public bool Walking;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(PlayerTemplate, new Vector3(0, 0, 0), Quaternion.identity);

        animator = player.GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.Log("Animator not found, disabling GameController");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", Walking);
    }
}
