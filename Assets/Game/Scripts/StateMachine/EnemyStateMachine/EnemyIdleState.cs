using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<Enemy>
{
    private float changeStateTimer;
    private float attackTimer;
    private float timeEnd;
    public void OnStart(Enemy enemy) {
        changeStateTimer = 0.0f;
        attackTimer = 0.0f;
        timeEnd = Random.Range(0.1f, 2.0f);
        enemy.navMeshAgent.isStopped = true;
        // Change animation to idle for enemy
        enemy.animator.SetBool("IsIdle", true);
    }
    public void OnUpdate(Enemy enemy) {
        if(enemy.CheckEnemyInRange()){
            attackTimer += Time.deltaTime;
            if(attackTimer >= 0.5f)enemy.SwitchState(enemy.enemyAttackState);
            return;
        }
        changeStateTimer += Time.deltaTime;
        if(changeStateTimer >= timeEnd){
            enemy.SwitchState(enemy.enemyMoveState);
            return;
        }
    }
    public void OnExit(Enemy enemy) {

    }
}
