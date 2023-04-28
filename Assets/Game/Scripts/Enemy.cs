using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character 
{
    IState<Enemy> currentState;
    public EnemyIdleState enemyIdleState = new EnemyIdleState();
    public EnemyAttackState enemyAttackState = new EnemyAttackState();
    public EnemyMoveState enemyMoveState = new EnemyMoveState();
    public NavMeshAgent navMeshAgent;
    private Action<GameObject> _killAction;
    // public void OnInit(Action<GameObject> killAction){
    //     _killAction = killAction;
    //     transform.position += UnityEngine.Random.insideUnitSphere * 30;
    // }
    private void OnEnable() {
        transform.position += new Vector3(UnityEngine.Random.Range(-30, 30), 0f, UnityEngine.Random.Range(-30, 30));
    }
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = enemyIdleState;
        currentState.OnStart(this);
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
