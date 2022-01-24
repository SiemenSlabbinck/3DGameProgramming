using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed, bulletLife;

    public Rigidbody myRigidbody;

    // Update is called once per frame
    void Update()
    {
        BulletFire();
        bulletLife -= Time.deltaTime;
        if (bulletLife < 0)
        {
            Destroy(gameObject);
        }
    }
    void BulletFire()
    {
        myRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
