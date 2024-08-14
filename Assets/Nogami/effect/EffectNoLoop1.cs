using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectNoLoop1 : MonoBehaviour
{
    public ParticleSystem particleSy;
    public float DestroyCount;
    public float FleezCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroyCount -= Time.deltaTime;
        if (DestroyCount <= 0)
        {
            Destroy(this.gameObject);
        }
        if (FleezCount <= 0)
        {
        }
    }
}
