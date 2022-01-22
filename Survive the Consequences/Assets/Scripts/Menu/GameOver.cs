using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private bool timer = false;
    private float timeLeft = 3.0f;

    void Start(){
        timer = true;
    }
    void Update()
    {
        if (timer)
            timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
