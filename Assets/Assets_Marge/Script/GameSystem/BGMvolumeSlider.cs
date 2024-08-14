using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
using UnityEngine.UI;

public class BGMvolumeSlider : MonoBehaviour
{
    public string categoryName1;
    public string categoryName2;
    private Slider slider;
    public static float MasterVolume = 1;
    public Text text;
    float textValue;



    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = MasterVolume;
        textValue = Mathf.Floor(MasterVolume * 100);
        text.text = textValue.ToString() + "%";
    }

    public void SetVolume()
    {
        Debug.Log(slider.value);
        MasterVolume = slider.value;
        textValue = Mathf.Floor(slider.value * 100);
        text.text = textValue.ToString() + "%";
        CriAtom.SetCategoryVolume("BGM", MasterVolume * BGMSlider.BGMvolume);//BGM
        CriAtom.SetCategoryVolume("SFX", MasterVolume * SFXSlider.SFXvolume);//SFX
    }
}
