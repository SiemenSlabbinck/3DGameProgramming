using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MouseDirectionController : MonoBehaviour
{
    PlayerActions inputActions;

    public Transform target;
    Vector3 offset;
    Vector2 mousePosition;

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
