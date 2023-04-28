using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<Enemy>
{
    private float exitTimer;
    public void OnStart(Enemy enemy) {
        exitTimer = 0f;
        enemy.currentTarget = enemy.enemyInRange[0];
        enemy.animator.SetBool("IsAttack", true);
        enemy.transform.LookAt(enemy.currentTarget, Vector3.up);
    }
    public void OnUpdate(Enemy enemy) {
        exitTimer += Time.deltaTime;
        // Cache.GetWFS(0.7f);
        // enemy.SwitchState(enemy.enemyIdleState);
        if(exitTimer > 0.7f){
            enemy.SwitchState(enemy.enemyIdleState);
        }
    }
    public void OnExit(Enemy enemy) {
        enemy.animator.SetBool("IsAttack", false);
    }
}
