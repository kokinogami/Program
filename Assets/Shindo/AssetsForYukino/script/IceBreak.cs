using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class IceBreak : MonoBehaviour
{
    [SerializeField] private GameObject breakEffect;
    private float destroyTime = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            BrokenBridge();
        }
    }
    public void BrokenBridge()
    {
        Instantiate(breakEffect, this.transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
    public void countUpdate()
    {
        destroyTime = 10f;
    }
}
