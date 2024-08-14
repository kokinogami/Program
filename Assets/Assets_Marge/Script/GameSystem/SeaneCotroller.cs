using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem.UI;


public class SeaneCotroller : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject StartButtons;//�������j���[�{�^��
    public GameObject StagetButtons;//�X�e�[�W�I���̃{�^��
    public GameObject Option;//�I�v�V�����̃{�^���ƃX���C�_�[
    public GameObject StaffRollUI;//�I�v�V�����̃{�^���ƃX���C�_�[
    public GameObject DebugModeUI;//�f�o�b�N�ƃT���v���X�e�[�W�p�̃{�^��
    public GameObject pressAnyBotton;//pressAnyBottonText
    public FadeIn_Out FadeScreen;//�t�F�[�h�A�E�g�p�̍������
    public Animator LogoAnim;//�^�C�g�����S�̃A�j���[�V����
    public Animator ImageAnim;//�^�C�g���w�i�̃A�j���[�V����
    public Animator StartButtonsAnim;//�X�^�[�g�{�^���̃A�j���[�V����
    public Animator StageButtonsAnim;//�X�e�[�W�I���{�^���̃A�j���[�V����
    public Animator OptionButtonsAnim;//�I�v�V�����{�^���̃A�j���[�V����
    public TitleUIController uIController;
    public static int a = 100;
    public Slider VolumeSlider;
    public Slider SensivitySlider;
    float Vnow = 20f;
    [System.NonSerialized] public static float Snow = 1f;
    string str;
    string str2;
    string sceneName;
    public Text text1;
    public Text text2;
    public Text screen_size_text;
    bool kirikae;
    bool debugMode;
    Image fadeinImage;
    [SerializeField] PlayerInput input;
    public static PlayerInput _input;
    [SerializeField] InputSystemUIInputModule eventSystema;
    float MoveVar;

    //FadeOutDebuga fade;
    private void Start()
    {
        DebugModeUI.SetActive(false);
        StartButtons.SetActive(false);
        StagetButtons.SetActive(false);
        Option.SetActive(false);
        StaffRollUI.SetActive(false);
        pressAnyBotton.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //fade = FadeScreen.GetComponent<FadeOutDebug>();


        //�X���C�_�[�̌��ݒl�̐ݒ�
        //VolumeSlider.value = Vnow;
        //SensivitySlider.value = Snow;

        str = Vnow.ToString();
        str2 = Snow.ToString();
        sceneName = SceneManager.GetActiveScene().name;

        _input = input;
        input = null;

        TryGetComponent(out fadeinImage);

        screen_size_text.text = $"{Screen.width}�~{Screen.height}";
    }
    private void Update()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)//�}�E�X�J�[�\���̕\������
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (sceneName == "MainMenu" && debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) FadeScreen.fadeOutBool("Debug");
            if (Input.GetKeyDown(KeyCode.Alpha1)) FadeScreen.fadeOutBool("Stage1");
            if (Input.GetKeyDown(KeyCode.Alpha2)) FadeScreen.fadeOutBool("Stage2");
            if (Input.GetKeyDown(KeyCode.Alpha3)) return;
            if (Input.GetKeyDown(KeyCode.Alpha4)) FadeScreen.fadeOutBool("Sample");
        }
        MoveVar = 1 - _input.actions["LeftStick"].ReadValue<float>();
        eventSystema.moveRepeatRate = MoveVar / 2;
    }
    void OpenButton()
    {
        Debug.Log("Start");
        if (Application.version.Contains("��") != false && Application.version.Contains("��") != false)
        {
            DebugModeUI.SetActive(true);
        }
        StartButtons.SetActive(true);
        StagetButtons.SetActive(false);
        Option.SetActive(false);
        StaffRollUI.SetActive(false);
        pressAnyBotton.SetActive(false);

        LogoAnim.SetBool("FirstToChoice", true);
        ImageAnim.SetBool("FirstToChoice", true);
        StartButtonsAnim.SetBool("FirstToChoice", true);
        StartButtonsAnim.Play("New State", 0, 0);
    }
    //�@�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        StartButtons.SetActive(false);
        StagetButtons.SetActive(true);
        StageButtonsAnim.SetBool("FirstToChoice", true);
        //uIController.SecondChangd();
        gamemanager.ClickSound();
    }

    public void ContinueGame()
    {
        if (gamemanager.data.tutorial1Clear == false)
        {
            return;
        }
        else if (gamemanager.data.tutorial1Clear == true && gamemanager.data.tutorial2Clear == false)
        {
            //Tutorial2Start();
            gamemanager.ClickSound();
            TutorialMainMap();
        }
        else
        {
            //StartGame();//��
            GoMainMap();
        }
    }

    public void NewGame()
    {
        gamemanager.SaveDataReset();
        //TutorialStart();
        TutorialMainMap();
    }

    void GoMainMap()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Mainmap");
    }
    void TutorialMainMap()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Stage_tutorial0");
    }

    //�@�Q�[���I���{�^��������������s����
    public void EndGame()
    {
        gamemanager.BackSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    public void DebugStart()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Debug");
        //SceneManager.LoadScene("Debug");
    }
    public void SampleStart()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Sample");
        //SceneManager.LoadScene("Sample");
    }
    public void TutorialStart()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Stage_tutorial1_verTGS");//"Stage_tutoria_verOC2");
        //SceneManager.LoadScene("Sample");
    }
    public void Tutorial2Start()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("TutorialStage2_verTGS");
        //SceneManager.LoadScene("Sample");
    }
    public void Stage1Start()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Stage1");
        //SceneManager.LoadScene("Sample");
    }
    public void Stage2Start()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Stage2");
        //SceneManager.LoadScene("Sample");
    }
    public void Stage3Start()
    {
        gamemanager.ClickSound();
        FadeScreen.fadeOutBool("Stage3");
        //SceneManager.LoadScene("Sample");
    }
    public void SDawdas()
    {
        Debug.Log("dawda");
    }
    public void Return()
    {
        gamemanager.BackSound();
        StartButtons.SetActive(true);
        StagetButtons.SetActive(false);
        Option.SetActive(false);
        StaffRollUI.SetActive(false);
        //uIController.StartChangd();

        StartButtonsAnim.SetBool("FirstToChoice", true);
        StartButtonsAnim.Play("New State", 0, 0);
    }

    public void OptionStart()//�I�v�V�����{�^��������������s����
    {
        gamemanager.ClickSound();
        StartButtons.SetActive(false);
        Option.SetActive(true);
        //uIController.OptionChangd();

        OptionButtonsAnim.SetBool("FirstToChoice", true);
    }
    public void OpenStaffRoll()
    {
        gamemanager.ClickSound();
        StartButtons.SetActive(false);
        StaffRollUI.SetActive(true);
        uIController.StaffRollChangd();
    }
    public void OnClickFullScreenMode()
    {
        gamemanager.ClickSound();
        if (Screen.fullScreen == false)
        {
            // �t���X�N���[�����[�h�ɐ؂�ւ��܂�
            Screen.fullScreen = true;
            //Debug.Log("10");
        }

        else if (Screen.fullScreen == true)
        {
            // �t���X�N���[�����[�h�ɐ؂�ւ��܂�
            Screen.fullScreen = false;
            //Debug.Log("20");
        }
    }

    public void OnClick_960x540()
    {
        gamemanager.ClickSound();
        if (kirikae == true)
        {
            Screen.SetResolution(960, 540, Screen.fullScreen);
            kirikae = false;
            //Debug.Log("100");
        }
        else if (kirikae == false)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
            kirikae = true;
            //Debug.Log("200");
        }
        screen_size_text.text = $"{Screen.width}�~{Screen.height}";
    }

    public void Text1()
    {
        Debug.Log(Vnow);
        Vnow = VolumeSlider.value;
        text1.text = Vnow.ToString();
    }

    public void Text2()
    {
        Debug.Log(Snow);
        Snow = SensivitySlider.value;
        text2.text = Snow.ToString();
    }
    public void openPressAnyBotton()//InputSysytem���蓖��
    {
        if (GameManager.titleBack == false)
        {
            pressAnyBotton.SetActive(true);
            _input.actions["AnyBottonKey"].started += PressAnyBottonInput;
        }
        else
        {
            //StartButtons.SetActive(true);
            //StagetButtons.SetActive(false);
            //Option.SetActive(false);
            //StaffRollUI.SetActive(false);
            //pressAnyBotton.SetActive(false);

            ////StageButtonsAnim.Play("FirstToChoice", 0, 0);
            ////StageButtonsAnim.SetBool("FirstToChoice", true);
            //StartButtonsAnim.SetBool("FirstToChoice", true);
            //StageButtonsAnim.Play("MoveStartButtons", 0, 0);

            //LogoAnim.SetBool("FirstToChoice", true);
            //ImageAnim.SetBool("FirstToChoice", true);


            ////uIController.SecondChangd();

            OpenButton();

            GameManager.titleBack = false;
        }
    }
    public void PressAnyBottonInput(InputAction.CallbackContext context)//AnyBotton����������̏���
    {
        OpenButton();
        _input.actions["AnyBottonKey"].started -= PressAnyBottonInput;
    }
}