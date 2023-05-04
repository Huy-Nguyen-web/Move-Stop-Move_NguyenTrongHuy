using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : IState<Player>
{
    private float exitTimer;
    public void OnStart(Player player){
        exitTimer = 0f;
        player.characterCollider.enabled = false;
        player.animator.SetBool("IsDead", true);
        player.controller.enabled = false;
        player.joystick.enabled = false;
    }
    public void OnUpdate(Player player){
        exitTimer += Time.deltaTime;
        if(exitTimer >= 2.0f){
            //TODO: Show the game over UI
            Debug.Log("Player die");
        }
    }
    public void OnExit(Player player){

    }
}
