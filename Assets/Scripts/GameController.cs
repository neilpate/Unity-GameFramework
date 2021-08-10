using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;


public class GameController : MonoBehaviour
{
    [SerializeField]
    GameSettings GameSettings;

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
    GameObject MunchyTemplate;

    [SerializeField]
    AISettings AISettings;

    [SerializeField]
    MunchySettings[] AllPossibleMunchies;

    public bool GameOver;

    GameObject player;

    List<GameObject> enemies;

    GameObject munchyContainer;

    public State currentState;

    [ShowNonSerializedField]
    State nextState;

    [ShowNonSerializedField]
    State previousState;

    [ShowNonSerializedField]
    int numberOfMunchies;

    [SerializeField]
    int NewEnemyScore;

    public delegate void StateChange(State state);
    public event StateChange StateChangeEvent;

    public float ElapsedTime;

    public int Score;

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

        CheckPlayerFallenOffWorld();
    }

    private void CheckPlayerFallenOffWorld()
    {
        if (player.transform.position.y < -10)
        {
            GameOver = true;
        }
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
        //  ExitGameOverState(false);
        Application.Quit();

#if UNITY_EDITOR
          EditorApplication.isPlaying = false;
#endif


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
        if (!GameSettings.GodMode)
        {
            GameOver = true;
        }
    }


    public void Start()
    {
        currentState = State.GameOver;
        nextState = State.NotStarted;

        SetupWorld();

        munchyContainer = new GameObject();
        munchyContainer.name = "Munchy Container";

        enemies = new List<GameObject>();

    }

    private void OnMunchedEvent(int value)
    {
        Score += value;

        if (Score > NewEnemyScore)
        {
            SpawnEnemies();

        }
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
                SpawnMunchies();
                break;

            case State.GameOver:
                UpdateGameOverState();
                break;
        }
    }

    private void SpawnMunchies()
    {
        foreach (var munchy in AllPossibleMunchies)
        {
            var randomPercentage = Random.value * 100;
            var spawnChance = munchy.LiklihoodOfSpawningPercentPerSecond * Time.deltaTime;

            if (randomPercentage < spawnChance)
            {
                var randomXPosition = UnityEngine.Random.Range(-EnvironmentSettings.xSize / 2 + 1, EnvironmentSettings.xSize / 2 - 1);
                var randomZPosition = UnityEngine.Random.Range(-EnvironmentSettings.zSize / 2 + 1, EnvironmentSettings.zSize / 2 - 1);
                var initialPosition = new Vector3(randomXPosition, 2, randomZPosition);
                //       Debug.Log($"Spawning Munchy at x:{randomXPosition:F0}, z:{randomZPosition:F0}");

                var newMunchy = Instantiate(MunchyTemplate, initialPosition, Quaternion.identity);

                newMunchy.transform.parent = munchyContainer.transform;

                newMunchy.GetComponent<AutoDestroy>().LifeTimeInSeconds = munchy.LifeTimeInSeconds;



            }
        }

        //Update total number of Munchies
        numberOfMunchies = munchyContainer.transform.childCount;
    }

    private void OnMunchyMunched(int value)
    {
        throw new System.NotImplementedException();
    }

    void SetupWorld()
    {
        var floor = Environment.transform.Find("Floor").gameObject;
        floor.transform.localScale = new Vector3(EnvironmentSettings.xSize, floor.transform.localScale.y, EnvironmentSettings.zSize);


    }

    void SpawnPlayer()
    {
        var randomXPosition = UnityEngine.Random.Range(-EnvironmentSettings.xSize / 2 + 1, EnvironmentSettings.xSize / 2 - 1);
        var randomZPosition = UnityEngine.Random.Range(-EnvironmentSettings.zSize / 2 + 1, EnvironmentSettings.zSize / 2 - 1);
        var initialPosition = new Vector3(randomXPosition, 2, randomZPosition);
        //     Debug.Log($"Spawning Player at x:{randomXPosition:F0}, z:{randomZPosition:F0}");

        player = Instantiate(PlayerTemplate, initialPosition, Quaternion.identity);

        player.name = "Player";

        var checkCaught = player.GetComponentInChildren<CheckCaught>();

        checkCaught.CaughtEvent += OnPlayerCaught;

        PlayerController.Player = player;

        player.GetComponentInChildren<EatMunchy>().MunchedEvent += OnMunchedEvent;

    }

    void SpawnEnemies()
    {
        var randomXPosition = UnityEngine.Random.Range(-EnvironmentSettings.xSize / 2 + 1, EnvironmentSettings.xSize / 2 - 1);
        var randomZPosition = UnityEngine.Random.Range(-EnvironmentSettings.zSize / 2 + 1, EnvironmentSettings.zSize / 2 - 1);
        var initialPosition = new Vector3(randomXPosition, 2, randomZPosition);
        //  Debug.Log($"Spawning Enemy at x:{randomXPosition:F0}, z:{randomZPosition:F0}");

        var enemy = Instantiate(EnemyTemplate, initialPosition, Quaternion.identity);

        enemies.Add(enemy);

        enemy.name = "Enemy";

        var enemyAI = enemy.GetComponentInChildren<EnemyAI>();
        enemyAI.gameController = this;
        enemyAI.target = player.transform;

        var navMeshAgent = enemy.GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.speed = AISettings.Speed;

    }

    void DestroyPlayer()
    {
        Destroy(player);
    }

    void DestroyEnemies()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}
