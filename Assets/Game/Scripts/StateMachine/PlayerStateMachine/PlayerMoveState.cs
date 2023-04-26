using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState<Player>
{
    public void OnStart(Player player) {
        Debug.Log(player.animator.GetBool("IsAttack"));
        player.animator.SetBool("IsIdle", false);
    }
    public void OnUpdate(Player player) {
        if(Vector3.Distance(player.moveDirection, Vector3.zero) < 0.1f){
            player.SwitchState(player.playerIdleState);
        }
    }
    public void OnExit(Player player) {

    }
}
