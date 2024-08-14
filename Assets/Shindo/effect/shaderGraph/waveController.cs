using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveController : MonoBehaviour
{
    private Material mat;
    private float adjuster;
    // Start is called before the first frame update
    void Start()
    {
        adjuster = -1 * Time.time;
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_Reset", adjuster);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
