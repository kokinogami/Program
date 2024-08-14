using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimeScale : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particles;
    [SerializeField]List<float> psTime;
    timescale timeScale = EffectTimeScale.timescale.nomal;
    enum timescale
    {
        nomal,
        freez
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            psTime.Add(particles[i].main.simulationSpeed);
        }
        if (Time.timeScale == 0)
        {
            timeScale = EffectTimeScale.timescale.freez;
            for (int i = 0; i < particles.Count; i++)
            {
                var psmain = particles[i].main;
                psmain.simulationSpeed = 0.0f;
            }
        }
        else
        {
            timeScale = EffectTimeScale.timescale.nomal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 && timeScale == EffectTimeScale.timescale.nomal)
        {
            timeScale = EffectTimeScale.timescale.freez;
            for (int i = 0; i < particles.Count; i++)
            {
                var psmain = particles[i].main;
                psmain.simulationSpeed = 0.0f;
            }
        }
        else if (Time.timeScale != 0 && timeScale == EffectTimeScale.timescale.freez)
        {
            timeScale = EffectTimeScale.timescale.nomal;
            for (int i = 0; i < particles.Count; i++)
            {
                var psmain = particles[i].main;
                psmain.simulationSpeed = psTime[i];
            }
        }
    }
}
