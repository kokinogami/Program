using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class StartPlatform : MonoBehaviour
{
    //public YukinoMain Main;

    [System.NonSerialized] public static bool OnPlayer = false;
    [System.NonSerialized] public static bool countdown = false;
    public float count = 3f;
    [TooltipAttribute("î≠éÀë¨ìx")] public float startForwardSpeed = 1500.0f;
    [TooltipAttribute("î≠éÀë¨ìx")] public float startUpSpeed = 35f;
    [TooltipAttribute("å∏êäë¨ìx")] public float dragSpeed = 1200.0f;
    [SerializeField] private GameObject startBarrier;

    int countSound = 3;
    bool OnSound = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = false;
        OnPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            count -= Time.deltaTime;
        }
        if (count < 0)
        {
            GameManager.Main.changeLoad();
            GameManager.Main.rb.AddForce(0.0f, startUpSpeed * Time.deltaTime, (startForwardSpeed + (dragSpeed * count)) * Time.deltaTime * 3, ForceMode.Impulse);
            OnGOSOund();
            GameManager.onTimeCount = true;
            Destroy(startBarrier);
            if (count < -0.15f)
            {
                GameManager.Main._input.enabled = true;
                GameManager.startBGM = true;
                Destroy(this.gameObject);
            }
        }
        CountDownSound((int)count + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && OnPlayer == false)
        {
            OnPlayer = true;
            GameManager.Main._input.enabled = false;
        }
    }
    void CountDownSound(int Co)
    {
        if (countSound > Co && Co > 0)
        {
            GameManager.Sound.SetCueName("321");
            GameManager.Sound.OnSound();
            countSound = Co;
        }
    }
    void OnGOSOund()
    {
        if (OnSound) return;
        GameManager.Sound.SetCueName("Go");
        GameManager.Sound.OnSound();
        OnSound = true;
    }
}
