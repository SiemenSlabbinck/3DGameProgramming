using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour {

    [SerializeField]
    private GameObject Fire;
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
    private bool timer = false;
    private float timeLeft = 3.0f;

    void OnParticleCollision(GameObject test) {
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
     }
    }
}