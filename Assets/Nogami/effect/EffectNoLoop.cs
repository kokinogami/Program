using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectNoLoop : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSy;
    float DestroyCount;
    // Start is called before the first frame update
    void Start()
    {
        DestroyCount = particleSy.startLifetime + particleSy.duration;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyCount -= Time.deltaTime;
        if (DestroyCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
