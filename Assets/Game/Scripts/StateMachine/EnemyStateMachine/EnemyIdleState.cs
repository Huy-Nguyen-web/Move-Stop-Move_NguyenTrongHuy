using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<Enemy>
{
    private float changeStateTimer;
    private float timeEnd;
    public void OnStart(Enemy enemy) {
        changeStateTimer = 0.0f;
        timeEnd = Random.Range(0.1f, 2.0f);
        // Change animation to idle for enemy
        enemy.animator.SetBool("IsIdle", true);
        Debug.Log("change to idle state");
    }
    public void OnUpdate(Enemy enemy) {
        // After staying for 2 seconds, enemy start to move.
        enemy.UpdateEnemyList();
        changeStateTimer += Time.deltaTime;
        if(changeStateTimer >= timeEnd){
            enemy.SwitchState(enemy.enemyMoveState);
            return;
        }
        if(enemy.enemyInRange.Count > 0){
            enemy.SwitchState(enemy.enemyAttackState);
            return;
        }
    }
    public void OnExit(Enemy enemy) {

    }
}
