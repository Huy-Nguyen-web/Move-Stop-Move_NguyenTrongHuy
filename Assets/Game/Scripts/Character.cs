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
    public int point;
    public float characterSize;
    private int[] characterSizePoints = new int[] {2, 4 ,6, 8, 10};
    
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
        Collider[] foundCharacter = Physics.OverlapSphere(transform.position, ((hitRange/2) + weaponType.weaponExtraRange) * characterSize, characterLayer);
        enemyInRange.Clear();
        for(int i = 0; i < foundCharacter.Length; i++){
           if(foundCharacter[i].transform == transform) continue;
           Transform enemy = foundCharacter[i].transform;
           enemyInRange.Add(enemy);
        }
    }
    public bool CheckEnemyInRange(){
        Collider[] foundCharacter = Physics.OverlapSphere(transform.position, ((hitRange/2) + weaponType.weaponExtraRange) * characterSize, characterLayer);
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
    public virtual void ChangeOnHandWeapon(WeaponData currentWeaponType){
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
        if(point < 2){
            characterSize = 1f;
        }else if(point >= 2 && point < 4){
            characterSize = 1.2f;
        }else if(point >= 4 && point < 6){
            characterSize = 1.5f;
        }else if(point >= 6 && point < 8){
            characterSize = 1.8f;
        }else if(point >= 8 && point < 10){
            characterSize = 2.1f;
        }else if(point >= 10){
            characterSize = 2.5f;
        }
        transform.localScale = new Vector3(characterSize, characterSize, characterSize);
    }
    // public void CheckSize(){

    // }
}   
