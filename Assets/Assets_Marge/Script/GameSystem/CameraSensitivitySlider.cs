using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraSensitivitySlider : MonoBehaviour
{
    Slider svSlider;
    [SerializeField] Text svValuetext;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out svSlider);
        svSlider.value = GameManager.CmSvValue * 100;
        float sValue = svSlider.value / 2;
        svValuetext.text = sValue.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeSliderValue()
    {
        GameManager.CmSvValue = svSlider.value / 100;
        float sValue = svSlider.value / 2;
        svValuetext.text = sValue.ToString() + "%";
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            //GameManager.Main.CameraCs.AllChangeSensitivity(GameManager.CmSvValue);
            GameManager.Main.CameraCs.CameraChangeSensitivity(GameManager.CmSvValue * GameManager.svValue);
        }
        Debug.Log(GameManager.CmSvValue);
    }
}
