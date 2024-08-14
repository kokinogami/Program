using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushi : MonoBehaviour
{
    Rigidbody rb;
    public bool Ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Ground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            if (Ground == true)
            {
                rb.AddForce(5.0f,8.0f, 0.0f, ForceMode.Impulse);
                Ground = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//ínñ Ç∆ÇÃê⁄êGîªíË
        {
            Ground = true;
        }
    }
}
