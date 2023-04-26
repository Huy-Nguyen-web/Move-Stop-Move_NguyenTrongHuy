using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Enemy : Character 
{
    IState<Enemy> currentState;
    public EnemyIdleState enemyIdleState = new EnemyIdleState();
    public EnemyAttackState enemyAttackState = new EnemyAttackState();
    public EnemyMoveState enemyMoveState = new EnemyMoveState();
    public NavMeshAgent navMeshAgent;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = enemyIdleState;
        currentState.OnStart(this);
        // UpdateHitArea();
    }
    private void Update() {
        currentState.OnUpdate(this);
    }
    public void SwitchState(IState<Enemy> newState){
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnStart(this);
    }
}
