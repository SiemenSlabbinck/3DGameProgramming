using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerActions inputActions;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isAttackingHash;
    int isRunGunHash;

    public GameObject bullet;
    public Transform firePosition;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 moveWalkDirection;
    Vector3 moveRunDirection;

    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed = false;
    bool isJumping = false;
    bool isAimPressed;
    bool isAttackPressed;
    bool isEquipPresssed;

    float gravity = -9.81f;
    float groundedGravity = -0.05f;
    float verticalVelocity;
    float maxJumpTime = 0.5f;


    [SerializeField]
    float runSpeed = 5.0f;

    [SerializeField]
    float walkSpeed = 2.0f;

    [SerializeField]
    float jumpHeight = 1.0f;

    private void Awake()
    {
        inputActions = new PlayerActions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isRunGunHash = Animator.StringToHash("isRunGun");

        inputActions.PlayerControls.Move.started += MovementInputs;
        inputActions.PlayerControls.Move.canceled += MovementInputs;
        inputActions.PlayerControls.Move.performed += MovementInputs;
        inputActions.PlayerControls.Sprint.started += Run;
        inputActions.PlayerControls.Sprint.canceled += Run;
        inputActions.PlayerControls.Jump.started += Jump;
        inputActions.PlayerControls.Jump.canceled += Jump;
        inputActions.PlayerControls.Aim.started += Aim;
        inputActions.PlayerControls.Aim.canceled += Aim;
        inputActions.PlayerControls.Attack.started += Attack;
        inputActions.PlayerControls.Attack.canceled += Attack;
        inputActions.PlayerControls.Equip.started += Equip;
        inputActions.PlayerControls.Equip.canceled += Equip;


        SetupJumpVariables();
    }



    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        verticalVelocity = (2 * jumpHeight) / timeToApex;
    }

    private void Update()
    {
        HandleRotation();
        HandleAnimation();
        if (isRunPressed)
        {
            characterController.Move(moveRunDirection * Time.deltaTime);
        }
        else
        {
            characterController.Move(moveWalkDirection * Time.deltaTime);
        }
        HandleGravity();
        HandleJump();
        HandleEquip();
        HandleAttackAnimation();
        if (isAttackPressed && isAimPressed)
        {
            Shoot();
        }
    }

    private void OnEnable()
    {
        inputActions.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerControls.Disable();
    }

    private void MovementInputs(InputAction.CallbackContext callbackContext)
    {
        currentMovementInput = callbackContext.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * walkSpeed;
        currentMovement.z = currentMovementInput.y * walkSpeed;
        currentRunMovement.x = currentMovementInput.x * runSpeed;
        currentRunMovement.z = currentMovementInput.y * runSpeed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void Run(InputAction.CallbackContext callbackContext)
    {
        isRunPressed = callbackContext.ReadValueAsButton();
    }


    private void Jump(InputAction.CallbackContext callbackContext)
    {
        isJumpPressed = callbackContext.ReadValueAsButton();
        
    }
    private void Aim(InputAction.CallbackContext callbackContext)
    {
        isAimPressed = callbackContext.ReadValueAsButton();

    }
    private void Attack(InputAction.CallbackContext callbackContext)
    {
        isAttackPressed = callbackContext.ReadValueAsButton();
    }
    private void Equip(InputAction.CallbackContext callbackContext)
    {
        isEquipPresssed = callbackContext.ReadValueAsButton();
    }

    private void HandleEquip()
    {
       
    }
    private void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumping = true;
            moveWalkDirection.y = verticalVelocity;
            moveRunDirection.y = verticalVelocity;
        }else if (isJumping && characterController.isGrounded && !isJumpPressed)
        {
            isJumping = false;
        }
    }
    private void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

        
    }
    
    private void HandleRotation()
    {
        float walkYStore = moveWalkDirection.y;
        float runYStore = moveWalkDirection.y;
        moveWalkDirection = (transform.forward * currentMovement.z) + (transform.right * currentMovement.x);
        moveRunDirection = (transform.forward * currentRunMovement.z) + (transform.right * currentRunMovement.x);

        moveWalkDirection = moveWalkDirection.normalized * walkSpeed;
        moveRunDirection = moveRunDirection.normalized * runSpeed;
        moveWalkDirection.y = walkYStore;
        moveRunDirection.y = runYStore;
    }

    private void HandleAttackAnimation()
    {
        bool isAttacking = animator.GetBool(isAttackingHash);
        bool isRunningGunning = animator.GetBool(isRunGunHash);

        if (isAimPressed && !isAttacking )
        {
            animator.SetBool(isAttackingHash, true);
        }
        else if (!isAimPressed && isAttacking)
        {
            animator.SetBool(isAttackingHash, false);
        }
        else if (isAimPressed && isAttacking && isMovementPressed)
        {
            animator.SetBool(isRunGunHash, true);
        }

        if ((isRunPressed && isAimPressed) && !isRunningGunning)
        {
            animator.SetBool(isRunGunHash, true);
        }
        else if ((isRunPressed && !isAimPressed) && isRunningGunning)
        {
            animator.SetBool(isRunGunHash, false);
        }
        

    }

    private void Shoot()
    {
        Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            animator.SetBool(isJumpingHash, false);
            moveWalkDirection.y = groundedGravity;
            moveRunDirection.y = groundedGravity;
        }
        else 
        {
            moveWalkDirection.y += gravity * Time.deltaTime;
            moveRunDirection.y += gravity * Time.deltaTime;
        }

    }


}
