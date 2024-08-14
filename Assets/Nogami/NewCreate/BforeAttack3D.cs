using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class BforeAttack3D : MonoBehaviour
{
    CriAtomSource criAtomSource;
    float soundVolum = 1;
    float count = 0;
    bool onsound;
    [SerializeField]Boss boss;
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
        //testSound();
        if (boss.StateProcessor.State == boss.StateAttack3 && onsound==false)
        {
            On3Daudio();
            onsound = true;
        }
        else if (boss.StateProcessor.State != boss.StateAttack3 && onsound)
        {
            criAtomSource.Stop();
            onsound = true;
        }
    }

    public void On3Daudio()
    {
        Debug.Log("OnSound");
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
