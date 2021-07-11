using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerTemplate;

    [SerializeField]
    GameObject EnemyTemplate;

    GameObject player;
    GameObject enemy;

    [ShowNonSerializedField]
    State currentState;

    public State nextState;

    public delegate void StateChange(State state);
    public static event StateChange StateChangeEvent;



    public enum State
    {
        NotStarted,
        Starting,
        Running,
        GameOver
    }
    
    // Start is called before the first frame update
    void Start()
    {
     

    }



    public void StartGame()
    {
        nextState = State.Starting;
    }


    public bool GameJustEnded()
    {
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        //Simple state machine
        
        switch (currentState)
        {
            case State.NotStarted:

                if (nextState == State.Starting)
                {
                    currentState = State.Running;
                    StateChangeEvent(State.Running);
                
                }
                break;

              //  if (state == State.Starting)

            case State.Running:

                if (GameJustEnded())
                {
                    currentState = State.GameOver;
                    StateChangeEvent(State.GameOver);
                }

                GameRunningUpdate();

                break;

            case State.GameOver:
                break;


        }
    }

    void GameRunningUpdate()
    {
        //This is primary game update method
    }

    void SpawnPlayer()
    {
        player = Instantiate(PlayerTemplate, new Vector3(0, 2, 0), Quaternion.identity);
        player.name = "Player";
    }

    void SpawnEnemies()
    {
        enemy = Instantiate(EnemyTemplate, new Vector3(2, 2, 0), Quaternion.identity);
        enemy.name = "Enemy";
    }


}
