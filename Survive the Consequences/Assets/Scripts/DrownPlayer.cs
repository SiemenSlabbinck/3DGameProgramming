using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownPlayer : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Player has died");
    }    
}
