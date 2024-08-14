using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpiral : MonoBehaviour
{
    [SerializeField] float spiralSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * spiralSpeed * Vector3.forward);
    }
}
