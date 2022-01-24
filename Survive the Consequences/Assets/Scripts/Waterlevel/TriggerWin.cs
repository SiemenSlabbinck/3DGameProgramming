using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has won");
        SceneManager.LoadScene(5);    
    }
}
