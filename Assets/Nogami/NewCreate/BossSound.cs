using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    [SerializeField] Transform befor;
    [SerializeField] Transform nomal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (befor.gameObject.activeSelf == true)
        {
            this.transform.position = befor.position;
        }
        else
        {
            this.transform.position = nomal.position;
        }
    }
}
