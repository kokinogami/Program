using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllSensitivitySlider : MonoBehaviour
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
        svSlider.value = GameManager.svValue * 1000;
        float sValue = GameManager.svValue * 100;
        svValuetext.text = sValue.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeSliderValue()
    {
        GameManager.svValue = svSlider.value / 1000;
        float sValue = GameManager.svValue * 100;
        svValuetext.text = sValue.ToString() + "%";
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            //GameManager.Main.CameraCs.AllChangeSensitivity(GameManager.svValue);
            GameManager.Main.CameraCs.CameraChangeSensitivity(GameManager.CmSvValue * GameManager.svValue);
            GameManager.Main.CameraCs.ChargeBreakChangeSensitivity(GameManager.ChSvValue * GameManager.svValue);
        }
        Debug.Log(GameManager.svValue);
    }
}
