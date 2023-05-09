using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState<Player>
{
    private float exitTimer;
    private bool isThrowing;
    public void OnStart(Player player) {
        exitTimer = 0f;
        isThrowing = false;
        player.animator.SetBool("IsAttack", true);
        player.currentTarget = player.enemyInRange[0];
        player.transform.LookAt(player.currentTarget, Vector3.up);
    }
    public void OnUpdate(Player player) {
        exitTimer += Time.deltaTime;
        if(exitTimer > 0.25f && !isThrowing){
            isThrowing = true;
            player.SpawnWeapon();
        }
        if(exitTimer > 1f){
            player.SwitchState(player.playerIdleState);
            return;
        }
        if(Vector3.Distance(player.moveDirection, Vector3.zero) > 0.1f){
            player.SwitchState(player.playerMoveState);
            return;
        }
    }
    public void OnExit(Player player) {
        player.animator.SetBool("IsAttack", false);
    }
}
