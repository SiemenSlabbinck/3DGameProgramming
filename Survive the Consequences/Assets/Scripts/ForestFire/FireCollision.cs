using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireCollision : MonoBehaviour {

    [SerializeField]
    private GameObject Fire;
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    private bool timer = false;
    private float timeLeft = 3.0f;
    void OnParticleCollision(GameObject collision) {
         Debug.Log("On Fire");
         timer = true;
         Fire.SetActive(true);
     }

    void Update()
    {
    if (timer)

     timeLeft -= Time.deltaTime;
     if ( timeLeft < 0 )
     {
        Debug.Log("Die");
        SceneManager.LoadScene(4);
     }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Finish")
        {
            Debug.Log("Do something here");
                        Debug.Log("Finish L");

        }
    }
}