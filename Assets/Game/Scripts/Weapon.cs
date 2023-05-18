using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Weapon : GameUnit
{
    private WeaponData currentWeapon;
    private float speed;
    private float travelExtraRange;
    private bool isReturning;
    private GameObject weaponModel;
    private GameObject[] weaponModels;
    private Vector3 throwingPosition;
    [SerializeField] private Rigidbody rb;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float maxTravelDistance;
    [HideInInspector] public Character currentCharacter;
    private void Start() {

    }
    public void OnInit(Character character){
        UpdateWeapon(character);
        transform.position = character.attackPosition.position;
        transform.rotation = character.transform.rotation;
        throwingPosition = transform.position;

        isReturning = false;

        startPosition = character.transform.position;
        moveDirection = character.transform.forward;
        currentCharacter = character;
        currentCharacter.isDead = false;
        maxTravelDistance = character.hitRange/2 + travelExtraRange + 1.0f;

        rb.velocity = moveDirection * speed;
    }
    private void Update() {
        // Update visual
        if(currentWeapon != null && currentWeapon.canRotate){
            transform.Rotate(0, -1000 * Time.deltaTime, 0);
        }

        if(!isReturning){
            GoForward();
        }else{
            GoBack(throwingPosition);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == currentCharacter.gameObject) return;
        currentCharacter.AddPoint(2);
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
    private void UpdateWeapon(Character character){
        currentWeapon = character.weaponType;
        weaponModel = Instantiate(currentWeapon.weaponModel, transform);
        weaponModel.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        travelExtraRange = currentWeapon.weaponExtraRange;
        speed = currentWeapon.weaponSpeed;
    }
    private void GoForward(){
        if(Vector3.Distance(transform.position, startPosition) > maxTravelDistance){
            if(currentWeapon.canReturn){
                isReturning = true;
            }else{
                OnDespawn();
            }
        }
    }
    private void GoBack(Vector3 characterPosition){
        moveDirection = (characterPosition - transform.position).normalized;
        rb.velocity = moveDirection * speed;
        if(currentCharacter.isDead){
            Debug.Log("Cannot go back");
            OnDespawn();
            return;
        } 
        // transform.position = Vector3.MoveTowards(transform.position, throwingPosition, 10 * Time.deltaTime);
        if(Vector3.Distance(transform.position, characterPosition) <= 0.3f){
            OnDespawn();
            return;
        } 
        Debug.Log("Go Back");
    }
}
