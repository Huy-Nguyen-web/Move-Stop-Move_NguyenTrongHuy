using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : IState<Enemy>
{
    public void OnStart(Enemy enemy) {
        enemy.navMeshAgent.SetDestination(GetRandomPosition(enemy));
        enemy.navMeshAgent.isStopped = false;
        enemy.animator.SetBool("IsIdle", false);
    }
    public void OnUpdate(Enemy enemy) {
        if(enemy.CheckEnemyInRange()){
            enemy.SwitchState(enemy.enemyIdleState);
            return;
        }
        if(enemy.navMeshAgent.remainingDistance < 0.2f){
            enemy.SwitchState(enemy.enemyIdleState);
            return;
        }
    }
    public void OnExit(Enemy enemy) {

    }
    private Vector3 GetRandomPosition(Enemy enemy){
        float radius = 15f;
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        Vector3 randomPosition = new Vector3(randomPoint.x, 0f, randomPoint.y) * radius;
        randomPosition += enemy.transform.position;
        return randomPosition;
    }
}
