using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDoorAutomation : MonoBehaviour
{
    public GameObject movingDoor;

    public float maximumOpening = 5f;
    public float maximumClosing = -0.77f;

    public float movementSpeed = 3f;

    bool playerIsHere;

    void Start()
    {
        playerIsHere = false;
    }

    void Update()
    {
        if (playerIsHere && movingDoor.CompareTag("baseDoor_x"))
        {
            if (movingDoor.transform.position.x < maximumOpening)
            {
                movingDoor.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
            }
        }
        else if (!playerIsHere && movingDoor.CompareTag("baseDoor_x"))
        {
            if (movingDoor.transform.position.x > maximumClosing)
            {
                movingDoor.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
            }
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerIsHere = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerIsHere = false;
        }
    }
}
