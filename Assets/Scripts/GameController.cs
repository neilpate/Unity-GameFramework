using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;


public class GameController : MonoBehaviour
{
    [SerializeField]
    EnvironmentSettings EnvironmentSettings;

    [SerializeField]
    GameObject Environment;

    [SerializeField]
    PlayerController PlayerController;
    
    [SerializeField]
    GameObject PlayerTemplate;

    [SerializeField]
    GameObject EnemyTemplate;

    [SerializeField]
    AISettings enemyAISettings;

    public bool GameOver;

    GameObject player;
    GameObject enemy;

    // [ShowNonSerializedField]
    public State currentState;

    [ShowNonSerializedField]
    State nextState;

    [ShowNonSerializedField]
    State previousState;

    public delegate void StateChange(State state);
    public event StateChange StateChangeEvent;

    public float ElapsedTime;

    public enum State
    {
        NotStarted,
        Starting,
        Running,
        GameOver
    }


    void UpdateNotStartedState()
    {
        if (previousState != State.NotStarted)
        {
            EnterNotStartedState();
        }

        //Nothing to do here, it is controlled by the button click
    }

    void EnterNotStartedState()
    {
        StateChangeEvent(State.NotStarted);

        GameOver = false;
    }
    public void OnStartGameClick()
    {
        ExitNotStartedState();
    }

    void ExitNotStartedState()
    {
        nextState = State.Starting;
    }

    void UpdateStartingState()
    {
        if (previousState != State.Starting)
        {
            //This will be true the first time through here
            EnterStartingState();
        }

        //Nothing to do here, the game immediately transitions to the Exit from the Enter
    }

    void EnterStartingState()
    {
        StateChangeEvent(State.Starting);

        SpawnPlayer();
        SpawnEnemies();

        ElapsedTime = 0;

        ExitStartingState();

    }

    void ExitStartingState()
    {
        nextState = State.Running;
    }



    void UpdateRunningState()
    {
       
        if (previousState != State.Running)
        {
            EnterRunningState();
        }
        
        //This is the main game loop when the game is running
        if (GameOver)
        {
            ExitRunningState();
        }

        ElapsedTime += Time.deltaTime;
    }
    void EnterRunningState()
    {
        StateChangeEvent(State.Running);
    }

    void ExitRunningState()
    {
        nextState = State.GameOver;
    }


    void UpdateGameOverState()
    {
        if (previousState != State.GameOver)
        {
            //This will be true the first time through here
            EnterGameOverState();
        }

        //Exit is determined by the button click
    }


    void EnterGameOverState()
    {
        StateChangeEvent(State.GameOver);

        DestroyPlayer();
        DestroyEnemies();
    }

    public void OnGameOverPlayAgainClick()
    {
        ExitGameOverState(true);
    }

    public void OnGameOverQuitClick()
    {
        ExitGameOverState(false);
    }



    void ExitGameOverState(bool playAgain)
    {
        GameOver = false;

        if (playAgain)
        {
            nextState = State.Starting;

        }
        else
        {
            nextState = State.NotStarted;
        }
    }

    void OnPlayerCaught()
    {
        GameOver = true;
    }


    public void Start()
    {
        currentState = State.GameOver;
        nextState = State.NotStarted;

        SetupWorld();
    }


    // Update is called once per frame
    void Update()
    {
        //Simple state machine

        previousState = currentState;
        currentState = nextState;

        switch (currentState)
        {
            case State.NotStarted:
                UpdateNotStartedState();
                break;

            case State.Starting:
                UpdateStartingState();
                break;

            case State.Running:
                UpdateRunningState();
                break;

            case State.GameOver:
                UpdateGameOverState();
                break;
        }
    }

    void SetupWorld()
    {
        var floor = Environment.transform.Find("Floor").gameObject;
        floor.transform.localScale = new Vector3(EnvironmentSettings.xSize, floor.transform.localScale.y, EnvironmentSettings.zSize);


      //  Environment.GetComponen
    }


    void SpawnPlayer()
    {
        var randomXPosition =  UnityEngine.Random.Range( -EnvironmentSettings.xSize/2, EnvironmentSettings.xSize / 2);
        var randomZPosition =  UnityEngine.Random.Range(-EnvironmentSettings.zSize/2, EnvironmentSettings.zSize / 2);
        var initialPosition = new Vector3(randomXPosition, 2, randomZPosition);
        Debug.Log($"Spawning Player at x:{randomXPosition:F0}, z:{randomZPosition:F0}");

        player = Instantiate(PlayerTemplate, initialPosition, Quaternion.identity);
        
        player.name = "Player";

        var checkCaught = player.GetComponentInChildren<CheckCaught>();
        checkCaught.gameController = this;

        checkCaught.CaughtEvent += OnPlayerCaught;

        PlayerController.Player = player;
        
    }

    void SpawnEnemies()
    {
        var randomXPosition = UnityEngine.Random.Range(-EnvironmentSettings.xSize / 2, EnvironmentSettings.xSize / 2);
        var randomZPosition = UnityEngine.Random.Range(-EnvironmentSettings.zSize / 2, EnvironmentSettings.zSize / 2);
        var initialPosition = new Vector3(randomXPosition, 2, randomZPosition);
        Debug.Log($"Spawning Enemy at x:{randomXPosition:F0}, z:{randomZPosition:F0}");

        enemy = Instantiate(EnemyTemplate, initialPosition, Quaternion.identity);
        
        enemy.name = "Enemy";

        var enemyAI = enemy.GetComponentInChildren<EnemyAI>();
        enemyAI.gameController = this;
        enemyAI.target = player.transform;

        var navMeshAgent = enemy.GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.speed = enemyAISettings.Speed;

    }

    void DestroyPlayer()
    {
        Destroy(player);
    }

    void DestroyEnemies()
    {
        Destroy(enemy);
    }

}
