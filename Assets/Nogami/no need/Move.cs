using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody Toprb;
    Rigidbody Yukinorb;
    public GameObject Top;
    GameObject Yukino;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out Yukinorb);
        Toprb = Top.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        Vector2 A = new Vector2(dx, dz).normalized * 5;
        Toprb.velocity = new Vector3(A.x, -1.05f, A.y);
        //rb.AddForce(0.0f, 9.8f, 0.0f, ForceMode.Force);
        Debug.Log(Toprb.velocity);
        Yukinorb.AddForce(0.0f, -1.0f, 0.0f, ForceMode.Impulse);
    }
}
