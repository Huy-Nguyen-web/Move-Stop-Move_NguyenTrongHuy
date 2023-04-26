using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState<Player>
{
    public void OnStart(Player player) {
        player.animator.SetBool("IsIdle", true);
        Debug.Log("Player on idle");
    }
    public void OnUpdate(Player player) {
        player.UpdateEnemyList();
        if(Vector3.Distance(player.moveDirection, Vector3.zero) > 0.1f){
            player.SwitchState(player.playerMoveState);
        }
        if(player.enemyInRange.Count > 0){
            player.SwitchState(player.playerAttackState);
        }
    }
    
    public void OnExit(Player player) {

    }

}
