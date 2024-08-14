using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycon : MonoBehaviour
{
    Rigidbody rb;
    private float TargetDis;
    private float MoveDis;
    public Transform target;
    private float MoveCount = 0.0f;
    private bool Ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Ground = true;
        MoveDis = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MoveCount);
        TargetDis = Vector3.Distance(this.transform.position, target.transform.position);
        if (TargetDis < MoveDis)
        {
            if (Ground)
            {
                if (MoveCount >= 2.0f)
                {
                    Debug.Log("a");
                    Move();
                }
                else
                {
                    transform.LookAt(target);
                    MoveCount = 1.0f * Time.deltaTime + MoveCount;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Ground = true;
        }
    }
    private void Move()
    {
        rb.AddRelativeForce(0.0f, 200.0f, 200.0f, ForceMode.Force);
        Ground = false;
        MoveCount = 0.0f;
    }
}
