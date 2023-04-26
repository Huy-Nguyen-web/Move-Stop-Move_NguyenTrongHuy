using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 moveDirection;
    public Transform currentTarget;
    public GameObject hammerPrefab;
    public List<Transform> enemyInRange;
    // public Collider hitArea;
    public Animator animator;
    public LayerMask characterLayer;
    public float hitRange = 10f;
    public bool isThrowing = false;
    public virtual void Attack(){
        transform.LookAt(currentTarget.transform.position);
        if(!isThrowing){
            isThrowing = true;
            // TODO: Rewrite the GameObject to Weapon
        }
    }
    public void UpdateEnemyList(){
        Collider[] foundCharacter = Physics.OverlapSphere(transform.position, hitRange/2, characterLayer);
        enemyInRange.Clear();
        for(int i = 0; i < foundCharacter.Length; i++){
           if(foundCharacter[i].transform == transform) continue;
           Transform enemy = foundCharacter[i].transform;
           enemyInRange.Add(enemy);
        }
    }
    // public void SwitchState(IState<Character> state){
    //     if(currentState != null){
    //         currentState.OnExit(this);
    //     }
    //     currentState = state;
    //     if(currentState != null){
    //         currentState.OnStart(this);
    //     }
    // }
}
