using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase : MonoBehaviour
{
    public Transform player;
    static Animator animate;

    private void Awake()
    {
        player = GameObject.Find("Lizzy").transform;
        animate  = GetComponent<Animator>();
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
                this.transform.Translate(0, 0, 0.03f);
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
    }
}



    