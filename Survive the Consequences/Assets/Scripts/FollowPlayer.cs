using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FollowPlayer : MonoBehaviour
{
    PlayerActions inputActions;

    public Transform target;
    public Transform toLookAt;
    public Vector3 offset;
    Vector2 mousePosition;

    //public bool useOffsetValues;

    [SerializeField]
    float mouseSensitivity = 5.0f;

    private void Awake()
    {
        inputActions = new PlayerActions();
        inputActions.PlayerControls.Look.performed += MouseMovement;
    }

    private void Start()
    {
        offset = target.position - transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        MouseRotate();
    }
    private void MouseMovement(InputAction.CallbackContext callbackContext)
    {
        mousePosition = callbackContext.ReadValue<Vector2>();
    }
    private void MouseRotate()
    {
        mousePosition.x = mousePosition.x * mouseSensitivity * Time.deltaTime;
        target.Rotate(0, mousePosition.x, 0);

        float desiredYAngle = target.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);
        transform.LookAt(toLookAt);
    }
  
    private void OnEnable()
    {
        inputActions.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerControls.Disable();
    }

}
