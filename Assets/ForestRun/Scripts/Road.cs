using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public float speed;
    void Start()
    {
       
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);

        if (transform.position.z < -50f)
            Destroy(gameObject);
    }
}
