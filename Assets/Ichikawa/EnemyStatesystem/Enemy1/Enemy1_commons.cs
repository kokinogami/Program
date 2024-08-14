using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }
    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
}