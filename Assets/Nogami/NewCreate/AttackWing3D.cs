using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class AttackWing3D : MonoBehaviour
{
    // Start is called before the first frame update
    CriAtomSource criAtomSource;
    float soundVolum = 1;
    float count = 0;
    bool onsound;
    [SerializeField] BossWing bossWing;
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
        if (bossWing.isWing == true && onsound == false)
        {
            On3Daudio();
            onsound = true;
            Debug.Log("‰Hon");

        }
        else if (bossWing.isWing == false && onsound)
        {
            criAtomSource.Stop();
            onsound = false;
            Debug.Log("‰Hoff");
        }
    }

    public void On3Daudio()
    {
        Debug.Log("OnSound");
        criAtomSource.volume = SFXSlider.SFXvolume / 2 * BGMvolumeSlider.MasterVolume * soundVolum;
        //criAtomSource.cueName = cueName;
        criAtomSource.Play();
    }
    void testSound()
    {
        count += Time.deltaTime;
        if (count < 5) return;
        On3Daudio(); count = 0;
    }
}

