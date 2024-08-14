using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class TimelineInstanceBossafter : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossGauge;
    [SerializeField] GameObject MoviePauseUI;
    [SerializeField] GameObject MoviePauseUIfirestButton;
    [SerializeField] GameObject BossSound;
    [SerializeField] Animator animator;
    [SerializeField] PlayableDirector PlayableDirector;
    [SerializeField] GameObject ClearBG;
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
        GameManager.GM.SoundSlider = MoviePauseUI;
        GameManager.GM.SoundSliderButton = MoviePauseUIfirestButton;
        GameManager.Sound.StopBGM();
        GameManager.onTimeCount = true;

        setBossSound();
    }
    public void finTimeline()
    {
        disFreezPlayer();
    }
    public void bossActive()
    {
        bossGauge.SetActive(false);
        boss.SetActive(false);
    }
     void freezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateMovie);
    }
    void disFreezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateIdle);
    }
    public void activeAnimator(bool active)
    {
        animator.enabled = active;
    }
    public void stopTimeline()
    {
        PlayableDirector.Stop();
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += playTimelinePAD;
    }
    public void playTimeline()
    {
        PlayableDirector.Play();
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= playTimelinePAD;
    }
    public void playTimelinePAD(InputAction.CallbackContext context)
    {
        playTimeline();
    }

    public void UIInput()
    {
        GameManager.Main.CameraCs.CameraMove(false);
        GameManager.Main.DestroyInput();// this.GetType().Name);
        GameManager.Main.EnableInputCurrent();
        GameManager.Main.DestroyInputMenu();
        GameManager.gameState = GameState.gameclera;
        GameManager.Main._input.SwitchCurrentActionMap("UI");
    }

    public void gameClear() 
    {
        //ClearBG.SetActive(true);
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += ClearText;
    }
    /*public void setBossSound()
    {
        //BossSound.SetActive(false);
    }*/
    public void clearUIactive()
    {
        ClearBG.SetActive(true);
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= ClearText;
    }
    void ClearText(InputAction.CallbackContext context)
    {
        clearUIactive();
    }
    public void setBossSound()
    {
        GameManager.GM.BossClearUI();
        GameManager.Sound.SetCueName("Boss_After");
        GameManager.Sound.OnBGM();
    }
    public void setBossClearSound()
    {
        GameManager.GM.BossClearUI();
        GameManager.Sound.SetCueName("Boss_Clear");
        GameManager.Sound.OnBGM();
    }
}
