using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Yukino;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag =="Player")
        {
            Yukino.transform.Rotate(0f, 180f, 0f);
        }
    }
}
