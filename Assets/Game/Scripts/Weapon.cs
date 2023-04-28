using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Weapon : GameUnit
{
    [SerializeField] private Rigidbody rb;
    private float speed = 10.0f;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float maxTravelDistance;
    [HideInInspector] public Character currentCharacter;
    private void Update() {
        // if(Vector3.Distance(startPosition, transform.position) >= maxTravelDistance){
        //     OnDespawn();
        // }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == currentCharacter.gameObject) return;
        OnDespawn();
    }

    public override void OnInit(){

    }

    public override void OnDespawn(){
        SimplePool.Despawn(this);
    }
    public virtual void OnInit(Character character){
        transform.position = character.AttackPosition.position;
        startPosition = character.transform.position;
        moveDirection = character.transform.forward;
        currentCharacter = character;
        maxTravelDistance = character.hitRange/2;

        rb.velocity = moveDirection * speed;
    }
}
