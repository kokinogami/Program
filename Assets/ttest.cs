using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttest : MonoBehaviour
{
    BoxCollider CapCol;
    // Start is called before the first frame update
    void Start()
    {
        CapCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            CapCol.enabled = false;

            Invoke("modoru", 3);
        }
    }

    void modoru()
    {
        CapCol.enabled = true;
    }

}
