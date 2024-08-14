using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHipDrop : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private int upForce;
    private int hipDrop;
    private float distance;
    private bool doubleJump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        upForce = 500;
        distance = 1.0f;
        hipDrop = -1000;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isGround = Physics.Raycast(ray, distance);
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);

        if (isGround)
        {
            doubleJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isGround)
                rb.AddForce(new Vector3(0, upForce, 0));

            else
            {
                if (doubleJump)
                    rb.AddForce(new Vector3(0, hipDrop, 0));
                doubleJump = false;
            }
        }
    }
}
