using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public StateManager stateManagerRef;
    public Target targetState;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }
    // Still haven't finished this class yet. Waiting to implement attack feature.
    public override State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy)
    {
        Debug.Log("You should lose now");
        stateManagerRef.enabled = false;
        OnTriggerEnter();

        return targetState;
    }
    void OnTriggerEnter()
    {
        gameManager.LostLevel();
    }
}
