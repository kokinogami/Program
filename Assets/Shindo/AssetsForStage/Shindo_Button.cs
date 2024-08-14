using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shindo_Button : MonoBehaviour
{
    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(Door);
            Destroy(this.gameObject);
        }
    }
}
