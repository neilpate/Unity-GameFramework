using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navMeshAgent;

    public GameController gameController;

    public Transform target;

    Transform root;

    private void Awake()
    {
        root = GetComponentInParent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!gameController.GameOver)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            //Game is over, immediately stop the enemy from moving imm
            navMeshAgent.isStopped = true;
        }
    }
}
