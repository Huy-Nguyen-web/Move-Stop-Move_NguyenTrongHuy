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
    public EnemyDieState enemyDieState = new EnemyDieState();
    public NavMeshAgent navMeshAgent;
    private Action<GameObject> _killAction;
    public override void OnInit()
    {
        base.OnInit();
        Vector3 randomPoint;
        if(RandomPoint(Vector3.zero, 40.0f, out randomPoint)){
            transform.position = randomPoint;
        }
        characterCollider.enabled = true;
        navMeshAgent.enabled = true;
        animator.SetBool("IsDead", false);
        currentState = enemyIdleState;
        currentState.OnStart(this);
        //TEST
        attackPosition = transform;
    }
    // private void Start() {
    // }
    private void Update() {
        currentState.OnUpdate(this);
    }
    public void SwitchState(IState<Enemy> newState){
        if(currentState != null){
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState != null){
            currentState.OnStart(this);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(weapon == null || (weapon.gameObject != other.gameObject)){
            SwitchState(enemyDieState);
        }
    }
        public float range = 10.0f;

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
