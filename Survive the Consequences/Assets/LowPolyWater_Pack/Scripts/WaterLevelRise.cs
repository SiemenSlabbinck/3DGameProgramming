using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelRise : MonoBehaviour
{
    public Transform target;
    public int speed;
    public float speedLevel;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = this.gameObject.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;

        transform.position = Vector3.MoveTowards(a, b, speedLevel);
    }
}
