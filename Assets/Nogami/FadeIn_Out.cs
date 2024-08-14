using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn_Out : MonoBehaviour
{
    Animator animator;
    [System.NonSerialized] string selectedScene = "";

    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;

    //　読み込み率を表示するスライダー
    [SerializeField] private Slider slider;

    [SerializeField] SeaneCotroller seaneCotroller;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out animator);
        animator.SetBool("FadeIn", true);
        selectedScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void fadeOutBool(string seenName)
    {
        if (GameManager.stageState != Stage.Title)
        {
            GameManager.Main.DestroyInput();// this.GetType().Name);//操作不可処理
            GameManager.Main.DestroyInputMenu();//操作不可処理
        }
        GameManager.GM.ChangeGamestate(GameState.fadeInOut);
        fadeOutSet();
        selectedScene = seenName;
        animator.SetBool("FadeOut", true);
    }
    public void SetFadeIn()//フェードイン終了後処理
    {
        GameManager.GM.ChangeGamestate(GameState.Nomal);
        GameManager.ChangeSceenReset = false;
        slider.value = 0;
        GetComponent<Image>().enabled = false;
        if (GameManager.stageState != Stage.Title)
        {
            //GameManager.Main.DestroyInputCurrent();
            GameManager.Main.EnableInput();
            GameManager.Main._input.SwitchCurrentActionMap("Player");
            StartCoroutine("StartCmaera");
            Time.timeScale = 1;
        }
        if (GameManager.stageState == Stage.MainStage && GameManager.retryBoss == false)
        {
            if (GameManager.MainmapUnlock == false) { }
            else if (GameManager.stage1Clear && GameManager.isStage2Unlock == false) { }
            else if (GameManager.stage2Clear && GameManager.isStage3Unlock == false) { }
            else if (GameManager.stage3Clear && GameManager.isBossUnlock == false){ }
            else
            {
                GameManager.Sound.SetCueName("Mainmap");
                GameManager.Sound.OnBGM();
            }
        }
        GameManager.retryBoss = false;
    }
    IEnumerator StartCmaera()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Main.CameraCs.CameraMove(true);
        yield break;
    }
    public void fadeOutSet()
    {
        GetComponent<Image>().enabled = true;
    }
    public void TitleAnimationEvent()
    {
        seaneCotroller.openPressAnyBotton();
        GameManager.Sound.SetCueName("Title");
        GameManager.Sound.OnBGM();
    }
    public void TitleFinEvent()
    {
        GameManager.Sound.StopBGM();
    }
    public IEnumerator LoadData()
    {
        // シーンの読み込みをする
        GameManager.Sound.enabled = false;
        async = SceneManager.LoadSceneAsync(selectedScene, LoadSceneMode.Single);

        async.allowSceneActivation = false;
        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            if (async.progress >= 0.9f)
            {
                //GameManager.GM.SceneUnloaded();
                async.allowSceneActivation = true;

            }
            yield return null;
        }
        yield break;
    }
}
