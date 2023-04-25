using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float exitTimer;
    public override void OnStart(Enemy enemy) {
        exitTimer = 0f;
        enemy.currentTarget = enemy.enemyInRange[0];
        enemy.animator.SetBool("IsAttack", true);
        enemy.transform.LookAt(enemy.currentTarget, Vector3.up);
    }
    public override void OnUpdate(Enemy enemy) {
        exitTimer += Time.deltaTime;
        if(exitTimer > 0.7f){
            enemy.SwitchState(enemy.enemyIdleState);
        }
    }
    public override void OnExit(Enemy enemy) {
        enemy.animator.SetBool("IsAttack", false);
    }

}
