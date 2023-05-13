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
    private void Start() {
        weaponType = CosmeticManager.Instance.currentWeapon;
        SpawnOnHandWeapon();
        
        CosmeticManager.Instance.onWeaponChange += ChangeOnHandWeapon;

        currentState = playerIdleState;
        currentState.OnStart(this);
        areaCircle.UpdateCircle(hitRange/2);

        ChangeCharacterMaterial();
    }
    private void Update() {
        if(GameManager.Instance.currentState != GameManager.GameState.Start) return;
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
    private void OnTriggerEnter(Collider other) {
        if(weapon == null || (weapon.gameObject != other.gameObject)){
            SwitchState(playerDieState);
        }
    }
}
