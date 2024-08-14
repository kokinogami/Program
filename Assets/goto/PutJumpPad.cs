using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutJumpPad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject JumpPadPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Instantiate(JumpPadPrefab, transform.position, Quaternion.identity);
        }
    }
}
