using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrownPlayer : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Die has drowned");
        SceneManager.LoadScene(4);
    }    
}
