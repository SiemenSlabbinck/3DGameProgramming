using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        Vector3 desiredPositon = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPositon, smoothSpeed);
        transform.position = smoothPosition;
    }
}
