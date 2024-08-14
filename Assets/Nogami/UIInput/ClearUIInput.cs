using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClearUIInput : MonoBehaviour
{
    const string backMainmap = "Submit";
    const string retry = "RetryGame";
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {

    }
    public void InputEnableClearUI()
    {
        if (GameManager.stageState == Stage.tutorial1)
        {
            GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started += NextTutorial2;
        }
        else if(GameManager.stageState == Stage.MainStage)
        {
        }
        else
        {
            //GameManager.Main._input.actions.FindActionMap("UI")[actionName].started += TitleBackcs;
            GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started += MainMapBackcs;
        }
        GameManager.Main._input.actions.FindActionMap("UI")[retry].started += RetryGame;
    }
    public void TitleBack()
    {
        GameManager.Main.fadeInOut.fadeOutBool("MainMenu");
        GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started -= TitleBackcs;
        GameManager.GM.UnLoadStage = true;
        GameManager.titleBack = true;
    }

    public void MainMapBack()//ステージクリア後、メインマップに戻る
    {
        GameManager.Main.fadeInOut.fadeOutBool("MainMap");
        GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started -= MainMapBackcs;
        GameManager.Main._input.actions.FindActionMap("UI")[retry].started -= RetryGame;
        GameManager.GM.UnLoadStage = true;
    }

    public void Retry()//ステージクリア後、リトライ
    {
        //GameManager.Main.DestroyInput();//this.GetType().Name);//操作不可処理
        //GameManager.Main.DestroyInputMenu();//操作不可処理
        GameManager.Main.fadeInOut.fadeOutBool(SceneManager.GetActiveScene().name);
        if (GameManager.stageState == Stage.tutorial1)
        {
            GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started -= NextTutorial2;
        }
        else if (GameManager.stageState == Stage.MainStage)
        {
        }
        else
        {
            //GameManager.Main._input.actions.FindActionMap("UI")[actionName].started += TitleBackcs;
            GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started -= MainMapBackcs;
        }
        GameManager.Main._input.actions.FindActionMap("UI")[retry].started -= RetryGame;
        Time.timeScale = 1f;
        GameManager.Sound.SetCueName("Retry");
        GameManager.Sound.OnSound();
        GameManager.ChangeSceenReset = true;
        GameManager.GM.UnLoadStage = true;
    }
    public void NextTutorial()
    {
        GameManager.Main.fadeInOut.fadeOutBool("TutorialStage2_verTGS");
        GameManager.Main._input.actions.FindActionMap("UI")[backMainmap].started -= NextTutorial2;
        GameManager.Main._input.actions.FindActionMap("UI")[retry].started += RetryGame;
        GameManager.GM.UnLoadStage = true;
        GameManager.titleBack = true;
    }
    public void TitleBackcs(InputAction.CallbackContext context)
    {
        TitleBack();
    }

    public void MainMapBackcs(InputAction.CallbackContext context)
    {
        MainMapBack();
    }

    public void NextTutorial2(InputAction.CallbackContext context)
    {
        NextTutorial();
    }

    public void RetryGame(InputAction.CallbackContext context)
    {
        Retry();
    }
}