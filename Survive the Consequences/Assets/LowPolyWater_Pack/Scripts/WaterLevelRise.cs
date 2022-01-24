using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelRise : MonoBehaviour
{
    public Transform target;
    public int speed;
    public float speedLevel;

    void Start()
    {
        if(target == null)
        {
            target = this.gameObject.transform;
        }
        
    }
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;

        transform.position = Vector3.MoveTowards(a, b, speedLevel);
    }
}
