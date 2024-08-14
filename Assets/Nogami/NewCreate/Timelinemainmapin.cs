using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timelinemainmapin : MonoBehaviour
{
    GameObject DefalutPauseUI;
    GameObject DefalutUIfirestButton;
    [SerializeField] GameObject MoviePauseUI;
    [SerializeField] GameObject MoviePauseUIfirestButton;
    [SerializeField]PlayableDirector PlayableDirector;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.MainmapUnlock == false)
        {
            PlayableDirector.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void startTimeline()
    {
        freezPlayer();
        DefalutPauseUI = GameManager.GM.SoundSlider;
        DefalutUIfirestButton = GameManager.GM.SoundSliderButton;
        GameManager.GM.SoundSlider = MoviePauseUI;
        GameManager.GM.SoundSliderButton = MoviePauseUIfirestButton;
        GameManager.Sound.SetCueName("Movie2");
        GameManager.Sound.OnSound();

    }
    public void finTimeline()
    {
        disFreezPlayer();
        GameManager.Sound.SetCueName("Mainmap");
        GameManager.Sound.OnBGM();
        GameManager.GM.SoundSlider = DefalutPauseUI;
        GameManager.GM.SoundSliderButton = DefalutUIfirestButton;
        GameManager.MainmapUnlock = true;
    }
}
