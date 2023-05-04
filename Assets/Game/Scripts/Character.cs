using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public Transform attackPosition;
    public Vector3 moveDirection;
    public Transform currentTarget;
    public GameObject hammerPrefab;
    public List<Transform> enemyInRange;
    public Collider characterCollider;
    public Animator animator;
    public LayerMask characterLayer;
    public float hitRange = 10f;
    public bool isThrowing = false;
    public Weapon weapon;
    
    public virtual void Attack(){
        transform.LookAt(currentTarget.transform.position);
        if(!isThrowing){
            isThrowing = true;
        }
    }

    public override void OnDespawn()
    {
       SimplePool.Despawn(this);
    }

    public override void OnInit()
    {

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
    public void SpawnWeapon(){
        weapon = LevelManager.Instance.SpawnWeapon();
        weapon.OnInit(this);
    }
}
