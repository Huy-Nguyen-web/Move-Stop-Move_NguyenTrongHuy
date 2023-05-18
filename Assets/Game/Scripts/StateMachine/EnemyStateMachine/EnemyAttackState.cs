using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<Enemy>
{
    private float exitTimer;
    private bool isThrowing;
    public void OnStart(Enemy enemy) {
        exitTimer = 0f;
        isThrowing = false;
        enemy.UpdateEnemyList();
        enemy.currentTarget = enemy.enemyInRange[0];
        enemy.animator.SetBool(Constant.ANIM_ATTACK, true);
        
        //TODO: quy tac dong goi
        //enemy.ChangeAnim("IsAttack");
        //TODO: cache transform
        enemy.TF.LookAt(enemy.currentTarget, Vector3.up);
    }
    public void OnUpdate(Enemy enemy) {
        exitTimer += Time.deltaTime;
        if(exitTimer > 0.25f && !isThrowing){
            isThrowing = true;
            enemy.SpawnWeapon();
        }
        if(exitTimer > 1.5f){
            enemy.SwitchState(enemy.enemyIdleState);
            return;
        }
    }
    public void OnExit(Enemy enemy) {
        enemy.animator.SetBool("IsAttack", false);
        enemy.onHandWeapon.SetActive(true);
        enemy.currentTarget = null;
    }
}
