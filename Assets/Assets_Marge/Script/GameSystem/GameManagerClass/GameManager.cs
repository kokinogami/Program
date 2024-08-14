using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public enum Stage
{
    Title,
    MainStage,
    Stage1,
    Stage2,
    Stage3,
    tutorial1,
    tutorial2,
    tutorial0,
    Debug,
    Sample,
}
public enum GameState
{
    Nomal,
    pouse,
    gameover,
    gameclera,
    fadeInOut
}
public partial class GameManager : MonoBehaviour
{
    [System.NonSerialized] public static Stage stageState = Stage.Title;
    [System.NonSerialized] public Stage stageStateBefore;
    [System.NonSerialized] public static GameState gameState = GameState.Nomal;
    [System.NonSerialized] public static GameState backGameState;
    [System.NonSerialized] public static GameManager GM;
    [System.NonSerialized] public static bool startBGM = false;
    [System.NonSerialized] public static bool onTimeCount = false;
    [System.NonSerialized] public static bool retryBoss = false;
    [System.NonSerialized] public static int debugCount = 0;
    [SerializeField] Stage Defultstate;
    public GameObject ClearUI;//�g�p���Ȃ�
    public static float TimeCount;
    public Text TimeText;
    public Text ClearText;
    public Text Rank1Time;
    public Text Rank2Time;
    public Text Rank3Time;
    public Text Rank4Time;
    public Text Rank5Time;
    public Text EnemyCountText;
    public static float minutes;
    private bool Result;
    private float BeforeTimescale;
    [SerializeField] private float BaseTime = 60;//�N���A�^�C���̊�@���5��̃^�C�������݂��Ȃ��ꍇ�A���̃^�C������30�b���݂Ń^�C�����쐬
    private float[] RankedTime = new float[6];//���5��̃^�C���ƍ���̃^�C�����i�[
    private string currentscene;//�N���A�^�C�����X�e�[�W�ʂɕ����ĕۑ����邽�߂Ɏg�p
    float MoveVar;
    [SerializeField] private RectTransform zone;
    [SerializeField] private GameObject resultObj;
    public GameObject explanationUI;
    Vector3 A;

    //�N���A�]���p
    [SerializeField] private float TimeS = 120f;
    [SerializeField] private float TimeA = 180f;
    [SerializeField] private float TimeB = 360f;
    [SerializeField] private float TimeC = 600f;
    [SerializeField] private GameObject RankS;
    [SerializeField] private GameObject RankA;
    [SerializeField] private GameObject RankB;
    [SerializeField] private GameObject RankC;

    //�@�|�[�Y�������ɕ\������UI�̃v���n�u
    [SerializeField] private GameObject pauseUIPrefab;
    [SerializeField] private GameObject pauseUIFirestButton;
    //�@�|�[�YUI�̃C���X�^���X
    [System.NonSerialized] public GameObject pauseUIInstance;
    [System.NonSerialized] public GameObject pauseUIButton;
    //
    //[SerializeField]
    [System.NonSerialized] public GameObject PauseFirstButton;
    public InputSystemUIInputModule InputSystemUI;
    [System.NonSerialized] public bool StartSet = false;
    [System.NonSerialized] public bool UnLoadStage = false;
    [System.NonSerialized] public bool BossBattle = false;

    [SerializeField]
    //�Q�[���I�[�o�[���ɕ\������UI�̃v���n�u
    private GameObject gameOverUIPrefab;
    private GameObject gameOverUIButton;
    private GameObject gameOverUIInstance;

    //[SerializeField]
    [SerializeField] GameObject gameOverFirstButton;

    [System.NonSerialized] public GameObject firstButton;
    public GameObject SoundSlider;
    public GameObject SoundSliderButton;

    //�\��UI�ƍŏ��̃{�^���ۑ��p���X�g
    public List<GameObject> UIOpenList;
    public List<GameObject> UIFirestButtonList;
    //Testmode
    public bool testmode;

    [SerializeField] TimelineInstanceBossBefor TimelineInstanceBossBefor;

    public Boss boss;

    // Start is called before the first frame update
    private void Awake()
    {
        SaveAwake();
        Application.targetFrameRate = Maxfps;
        onTimeCount = false;
        stageStateBefore = stageState;
        stageState = Defultstate;
        DataBaseStart();//static������
        //SceneManager.sceneUnloaded += SceneUnloaded;
        TryGetComponent(out GM);
        startBGM = false;
        if (stageState == Stage.Title) startBGM = true;
        if (SoundSlider)
        {
            SoundSlider.SetActive(false);
        }
        Editor();

        if (stageState == Stage.Title) return;

        EnemyCountText.text = EnemyCount.ToString();
    }
    void Start()
    {
        if (stageState == Stage.Title) return;
        resultObj.SetActive(false);
        TimeCounterStary();//���Ԃ̏�����
        PauseUIStart();
        currentscene = SceneManager.GetActiveScene().name;

        float[] saveClearTime = new float[5];//�Z�[�u�f�[�^���̃N���A�^�C���i�[�p
        switch (stageState)//�Z�[�u�f�[�^���̃N���A�^�C���i�[
        {
            case (Stage.tutorial1):
                saveClearTime = data.t1Time;
                break;
            case (Stage.tutorial2):
                saveClearTime = data.t2Time;
                break;
            case (Stage.Stage1):
                saveClearTime = data.s1Time;
                break;
            case (Stage.Stage2):
                saveClearTime = data.s2Time;
                break;
            case (Stage.Stage3):
                saveClearTime = data.s3Time;
                break;
            default:
                break;
        }

        for (int i = 0; i < 5; i++)
        {
            /*if (PlayerPrefs.GetString(currentscene + (i + 1).ToString()) == "")//prefs�g�p��
            {
                RankedTime[i] = BaseTime + 60.0f * (i + 1);
            }
            else
            {
                RankedTime[i] = PlayerPrefs.GetFloat(currentscene + "Rank" + (i + 1).ToString());
            }*/
            if (saveClearTime[i] == 0)//�Z�[�u�f�[�^����0�̎��f�t�H���g����
            {
                if (stageState == Stage.MainStage)
                {
                    RankedTime[i] = BaseTime + 30.0f * (i + 8);
                }
                else
                {
                    RankedTime[i] = BaseTime + 60.0f * (i + 1);
                }
            }
            else//�Z�[�u�f�[�^������
            {
                RankedTime[i] = saveClearTime[i];
            }
        }

        MainStagePos();//���C���}�b�v�ɖ߂����Ƃ��̏ꏊ
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(gameState);
        // Debug.Log(Time.timeScale);

        SliderController();

        StartBGM();

        if (stageState == Stage.Title) return;

        CurrentUIUpdate();

        cheatcode();

        CursorNow();//�J�[�\���̕\������

        TimeCounter();//���Ԍv���{�N���A���̕\��
    }
    public void SceneUnloaded()//Scene thisScene)
    {
        GameManager.Sound.StopBGM();
        GameManager.Sound.StopSound();
    }
    public static void EnemyKill()//�G�l�~�[���|���ꂽ�Ƃ�
    {
        EnemyCount -= 1;
        GameManager.GM.EnemyCountText.text = EnemyCount.ToString();
    }
    void TimeCounterStary()//���Ԃ̏�����
    {
        TimeCount = 0.0f;
        minutes = 0;
        if (TimeCount > 60.0f)
        {
            minutes = Mathf.Floor(TimeCount / 60.0f);
            float lostcount = minutes * 60;
            TimeCount += lostcount;
        }
    }

    void TimeCounter()//���Ԍv���{�N���A���̕\��
    {
        A = new Vector3(Screen.width / 2 + 320 /*- 30*/, Screen.height / 2 + 60 /*- 30, 0.0f*/);
        //Debug.Log(A);
        if (TimeCount > 60.0f)
        {
            minutes += 1;
            TimeCount -= 60.0f;
        }
        TimeText.text = string.Format("{0:00.00}", TimeCount);
        /*GetComponent<Text>()*/
        TimeText.text = minutes + ":" + string.Format("{0:00.00}", TimeCount);

        if (Main.fadeIn.color.a <= 0)
        {
            if (stageState != Stage.MainStage && stageState != Stage.tutorial0)
            {
                if (EnemyCount != 0)
                {
                    if (onTimeCount && Time.timeScale != 0) TimeCount += Time.unscaledDeltaTime;
                }
                else if (Result == false)//�N���A������
                {
                    Main.CameraCs.CameraMove(false);
                    GameManager.Sound.SetCueName("Filed1");
                    GameManager.Sound.StopBGM();
                    GameManager.Sound.SetCueName("Clear");
                    GameManager.Sound.OnBGM();
                    GameManager.Sound.StopSound();
                    GameManager.Main.DestroyInput();//this.GetType().Name);
                    GameManager.Main.EnableInputCurrent();
                    Main.DestroyInputMenu();
                    ChangeGamestate(GameState.gameclera);
                    Main._input.SwitchCurrentActionMap("UI");
                    InputSystemUI.enabled = true;
                    //ClearUI.SetActive(true);
                    BeforeTimescale = Time.timeScale;
                    firstButton = GameObject.Find("TitlebackButton");
                    //firstButton = PauseFirstButton;
                    if (currentInputHardware == InputHardware.GamePad)//Main.InControllerInput)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(firstButton);
                        StartSet = true;
                    }
                    Time.timeScale = 0.0f;
                    resultObj.SetActive(true);
                    //zone.position = A;//���o�ύX�ɂ��폜

                    ClearText.text = minutes + ":" + string.Format("{0:00.00}", TimeCount);

                    RankedTime[5] = 60 * minutes + TimeCount;

                    if (RankedTime[5] <= TimeS)
                    {
                        RankS.SetActive(true);
                    }
                    else if (RankedTime[5] <= TimeA)
                    {
                        RankA.SetActive(true);
                    }
                    else if (RankedTime[5] <= TimeB)
                    {
                        RankB.SetActive(true);
                    }
                    else if (RankedTime[5] <= TimeC)
                    {
                        RankC.SetActive(true);
                    }

                    System.Array.Sort(RankedTime);
                    float[] saveClearTime = new float[5];//�N���A�^�C���i�[�p
                    for (int i = 0; i < 5; i++)
                    {
                        float resultminutes = MathF.Floor(RankedTime[i] / 60);
                        float resultseconds = RankedTime[i] - resultminutes * 60;
                        string ClearTime = resultminutes.ToString() + ":" + string.Format("{0:00.00}", resultseconds);

                        switch (stageState)
                        {
                            case (Stage.tutorial1):
                                data.t1Time[i] = RankedTime[i];
                                break;
                            case (Stage.tutorial2):
                                data.t2Time[i] = RankedTime[i];
                                break;
                            case (Stage.Stage1):
                                data.s1Time[i] = RankedTime[i];
                                break;
                            case (Stage.Stage2):
                                data.s2Time[i] = RankedTime[i];
                                break;
                            case (Stage.Stage3):
                                data.s3Time[i] = RankedTime[i];
                                break;
                            default:
                                break;
                        }

                        //PlayerPrefs.SetFloat(currentscene + "Rank" + (i + 1).ToString(), RankedTime[i]);
                        //PlayerPrefs.SetString(currentscene + (i + 1).ToString(), ClearTime);
                        if (i == 0)
                        {
                            Rank1Time.text = "1 " + ClearTime;
                        }

                        if (i == 1)
                        {
                            Rank2Time.text = "2 " + ClearTime;
                        }

                        if (i == 2)
                        {
                            Rank3Time.text = "3 " + ClearTime;
                        }

                        if (i == 3)
                        {
                            Rank4Time.text = "4 " + ClearTime;
                        }

                        if (i == 4)
                        {
                            Rank5Time.text = "5 " + ClearTime;
                        }
                    }
                    ClearStage();//�N���A�X�e�[�W����
                    Result = true;
                }
            }
            else if (BossBattle && Time.timeScale != 0)
            {
                TimeCount += Time.unscaledDeltaTime;
            }
        }
    }
    void ClearStage()
    {
        switch (stageState)
        {
            case (Stage.Stage1):
                stage1Clear = true;
                break;
            case (Stage.Stage2):
                stage2Clear = true;
                break;
            case (Stage.Stage3):
                stage3Clear = true;
                break;
            case (Stage.tutorial1):
                tutorial1Clear = true;
                break;
            case (Stage.tutorial2):
                tutorial2Clear = true;
                break;
            default:
                break;
        }
        Save(data);
    }
    void PauseUIStart()
    {
        InputSystemUI.enabled = false;
    }
    void CurrentUIUpdate()//���̓R���g���[���[�ɂ���ă}�E�X�J�[�\����ω�������
    {
        if (currentInputHardware == InputHardware.GamePad && StartSet == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (UIFirestButtonList.Count != 0)
            {
                EventSystem.current.SetSelectedGameObject(UIFirestButtonList[UIFirestButtonList.Count - 1]);
            }
            StartSet = true;
        }
        if (currentInputHardware == InputHardware.KeyboardMouse)
        {
            EventSystem.current.SetSelectedGameObject(null);
            StartSet = false;
        }
    }
    public void UIopen(float timescale, GameState gamestate, GameObject UIObject, GameObject ButtonObject)//UI��\��������i���ԑ��x,��ʃX�e�[�g,�\��������UI,UI���̃{�^���j
    {
        //��ʃX�e�[�g�ύX
        if (gameState != gamestate)
        {
            ChangeGamestate(gamestate);
            BeforeTimescale = Time.timeScale;//���ԕۑ����ύX
            Time.timeScale = timescale;
        }
        //Debug.Log(gameState);
        //�C�x���gSystem�L����
        InputSystemUI.enabled = true;

        //�\��UI����ۑ�
        UIOpenList.Add(UIObject);
        UIFirestButtonList.Add(ButtonObject);

        UIObject.SetActive(true);        //UI�\��

        if (currentInputHardware == InputHardware.GamePad)      //�{�^���Z�b�g
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(ButtonObject);
            StartSet = true;
        }

        GameManager.Main.DestroyInput();// this.GetType().Name);
        GameManager.Main.EnableInputCurrent();

        //���Đ�
        //GameManager.Sound.StopSound();
        GameManager.Sound.SetCueName("Menu_Open");
        GameManager.Sound.OnSound();
    }
    public void UIclose()
    {
        GameManager.Sound.SetCueName("Menu_Close");
        GameManager.Sound.OnSound();
        UIOpenList[UIOpenList.Count - 1].SetActive(false);
        UIOpenList.RemoveAt(UIOpenList.Count - 1);
        UIFirestButtonList.RemoveAt(UIFirestButtonList.Count - 1);
        if (UIFirestButtonList.Count != 0)
        {
            if (currentInputHardware == InputHardware.GamePad)      //�{�^���Z�b�g
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(UIFirestButtonList[UIFirestButtonList.Count - 1]);
                StartSet = true;
            }
        }
        else
        {
            InputSystemUI.enabled = false;
            ChangeGamestate(GameManager.backGameState);
            Time.timeScale = BeforeTimescale;
            GameManager.Main.DestroyInputCurrent();
            GameManager.Main.EnableInput();
            Main._input.SwitchCurrentActionMap("Player");
            Main.CameraCs.CameraMove(true);
        }
    }

    public void PauseMenu()
    {
        UIopen(0, GameState.pouse, SoundSlider, SoundSliderButton);
        //if (SoundSlider)
        //{
        //    soundtst();
        //}
        //else if (pauseUIInstance == null)
        //{
        //    InputSystemUI.enabled = true;
        //    pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
        //    Time.timeScale = 0f;
        //    PauseFirstButton = GameObject.Find("RetryButton");
        //    firstButton = PauseFirstButton;
        //    if (currentInputHardware ==InputHardware.Controller)
        //    {
        //        EventSystem.current.SetSelectedGameObject(null);
        //        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
        //        StartSet = true;
        //    }
        //    GameManager.Main.DestroyInput();//this.GetType().Name);
        //    GameManager.Main.EnableInputCurrent();
        //    GameManager.Sound.StopSound();
        //    GameManager.Sound.SetCueName("Menu_Open");
        //    GameManager.Sound.OnSound();
        //}
        //else
        //{
        //    InputSystemUI.enabled = false;
        //    Destroy(pauseUIInstance);
        //    Destroy(pauseUIInstance);
        //    PauseFirstButton = null;
        //    firstButton = null;
        //    Time.timeScale = 1f;
        //    GameManager.Main.DestroyInputCurrent();
        //    GameManager.Main.EnableInput();
        //}
    }
    public void ClosePauseMenu()
    {
        UIclose();
    }

    void soundtst()
    {
        if (pauseUIInstance == null)
        {
            InputSystemUI.enabled = true;
            SoundSlider.SetActive(true);
            pauseUIInstance = SoundSlider;
            BeforeTimescale = Time.timeScale;
            Time.timeScale = 0f;
            PauseFirstButton = GameObject.Find("RetryButton");
            firstButton = PauseFirstButton;
            if (currentInputHardware == InputHardware.GamePad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(PauseFirstButton);
                StartSet = true;
            }
            GameManager.Main.DestroyInput();// this.GetType().Name);
            GameManager.Main.EnableInputCurrent();
            GameManager.Sound.StopSound();
            GameManager.Sound.SetCueName("Menu_Open");
            GameManager.Sound.OnSound();
        }
        else
        {
            InputSystemUI.enabled = false;
            SoundSlider.SetActive(false);
            pauseUIInstance = null;
            PauseFirstButton = null;
            firstButton = null;
            Time.timeScale = BeforeTimescale;
            GameManager.Main.DestroyInputCurrent();
            GameManager.Main.EnableInput();
        }
    }

    public void GameOverMenu()
    {
        UIopen(0, GameState.gameover, gameOverUIPrefab, gameOverFirstButton);
        //if (gameOverUIInstance == null)
        //{
        //    Sound.StopBGM();
        //    Main.DestroyInputMenu();
        //    ChangeGamestate(GameState.gameover);
        //    InputSystemUI.enabled = true;
        //    pauseUIInstance = GameObject.Instantiate(gameOverUIPrefab) as GameObject;
        //    Time.timeScale = 0f;
        //    gameOverFirstButton = GameObject.Find("gameOverFirstButton");
        //    Debug.Log(gameOverFirstButton);
        //    firstButton = gameOverFirstButton;
        //    if (currentInputHardware == InputHardware.GamePad)
        //    {
        //        EventSystem.current.SetSelectedGameObject(null);
        //        EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
        //        StartSet = true;
        //    }
        //}
        //else
        //{
        //    Main.EnableInputMenu();
        //    InputSystemUI.enabled = false;
        //    Destroy(gameOverUIInstance);
        //    gameOverFirstButton = null;
        //    firstButton = null;
        //    Time.timeScale = 1f;
        //}
    }
    public void explanationOpen()
    {
        if (stageState == Stage.MainStage || stageState == Stage.tutorial0) return;

        gameState = GameState.pouse;
        Main._input.SwitchCurrentActionMap("UI");
        Main.CameraCs.CameraMove(false);
        InputSystemUI.enabled = true;
        explanationUI.SetActive(true);
        BeforeTimescale = Time.timeScale;
        Time.timeScale = 0f;
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += ExplanationInput;
        //firstButton = GameObject.Find("CloseButton");
        //firstButton = PauseFirstButton;
        //if (currentInputHardware == InputHardware.Controller)
        //{
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(firstButton);
        //    StartSet = true;
        //}
    }
    void CursorNow()
    {
        if (gameState == GameState.Nomal || gameState == GameState.fadeInOut || currentInputHardware == InputHardware.GamePad)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ChangeGamestate(GameState State)
    {
        if (State == GameState.pouse)
        {
            backGameState = gameState;
        }
        gameState = State;
    }
    void StartBGM()
    {
        if (startBGM == false) return;

        bool StBg = false;
        if (Main) StBg = Main.Ground;
        else { StBg = true; }

        if (StBg == false) return;

        switch (stageState)
        {
            case (Stage.Title):
                Sound.SetCueName("");
                //Sound.OnBGM();
                break;
            case (Stage.Stage1):
                Sound.SetCueName("Filed1");
                Sound.OnBGM();
                break;
            case (Stage.Stage2):
                Sound.SetCueName("Filed2");
                Sound.OnBGM();
                break;
            case (Stage.Stage3):
                Sound.SetCueName("Filed3");
                Sound.OnBGM();
                break;
            case (Stage.tutorial1):
                Sound.SetCueName("Tutorial_1");
                Sound.OnBGM();
                break;
            case (Stage.tutorial2):
                Sound.SetCueName("Tutorial_2");
                Sound.OnBGM();
                break;
            case (Stage.MainStage):
                Sound.SetCueName("Mainmap");
                Sound.OnBGM();
                break;
            default:
                Sound.SetCueName("");
                Sound.OnBGM();
                break;
        }
        StartCoroutine("OnStageBGM");
        startBGM = false;
    }
    IEnumerator OnStageBGM()
    {
        while (GameManager.GM.UnLoadStage == false)
        {
            yield return new WaitForSecondsRealtime(1);
            GameManager.Sound.BGM_Block_Next();
        }
        SceneUnloaded();
        yield break;
    }
    public void Explanation()
    {
        StartPlatform.countdown = true;
        GameManager.Sound.SetCueName("321");
        GameManager.Sound.OnSound();
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= ExplanationInput;
        InputSystemUI.enabled = false;
        ChangeGamestate(GameState.Nomal);
        Time.timeScale = 1f;
        explanationUI.SetActive(false);
        Main.CameraCs.CameraMove(true);
        Main.ChangeIdele();

        firstButton = null;
        Time.timeScale = 1;

        StartTimer.startcount = true;

        Main._input.SwitchCurrentActionMap("Player");
        Sound.SetCueName("No");
        Sound.OnSound();
    }
    public void ExplanationInput(InputAction.CallbackContext context)
    {
        Explanation();
    }
    void SliderController()//�X�e�B�b�N�̓|�����p�x�ŃX���C�_�[�̈ړ����x�ω�
    {
        if (InputSystemUI.enabled == false) return;
        try
        {
            MoveVar = 1 - Main._input.actions["LeftStick"].ReadValue<float>();
        }
        catch (NullReferenceException)
        {
            MoveVar = 1 - SeaneCotroller._input.actions["LeftStick"].ReadValue<float>();
        }
        InputSystemUI.moveRepeatRate = MoveVar / 2;
    }

    void MainStagePos()
    {
        Debug.Log(stageStateBefore);
        if (Defultstate != Stage.MainStage) { return; }
        switch (stageStateBefore)
        {
            case (Stage.Stage1):
                Main.transform.position = new Vector3(0.1750545f, -92.0542f, -522.6597f);
                Main.transform.eulerAngles = new Vector3(0, 180, 0);

                Main.Hisora.transform.position = Main.transform.position + transform.forward * 2 + Vector3.up * 0.7f;

                Main.CameraCs.yukinoCamera_POV.m_VerticalAxis.Value = 10.09993f;
                Main.CameraCs.yukinoCamera_POV.m_HorizontalAxis.Value = 7.629395e-05f;

                break;

            case (Stage.Stage2):
                Main.transform.position = new Vector3(-433.8f, -22.06413f, -264.36f);
                Main.transform.eulerAngles = new Vector3(0, 180, 0);

                Main.Hisora.transform.position = Main.transform.position + transform.forward * 2 + Vector3.up * 0.7f;

                Main.CameraCs.yukinoCamera_POV.m_VerticalAxis.Value = -7.500067f;
                Main.CameraCs.yukinoCamera_POV.m_HorizontalAxis.Value = 0;
                break;

            case (Stage.Stage3):
                Main.transform.position = new Vector3(-446.9116f, 47.94624f, 244.4321f);
                Main.transform.eulerAngles = new Vector3(0, -120, 0);

                Main.Hisora.transform.position = Main.transform.position + transform.forward * 2 + Vector3.up * 0.7f;

                Main.CameraCs.yukinoCamera_POV.m_VerticalAxis.Value = 0;
                Main.CameraCs.yukinoCamera_POV.m_HorizontalAxis.Value = 60;
                break;

            case (Stage.MainStage):
                if (retryBoss == true)
                {
                    //retryBoss = false;
                    Main.transform.position = new Vector3(0, 6.6f, 0);
                    Main.transform.eulerAngles = new Vector3(0, 180, 0);

                    Main.Hisora.transform.position = Main.transform.position + transform.forward * 2 + Vector3.up * 0.7f;

                    Main.CameraCs.yukinoCamera_POV.m_VerticalAxis.Value = -12.77998f;
                    Main.CameraCs.yukinoCamera_POV.m_HorizontalAxis.Value = 172.9f;
                    TimelineInstanceBossBefor.retryStartBoss();
                }
                break;
            default:
                break;
        }
    }
    void cheatcode()//�`�[�g�R�[�h
    {
        if (Input.GetKeyUp(KeyCode.I) && Main.DebugMode)
        {
            UIopen(0.0f, GameState.pouse, SoundSlider, SoundSlider);
        }
        if (Input.GetKeyUp(KeyCode.U) && Main.DebugMode)
        {
            UIOpenList.RemoveAt(UIOpenList.Count - 1);
            UIFirestButtonList.RemoveAt(UIFirestButtonList.Count - 1);
        }
        if (Main.DebugMode)
        {
            if (Input.GetKey(KeyCode.P))//�G�l�~�[�S����
            {
                for (int i = 0; i < AllEnemy.Count; i++)
                {
                    Destroy(AllEnemy[i].gameObject); //�I�u�W�F�N�g�̍폜
                }
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Main.OnDamage(100, false);
            }
        }
        //if (Input.GetKey(KeyCode.Y) && Main.DebugMode)
        //{
        //    Debug.Log("3Daudio");
        //    Sound.SetCueName("3D_Enemy_Pop");
        //    //Sound.OnSound();
        //    Sound.WindNextBlock();
        //}
    }
    public void BossClearUI()
    {
        Main.CameraCs.CameraMove(false);
        //Sound.SetCueName("Filed1");
        //Sound.StopBGM();
        //Sound.SetCueName("Clear");
        //Sound.OnBGM();
        //Sound.StopSound();
        ChangeGamestate(GameState.gameclera);
        InputSystemUI.enabled = true;
        //ClearUI.SetActive(true);
        BeforeTimescale = Time.timeScale;
        firstButton = GameObject.Find("TitlebackButton");
        //firstButton = PauseFirstButton;
        if (currentInputHardware == InputHardware.GamePad)//Main.InControllerInput)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton);
            StartSet = true;
        }
        //Time.timeScale = 0.0f;
        //resultObj.SetActive(true);
        //zone.position = A;//���o�ύX�ɂ��폜

        ClearText.text = minutes + ":" + string.Format("{0:00.00}", TimeCount);

        RankedTime[5] = 60 * minutes + TimeCount;

        RankS.SetActive(false);
        RankA.SetActive(false);
        RankB.SetActive(false);
        RankC.SetActive(false);

        if (RankedTime[5] <= TimeS)
        {
            RankS.SetActive(true);
        }
        else if (RankedTime[5] <= TimeA)
        {
            RankA.SetActive(true);
        }
        else if (RankedTime[5] <= TimeB)
        {
            RankB.SetActive(true);
        }
        else if (RankedTime[5] <= TimeC)
        {
            RankC.SetActive(true);
        }

        System.Array.Sort(RankedTime);
        float[] saveClearTime = new float[5];//�N���A�^�C���i�[�p
        for (int i = 0; i < 5; i++)
        {
            float resultminutes = MathF.Floor(RankedTime[i] / 60);
            float resultseconds = RankedTime[i] - resultminutes * 60;
            string ClearTime = resultminutes.ToString() + ":" + string.Format("{0:00.00}", resultseconds);

            data.BossTime[i] = RankedTime[i];

            if (i == 0)
            {
                Rank1Time.text = "1 " + ClearTime;
            }

            if (i == 1)
            {
                Rank2Time.text = "2 " + ClearTime;
            }

            if (i == 2)
            {
                Rank3Time.text = "3 " + ClearTime;
            }

            if (i == 3)
            {
                Rank4Time.text = "4 " + ClearTime;
            }

            if (i == 4)
            {
                Rank5Time.text = "5 " + ClearTime;
            }
        }
        BossClear = true;//�N���A�X�e�[�W����
        Result = true;
        Save(data);
    }
    void Editor()
    {
#if UNITY_EDITOR
        if (stageState == Stage.Title) return;
        PlayerPrefs.DeleteAll(); //�n�C�X�R�A�̃��Z�b�g������Ƃ��̂ݎg�p
        Main.DebugMode = true;
#else

#endif
    }
}
