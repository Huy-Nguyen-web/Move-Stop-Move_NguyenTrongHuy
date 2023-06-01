using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -10f;
    public DynamicJoystick joystick;
    [SerializeField] private AreaCircle areaCircle;
    public IState<Player> currentState;
    public CharacterController controller;
    public PlayerIdleState playerIdleState = new PlayerIdleState();
    public PlayerAttackState playerAttackState = new PlayerAttackState();
    public PlayerMoveState playerMoveState = new PlayerMoveState();
    public PlayerDieState playerDieState = new PlayerDieState();
    private int currentSelectedPant = -1;
    private int currentSelectedHat = -1;
    private Material defaultPantMaterial;

    private void Start() {
        point = 0;
        defaultPantMaterial = characterPantSkin.material;

        characterSize = 1f;
        weaponType = CosmeticManager.Instance.currentWeapon;

        ResizeCharacter();
        SpawnOnHandWeapon(weaponType);
        SpawnWaypoint();
        
        CosmeticManager.Instance.onWeaponChange += ChangeOnHandWeapon;
        CosmeticManager.Instance.onPantChange += ChangeCharacterPant;
        CosmeticManager.Instance.onHatChange += ChangeCharacterHat;
        CosmeticManager.Instance.onSelectedHatChange += ChangeSelectedHat;
        CosmeticManager.Instance.onSelectedPantChange += ChangeSelectedPant;
        

        currentState = playerIdleState;
        currentState.OnStart(this);
        areaCircle.UpdateCircle(hitRange/2 + weaponType.weaponExtraRange);

        ChangeCharacterMaterial();
    }
    private void Update() {
        if(!GameManager.IsState(GameManager.GameState.Start)) return;
        UpdateMovement();
        currentState.OnUpdate(this);
    }

    private void UpdateMovement(){
        Vector3 velocity = Vector3.zero;
        moveDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if(Vector3.Distance(Vector3.zero, moveDirection) > 0.1f){
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            velocity.x = joystick.Horizontal * speed * Time.deltaTime;
            velocity.z = joystick.Vertical * speed * Time.deltaTime;
        }

        controller.Move(velocity);
    }

    public void SwitchState(IState<Player> newState){
        if(currentState != null){
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState != null){
            currentState.OnStart(this);
        }
    }
    public void DeleteTempSkin(){
        if(currentSelectedHat == -1 && this.hat != null){
            Destroy(this.hat);
        }
        if(currentSelectedPant == -1){
            characterPantSkin.material = defaultPantMaterial;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(weapon == null || (weapon.gameObject != other.gameObject)){
            SwitchState(playerDieState);
        }
    }
    private void ChangeSelectedHat(int index){
        currentSelectedHat = index;
    }
    private void ChangeSelectedPant(int index){
        currentSelectedPant = index;
    }
    public override void ChangeOnHandWeapon(WeaponData currentWeaponType)
    {
        base.ChangeOnHandWeapon(currentWeaponType);
        areaCircle.UpdateCircle(hitRange/2 + weaponType.weaponExtraRange);
    }
}
