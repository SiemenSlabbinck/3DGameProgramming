using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    float moventSpeed = 5.0f;
    float gravity = -9.81f;
    float groundedGravity = -0.05f;

    Vector3 movement;
    Animator animate;
    CharacterController characterController;

    private void Awake()
    {
        player = GameObject.Find("Lizzy").transform;
        animate  = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            animate.SetBool("parasiteIsIdle", false);
            if (direction.magnitude > 2)
            {
                characterController.Move(moventSpeed * Time.deltaTime * direction.normalized);
                animate.SetBool("parasiteIsWalking", true);
                animate.SetBool("parasiteIsAttacking", false);
            }
            else
            {
                animate.SetBool("parasiteIsAttacking", true);
                animate.SetBool("parasiteIsWalking", false);
            }
        }
        else
        {
            animate.SetBool("parasiteIsIdle", true);
            animate.SetBool("parasiteIsWalking", false);
            animate.SetBool("parasiteIsAttacking", false);
        }
        HandleGravity();
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            movement.y = groundedGravity;
        }
        else
        {
            movement.y += gravity * Time.deltaTime;
        }

    }
}



    