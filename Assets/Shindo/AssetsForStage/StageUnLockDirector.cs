using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StageUnLockDirector : MonoBehaviour
{
    private bool isStage2Unlock = false;
    private bool isStage3Unlock = false;
    private bool isBossStageUnlock = false;
    public PlayableDirector playableDirector2;
    public PlayableDirector playableDirector3;
    public PlayableDirector playableDirectorBoss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.stage1Clear && GameManager.isStage2Unlock == false /* && GameManager.Main.onFadeIn == false*/)
        {
            playableDirector2.Play();
            //GameManager.isStage2Unlock = true;
        }

        //if (playableDirector2.state == PlayState.Playing)
        //{
        //    GameManager.Main.DestroyInput();//this.GetType().Name);//操作不可処理
        //    GameManager.Main.DestroyInputMenu();//操作不可処理
        //}

        //if (playableDirector2.state != PlayState.Playing && isStage2Unlock == true) //再生終了時、操作を有効化
        //{
        //    GameManager.Main.EnableInput();
        //}

        if (GameManager.stage2Clear && GameManager.isStage3Unlock == false/* && GameManager.Main.onFadeIn == false*/)
        {
            playableDirector3.Play();
            //GameManager.isStage3Unlock = true;
        }

        if (GameManager.stage3Clear && GameManager.isBossUnlock == false/* && GameManager.Main.onFadeIn == false*/)
        {
            playableDirectorBoss.Play();
            //GameManager.isBossUnlock = true;
        }

        //if (playableDirector3.state == PlayState.Playing)
        //{
        //    GameManager.Main.DestroyInput();//this.GetType().Name);//操作不可処理
        //    GameManager.Main.DestroyInputMenu();//操作不可処理
        //}

        //if (playableDirector3.state != PlayState.Playing && isStage3Unlock == true) //再生終了時、操作を有効化
        //{
        //    GameManager.Main.EnableInput();
        //}

        //if (playableDirector3.time >= playableDirector3.duration && isStage3Unlock == true)
        //{
        //    GameManager.Main.EnableInput();
        //}
        /*
        if (GameManager.stage3Clear && isBossStageUnlock == false)
        {
            playableDirectorBoss.Play();
            isBossStageUnlock = true;
        }

        if (playableDirectorBoss.state == PlayState.Playing)
        {
            GameManager.Main.DestroyInput();//this.GetType().Name);//操作不可処理
            GameManager.Main.DestroyInputMenu();//操作不可処理
        }

        if (playableDirectorBoss.state != PlayState.Playing && isBossStageUnlock == true) //再生終了時、操作を有効化
        {
            GameManager.Main.EnableInput();
        }*/
    }
    public void StartTimeline()
    {

    }
    public void FinTimeline()
    {
        freezPlayer();
        GameManager.Sound.SetCueName("Mainmap");
        GameManager.Sound.OnBGM();
    }

    public void stage2Unlook()
    {
        GameManager.isStage2Unlock = true;
    }
    public void stage3Unlook()
    {
        GameManager.isStage3Unlock = true;
    }

    public void BossUnlook()
    {
        GameManager.isBossUnlock = true;
    }

    public void freezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateMovie);
        GameManager.Sound.SetCueName("Mainmap");
        GameManager.Sound.OnBGM();
        //GameManager.Main.DestroyInput();//this.GetType().Name);
        //GameManager.Main.EnableInputCurrent();
    }
    public void disFreezPlayer()
    {
        GameManager.Main.ChangeState(YukinoMain.stateIdle);
        //GameManager.Main.DestroyInputCurrent();
        //GameManager.Main.EnableInput();
    }
    public void cameraInheritPosition(bool active)
    {
        GameManager.Main.CameraCs.yukinoCamera.m_Transitions.m_InheritPosition = active;
    }
    public void UnLookStageSound()//2=stage2 3=stage3 0=BossStage
    {
        GameManager.Sound.SetCueName("Unlock_Stage");
        GameManager.Sound.OnSound();
    }
    public void UnlookBoss()
    {
        GameManager.Sound.SetCueName("Unlock_Boss");
        GameManager.Sound.OnSound();
    }
}
