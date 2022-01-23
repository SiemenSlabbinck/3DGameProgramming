using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDissapear : MonoBehaviour
{
    private float timeLeft = 5.0f;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
