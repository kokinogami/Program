using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleDestroyer : MonoBehaviour
{
    [SerializeField] private float runTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, runTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
