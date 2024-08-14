using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn_Out : MonoBehaviour
{
    Animator animator;
    [System.NonSerialized] string selectedScene = "";

    //�@�񓯊�����Ŏg�p����AsyncOperation
    private AsyncOperation async;

    //�@�ǂݍ��ݗ���\������X���C�_�[
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
            GameManager.Main.DestroyInput();// this.GetType().Name);//����s����
            GameManager.Main.DestroyInputMenu();//����s����
        }
        GameManager.GM.ChangeGamestate(GameState.fadeInOut);
        fadeOutSet();
        selectedScene = seenName;
        animator.SetBool("FadeOut", true);
    }
    public void SetFadeIn()//�t�F�[�h�C���I���㏈��
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
        // �V�[���̓ǂݍ��݂�����
        GameManager.Sound.enabled = false;
        async = SceneManager.LoadSceneAsync(selectedScene, LoadSceneMode.Single);

        async.allowSceneActivation = false;
        //�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
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
