using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private AreaCircle areaCircle;
    public PlayerBaseState currentState;
    public PlayerIdleState playerIdleState = new PlayerIdleState();
    public PlayerAttackState playerAttackState = new PlayerAttackState();
    public PlayerMoveState playerMoveState = new PlayerMoveState();
    private void Start() {
        currentState = playerIdleState;
        currentState.OnStart(this);
        areaCircle.UpdateCircle(hitRange/2);
    }
    private void Update() {
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

    public void SwitchState(PlayerBaseState newState){
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnStart(this);
    }
}
