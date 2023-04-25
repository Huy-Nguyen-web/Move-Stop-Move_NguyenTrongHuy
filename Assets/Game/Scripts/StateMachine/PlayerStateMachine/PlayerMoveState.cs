using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void OnStart(Player player) {
        Debug.Log(player.animator.GetBool("IsAttack"));
        player.animator.SetBool("IsIdle", false);
    }
    public override void OnUpdate(Player player) {
        if(Vector3.Distance(player.moveDirection, Vector3.zero) < 0.1f){
            player.SwitchState(player.playerIdleState);
        }
    }
    public override void OnExit(Player player) {

    }
}
