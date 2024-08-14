using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idou : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0.0f, 0.0f, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0.0f, 0.0f, -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-speed * Time.deltaTime, 0.0f , 0.0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(speed * Time.deltaTime, 0.0f, 0.0f);
        }

        if (Input.GetKey("left shift"))
        {
            speed = 200.0f;
        }

        else
        {
            speed = 100.0f;
        }
    }
}
