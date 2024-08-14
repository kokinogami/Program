using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class audio3D : MonoBehaviour
{
    CriAtomSource criAtomSource;
    float soundVolum = 1;
    float count = 0;
    bool onSound = true;
    // Start is called before the first frame update
    private void Awake()
    {
        TryGetComponent(out criAtomSource);
        soundVolum = criAtomSource.volume;
        criAtomSource.volume = SFXSlider.SFXvolume / 2 * BGMvolumeSlider.MasterVolume * soundVolum;
    }
    void Start()
    {

    }
    private void Update()
    {
        criAtomSource.volume = SFXSlider.SFXvolume / 2 * BGMvolumeSlider.MasterVolume * soundVolum;
        //testSound();
        if (onSound == true && Time.timeScale == 0)
        {
            onSound = false;
            criAtomSource.Stop();
        }
        else if(onSound == false && Time.timeScale != 0)
        {
            onSound = true;
            criAtomSource.Play();
        }
    }

    public void On3Daudio()
    {
        criAtomSource.volume = SFXSlider.SFXvolume / 2 * BGMvolumeSlider.MasterVolume * soundVolum;
        criAtomSource.Play();
    }
    void testSound()
    {
        count += Time.deltaTime;
        if (count < 5) return;
        On3Daudio(); count = 0;
    }
}
