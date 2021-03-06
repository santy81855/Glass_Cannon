using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public State currentState;
    Camera fpsCam;
    Transform target;
    NavMeshAgent agent;
    Transform myEnemy;
    public bool isEnemyDead;

    // Start is called before the first frame update
    void Start()
    {
        // Get needed variables at start
        target = GameManager.Instance.treasure.transform;
        agent = GetComponent<NavMeshAgent>();
        myEnemy = transform;
        fpsCam = Camera.main;
        isEnemyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Run the state machine
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState(target, agent, fpsCam, myEnemy);


        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
