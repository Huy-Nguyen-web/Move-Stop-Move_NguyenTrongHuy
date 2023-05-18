using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public WeaponData weaponType;
    public Transform attackPosition;
    public Transform hatPosition;
    public Vector3 moveDirection;
    public Transform currentTarget;
    public List<Transform> enemyInRange;
    public Collider characterCollider;
    public SkinnedMeshRenderer characterSkin;
    public SkinnedMeshRenderer characterPantSkin;
    public Animator animator;
    public LayerMask characterLayer;
    public GameObject onHandWeapon;
    public GameObject hat;
    public Material pantMaterial;
    public float hitRange = 10f;
    public bool isThrowing = false;
    public bool isDead = false;
    public Weapon weapon;
    private int point;
    
    public virtual void Attack(){
        TF.LookAt(currentTarget.transform.position);
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
        //TODO: OverlapSphereNonAlloc
        Collider[] foundCharacter = Physics.OverlapSphere(transform.position, hitRange/2, characterLayer);
        enemyInRange.Clear();
        for(int i = 0; i < foundCharacter.Length; i++){
           if(foundCharacter[i].transform == transform) continue;
           Transform enemy = foundCharacter[i].transform;
           enemyInRange.Add(enemy);
        }
    }
    public bool CheckEnemyInRange(){
        Collider[] foundCharacter = Physics.OverlapSphere(transform.position, hitRange/2, characterLayer);
        if(foundCharacter.Length <= 1) return false;
        return true;
    }
    public void SpawnWeapon(){
        weapon = LevelManager.Instance.SpawnWeapon();
        weapon.OnInit(this);
        onHandWeapon.SetActive(false);
    }
    public void SpawnOnHandWeapon(){
        onHandWeapon = Instantiate(weaponType.weaponModel, attackPosition);
        onHandWeapon.transform.localRotation = Quaternion.Euler(180, 0 ,0);
    }
    public void ChangeOnHandWeapon(WeaponData currentWeaponType){
        if(onHandWeapon != null){
            Destroy(onHandWeapon);
        }
        weaponType = currentWeaponType;
        SpawnOnHandWeapon();
    }
    public void ChangeCharacterHat(SkinData hatType){
        if(this.hat != null){
            Destroy(this.hat);
        }
        this.hat = Instantiate(hatType.HatModel, hatPosition);
    }
    public void ChangeCharacterMaterial(){
        characterSkin.material = CosmeticManager.Instance.skinColor[Random.Range(0, 8)];
    }

    public void ChangeCharacterPant(SkinData currentPant){
        characterPantSkin.material = currentPant.skinMaterial;
    }

    public void AddPoint(int pointToGet){
        this.point += pointToGet;
        ResizeCharacter();
    }
    public void ResizeCharacter(){
        switch(point){
            case 2:
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case 4:
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            case 6:
                transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                break;
            case 8:
                transform.localScale = new Vector3(2.1f, 2.1f, 2.1f);
                break;
            case 10:
                transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                break;
        } 
    }
    public void ChangeWeapon(WeaponType weaponType){

    }

    public void ChangeHat(HatType hatType){

    }

    public void ChangePant(PantType pantType){

    }
}

public enum WeaponType 
{
    Hammer_1 = 0,
    Hammer_2 = 1, 
    Hammer_3 = 2,

    Knife_1 = 10,
    Knife_2 = 11,

}


public enum HatType{

}

public enum PantType{

}
