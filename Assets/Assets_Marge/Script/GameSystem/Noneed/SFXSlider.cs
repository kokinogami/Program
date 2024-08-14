using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CriWare;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{

    public string categoryName;
    private Slider slider;
    public static float SFXvolume = 1;
    public Text text;
    float textValue;



    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = SFXvolume;
        textValue = Mathf.Floor(SFXvolume * 100);
        textValue = textValue / 2;
        text.text = textValue.ToString() + "%";
    }

    public void SetVolume()
    {
        SFXvolume = slider.value;
        textValue = Mathf.Floor(slider.value * 100);
        textValue = textValue / 2;
        text.text = textValue.ToString() + "%";
        CriAtom.SetCategoryVolume(categoryName, BGMvolumeSlider.MasterVolume * SFXvolume);
    }
}