using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialOP : MonoBehaviour
{
    GameObject DefalutPauseUI;
    GameObject DefalutUIfirestButton;
    [SerializeField] GameObject MoviePauseUI;
    [SerializeField] GameObject MoviePauseUIfirestButton;
    [SerializeField] PlayableDirector PlayableDirector;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.tutorialUnlock == false)
        {
            PlayableDirector.enabled = true;
            GameManager.tutorialUnlock = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startTimeline()
    {
        freezPlayer();
        DefalutPauseUI = GameManager.GM.SoundSlider;
        DefalutUIfirestButton = GameManager.GM.SoundSliderButton;
        GameManager.GM.SoundSlider = MoviePauseUI;
        GameManager.GM.SoundSliderButton = MoviePauseUIfirestButton;
        GameManager.Sound.SetCueName("Movie1");
        GameManager.Sound.OnSound();
    }
    public void finTimeline()
    {
        disFreezPlayer();
        GameManager.GM.SoundSlider = DefalutPauseUI;
        GameManager.GM.SoundSliderButton = DefalutUIfirestButton;
    }
    void freezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateMovie);
    }
    void disFreezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateIdle);
    }
}
