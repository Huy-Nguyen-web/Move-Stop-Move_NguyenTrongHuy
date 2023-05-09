using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Weapon : GameUnit
{
    private WeaponData currentWeapon;
    private float speed;
    private float travelExtraRange;
    private GameObject weaponModel;
    private GameObject[] weaponModels;
    [SerializeField] private Rigidbody rb;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float maxTravelDistance;
    [HideInInspector] public Character currentCharacter;
    private void Start() {
        
    }
    private void Update() {
        if(currentWeapon != null && currentWeapon.canRotate){
            transform.Rotate(0, -1000 * Time.deltaTime, 0);
        }
        if(Vector3.Distance(transform.position, startPosition) > maxTravelDistance){
            if(currentWeapon.canReturn){
                BoomerangBehaviour(currentCharacter);
            }else{
                OnDespawn();
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == currentCharacter.gameObject) return;
        OnDespawn();
    }

    public override void OnInit(){

    }

    public override void OnDespawn(){
        if(weaponModel != null){
            Destroy(weaponModel);
        }
        SimplePool.Despawn(this);
    }
    public void OnInit(Character character){
        UpdateWeapon(character);
        transform.position = character.attackPosition.position;
        transform.rotation = character.transform.rotation;

        startPosition = character.transform.position;
        moveDirection = character.transform.forward;
        currentCharacter = character;
        maxTravelDistance = character.hitRange/2 + travelExtraRange + 1.0f;

        rb.velocity = moveDirection * speed;
    }
    private void UpdateWeapon(Character character){
        currentWeapon = character.weaponType;
        weaponModel = Instantiate(currentWeapon.weaponModel, transform);
        weaponModel.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        travelExtraRange = currentWeapon.weaponExtraRange;
        speed = currentWeapon.weaponSpeed;
    }
    private void BoomerangBehaviour(Character character){
        transform.position = Vector3.MoveTowards(transform.position, character.transform.position, 10 * Time.deltaTime);
        if(Vector3.Distance(transform.position, character.transform.position) <= 0.3f) OnDespawn();
    }
}
