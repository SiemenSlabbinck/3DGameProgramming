using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField]
    private GameObject Fire;
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    private bool timer = false;
    private float timeLeft = 3.0f;

    void Update()
    {
        if (timer)
            timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Debug.Log("Die");
            SceneManager.LoadScene(4);
        }
    }

    void OnParticleCollision(GameObject collision)
    {
        Debug.Log("On Fire");
        timer = true;
        Fire.SetActive(true);
    }

     private void OnTriggerEnter(Collider other){
        if (other.tag == "Finish")
            SceneManager.LoadScene(5);
        else if (other.tag == "Enemy")
            timer = true;
     }
}