using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : IState<Enemy>
{
    private float exitTimer;
    private bool finishDieAnimation;
    public void OnStart(Enemy enemy){
        exitTimer = 0f;
        finishDieAnimation = false;
        enemy.characterCollider.enabled = false;
        enemy.navMeshAgent.enabled = false;
        enemy.animator.SetBool("IsDead", true);
    }
    public void OnUpdate(Enemy enemy){
        exitTimer += Time.deltaTime;
        if(exitTimer >= 2.0f && !finishDieAnimation){
            finishDieAnimation = true;
            enemy.OnDespawn();
            LevelManager.Instance.RespawnEnemy();
        }
    }
    public void OnExit(Enemy enemy){

    }

}
