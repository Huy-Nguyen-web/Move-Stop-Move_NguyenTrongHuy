using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState<Player>
{
    public void OnStart(Player player) {
        player.animator.SetBool("IsIdle", true);
    }
    public void OnUpdate(Player player) {
        player.UpdateEnemyList();
        if(Vector3.Distance(player.moveDirection, Vector3.zero) > 0.1f){
            player.SwitchState(player.playerMoveState);
            return;
        }
        if(player.enemyInRange.Count > 0){
            player.SwitchState(player.playerAttackState);
            return;
        }
    }
    public void OnExit(Player player) {

    }

}
