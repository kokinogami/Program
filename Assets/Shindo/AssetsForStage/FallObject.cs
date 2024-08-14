using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    private Shindo_Main Shindo_Yukino; //ユキノ操作用スクリプト。統合するときは名前を書き換えてね
    private int iceball = 0;
    private float wait = 0.6f;
    private float oldwait = 0.6f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.Shindo_Yukino = FindObjectOfType<Shindo_Main>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iceball == 1 && wait > 0)
        {
            wait -= Time.deltaTime;
        }

        if (wait <= 0 && oldwait > 0)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }

        oldwait = wait;
    }

    private void OnCollisionEnter(Collision collision)
    {
        iceball = Shindo_Yukino.index;
    }
}
