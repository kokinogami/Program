using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerRetry : MonoBehaviour
{


    public FadeIn_Out FadeScreen;//フェードアウト用の黒い画面
    public GameObject OptionMenue;
    public GameObject OptionMenueStart;
    public GameObject exImage;
    public GameObject DefaultPause;
    [SerializeField] bool gameoverUI;

    GameObject fadein;
    // Start is called before the first frame update
    private void Start()
    {

    }
    private void Update()
    {
        if (gameoverUI) return;
        if (DefaultPause.activeSelf == false && OptionMenue.activeSelf == false)
        {
            DefaultPause.SetActive(true);
            exImage.SetActive(true);
        }
    }
    private void OnEnable()
    {

    }
    public void BackGame()
    {
        GameManager.Sound.SetCueName("Menu_Close");
        GameManager.Sound.OnSound();
        //Gamemanager.ChangeGamestate(GameManager.backGameState);
        GameManager.GM.ClosePauseMenu();
        GameManager.Main._input.SwitchCurrentActionMap("Player");
        GameManager.Main.CameraCs.CameraMove(true);
    }
    public void Retry()
    {
        //GameManager.Main.DestroyInput();//this.GetType().Name);//操作不可処理
        //GameManager.Main.DestroyInputMenu();//操作不可処理
        FadeScreen.fadeOutBool(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameManager.Sound.SetCueName("Retry");
        GameManager.Sound.OnSound();
        GameManager.ChangeSceenReset = true;
        GameManager.GM.UnLoadStage = true;
    }
    public void Option()
    {
        openDefaultPause(false);
        GameManager.GM.UIopen(0, GameState.pouse, OptionMenue, OptionMenueStart);
        exImage.SetActive(false);
        //firestpouse.SetActive(tr)
    }
    public void CloseOption()
    {
        openDefaultPause(true);
        GameManager.GM.UIclose();
        exImage.SetActive(true);
    }

    public void openDefaultPause(bool active)
    {
        DefaultPause.SetActive(active);
    }
    public void TitleBack()
    {
        GameManager.Main.DestroyInputMenu();
        string backStage = "";
        if (GameManager.stageState == Stage.MainStage|| GameManager.stageState == Stage.tutorial0)
        {
            backStage = "MainMenu";
            GameManager.Sound.StopBGM();
        }
        else if (GameManager.stageState == Stage.tutorial1 || GameManager.stageState == Stage.tutorial2)
        {
            backStage = "Stage_tutorial0";
        }
        else
        {

            backStage = "Mainmap";
        }


        FadeScreen.fadeOutBool(backStage);
        Time.timeScale = 1f;
        GameManager.Sound.SetCueName("Title_Back");
        GameManager.Sound.OnSound();
        GameManager.GM.UnLoadStage = true;
        GameManager.titleBack = true;
    }

    public void SelectSound()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
        {
            GameManager.Sound.SetCueName("Cursor");
            GameManager.Sound.OnSound();
        }
    }
    public void PointerEnterSound()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.KeyboardMouse)
        {
            GameManager.Sound.SetCueName("Cursor");
            GameManager.Sound.OnSound();
        }
    }
    public void retryBoss()
    {
        GameManager.retryBoss = true;
        //GameManager.Main.DestroyInput();//this.GetType().Name);//操作不可処理
        //GameManager.Main.DestroyInputMenu();//操作不可処理
        FadeScreen.fadeOutBool(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameManager.Sound.SetCueName("Retry");
        GameManager.Sound.OnSound();
        GameManager.ChangeSceenReset = true;
        GameManager.GM.UnLoadStage = true;
    }
}
