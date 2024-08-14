using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCo : MonoBehaviour
{
    // Start is called before the first frame update
    enum Loop
    {
        Loop,
        noLoop
    }
    enum Timescale
    {
        DeltaTime,
        unScaleTimeDeltatime
    }

    [SerializeField] ParticleSystem particleSy;
    float DestroyCount;
    [SerializeField] bool LoopDefault;
    [SerializeField] bool deltatimeDefault;
    [SerializeField] Loop thisLoop;
    [SerializeField] Timescale thisTimescale;
    bool Loopbool;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (LoopDefault)
        {
            switch (thisLoop)
            {
                case Loop.Loop:
                    particleSy.loop = true;
                    break;
                case Loop.noLoop:
                    particleSy.loop = false;
                    break;
            }
        }
        if (deltatimeDefault)
        {
            switch (thisTimescale)
            {
                case Timescale.DeltaTime:
                    particleSy.Simulate(Time.deltaTime);
                    break;
                case Timescale.unScaleTimeDeltatime:
                    particleSy.Simulate(Time.unscaledDeltaTime);
                    break;
            }
        }
        DestroyCount = particleSy.startLifetime + particleSy.duration;
        Loopbool = particleSy.loop;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        TimescaleState();
        if (deltatimeDefault == false)
        {
            switch (thisTimescale)
            {
                case Timescale.DeltaTime:
                    particleSy.Simulate(Time.deltaTime,true,false);
                    break;
                case Timescale.unScaleTimeDeltatime:
                    particleSy.Simulate(Time.unscaledDeltaTime,true,false);
                    break;
            }
        }
    }
    void TimescaleState()
    {
        if (Time.timeScale != 0)
        {
            thisTimescale = Timescale.unScaleTimeDeltatime;
        }
        else
        {
            thisTimescale = Timescale.DeltaTime;
        }
    }
    void LoopState()
    {
        if (Time.timeScale != 0)
        {
            thisTimescale = Timescale.unScaleTimeDeltatime;
        }
        else
        {
            thisTimescale = Timescale.DeltaTime;
        }
    }
    void DestroyEF()
    {
        DestroyCount -= Time.deltaTime;
        if (DestroyCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
