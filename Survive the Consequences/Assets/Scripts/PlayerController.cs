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

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed = false;
    bool isJumping = false;

    float rotationFactorPerFrame = 15.0f;
    float gravity = -9.81f;
    float groundedGravity = -0.05f;
    float verticalVelocity;
    float maxJumpTime = 0.5f;

    [SerializeField]
    float runSpeed = 5.0f;

    [SerializeField]
    float walkSpeed = 1.5f;

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

        inputActions.PlayerControls.Move.started += MovementInputs;
        inputActions.PlayerControls.Move.canceled += MovementInputs;
        inputActions.PlayerControls.Move.performed += MovementInputs;
        inputActions.PlayerControls.Sprint.started += Run;
        inputActions.PlayerControls.Sprint.canceled += Run;
        inputActions.PlayerControls.Jump.started += Jump;
        inputActions.PlayerControls.Jump.canceled += Jump;

        setupJumpVariables();
    }

    private void setupJumpVariables()
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
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        HandleGravity();
        HandleJump();
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

    private void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumping = true;
            currentMovement.y = verticalVelocity;
            currentRunMovement.y = verticalVelocity;
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
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation =  Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            animator.SetBool(isJumpingHash, false);
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else 
        {
            
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }

    }

}
