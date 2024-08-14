using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CriWare;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{

    public string categoryName;
    private Slider slider;
    public static float BGMvolume = 1;
    public Text text;
    float textValue;



    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = BGMvolume;
        textValue = Mathf.Floor(BGMvolume * 100);
        textValue = textValue / 2;
        text.text = textValue.ToString() + "%";
    }

    public void SetVolume()
    {
        BGMvolume = slider.value;
        textValue = Mathf.Floor(slider.value * 100);
        textValue = textValue / 2;
        text.text = textValue.ToString() + "%";
        CriAtom.SetCategoryVolume(categoryName, BGMvolumeSlider.MasterVolume * BGMvolume);
    }
}