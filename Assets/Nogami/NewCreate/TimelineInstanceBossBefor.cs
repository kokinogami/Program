using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineInstanceBossBefor : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossGauge;
    [SerializeField] GameObject TimeCounter;
    [SerializeField] GameObject UnderBrack;
    [SerializeField] GameObject MoviePauseUI;
    [SerializeField] GameObject MoviePauseUIfirestButton;
    [SerializeField] GameObject BossPauseUI;
    [SerializeField] GameObject BossPauseUIfirestButton;
    [SerializeField] GameObject AreaWall;
    [SerializeField] GameObject BossSound;
    [SerializeField] Animator animator;
    //[SerializeField] GameObject fadeinout;
    // Start is called before the first frame update
    void Start()
    {
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startTimeline()
    {
        freezPlayer();
        GameManager.Main.EnableInputMenu();
        GameManager.Sound.StopBGM();
        //GameManager.GM.SoundSlider = MoviePauseUI;
        //GameManager.GM.SoundSliderButton = MoviePauseUIfirestButton;
    }
    public void finTimeline()
    {
        disFreezPlayer();
        GameManager.onTimeCount = true;
        TimeCounter.SetActive(true);
        UnderBrack.SetActive(true);
        GameManager.GM.BossBattle = true;
        startBossBGM();
        AreaWall.SetActive(true);
        //Boss bossCs;
        //boss.TryGetComponent(out bossCs);
        //bossCs.enabled = true;
    }
    public void bossActive(bool active)
    {
        bossGauge.SetActive(true);
        boss.SetActive(active);
        GameManager.GM.SoundSlider = BossPauseUI;
        GameManager.GM.SoundSliderButton = BossPauseUIfirestButton;
    }
    void freezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateMovie);
        //GameManager.Main.DestroyInput();//this.GetType().Name);
        //GameManager.Main.EnableInputCurrent();
    }
    void disFreezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateIdle);
        //GameManager.Main.DestroyInputCurrent();
        //GameManager.Main.EnableInput();
    }

    public void retryStartBoss()
    {
        Debug.Log("リトライボス");
        bossGauge.SetActive(true);
        boss.SetActive(true);
        GameManager.onTimeCount = true;
        TimeCounter.SetActive(true);
        UnderBrack.SetActive(true);
        GameManager.GM.SoundSlider = BossPauseUI;
        GameManager.GM.SoundSliderButton = BossPauseUIfirestButton;
        GameManager.GM.BossBattle = true;
        startBossBGM();
        Debug.Log("BGM");
        AreaWall.SetActive(true);
        BossSound.SetActive(true);
    }
    public void activeAnimator(bool active)
    {
        animator.enabled = active;
    }
    public void startBossBGM()
    {
        Debug.Log("BossBGM" + GameManager.Sound);
        GameManager.Sound.SetCueName("Boss");
        GameManager.Sound.OnBGM();
        GameManager.GM.StartCoroutine("OnStageBGM");
    }
    public void soundstart(string cueName)
    {
        GameManager.Sound.SetCueName(cueName);
        GameManager.Sound.OnBGM();
    }
    public void setBossSound()
    {
        GameManager.Sound.SetCueName("Boss_Before");
        GameManager.Sound.OnBGM();
        BossSound.SetActive(true);
    }
}
