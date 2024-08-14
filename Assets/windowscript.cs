using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class windowscript : MonoBehaviour
{
    [SerializeField] GameObject BottomVideowindow;
    [SerializeField] GameObject Botton;

    int pageNum;
    int maxPageNum;

    float PageChangeCoolTime;
    float PageStartTime;
    public float Speed = 1;

    [SerializeField] List<VideoPlayerCs> tutorialList;

    bool OpenWind = true;
    bool OnConrole = false;
    bool ChangePageFirst = true;

    string stateName;

    [System.NonSerialized] public Animator animator;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && OpenWind)
        {
            OpenWind = false;
            BottomVideowindow.SetActive(true);
            tutorialList[pageNum].videoPlayer.Play();
            GameManager.GM.InputSystemUI.enabled = true;
            Time.timeScale = 0.0f;
            GameManager.GM.ChangeGamestate(GameState.pouse);
            GameManager.Main._input.SwitchCurrentActionMap("UI");
            GameManager.Main.CameraCs.CameraMove(false);
            animator.SetFloat(Animator.StringToHash("speed"), Speed);
            //GameManager.GM.PauseFirstButton = Botton;
            //GameManager.GM.firstButton = GameManager.GM.PauseFirstButton;
            if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                //EventSystem.current.SetSelectedGameObject(GameManager.GM.PauseFirstButton);
                GameManager.GM.StartSet = true;
            }
            if(pageNum== maxPageNum)
            {
                GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += CloseTutorialPage;
            }
            else
            {
                GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += GoTutorialPage;
            }
            GameManager.Sound.StopSound();
            //GameManager.Main._input.actions.FindActionMap("UI")["leftstick1"].started += useStick;
            //GameManager.Main._input.actions.FindActionMap("UI")["leftstick1"].canceled += useStick;
        }
    }
    void Start()
    {
        BottomVideowindow.SetActive(false);
        pageNum = 0;
        maxPageNum = tutorialList.Count - 1;
        //for (int i = 1; i <= maxPageNum; i++)
        //{
        //    tutorialList[i].gameObject.SetActive(false);
        //}
    }
    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 A = GameManager.Main._input.actions["leftstick1"].ReadValue<Vector2>();
        if (PageChangeCoolTime > 0)
        {
            PageChangeCoolTime -= Time.unscaledDeltaTime;
        }
        //PageScror();
    }
    public void ClosePage()
    {
        GameManager.GM.InputSystemUI.enabled = false;
        BottomVideowindow.SetActive(false);
        GameManager.Sound.SetCueName("Menu_Close");
        GameManager.Sound.OnSound();
        GameManager.GM.ChangeGamestate(GameState.Nomal);
        GameManager.GM.PauseFirstButton = null;
        GameManager.GM.firstButton = null;
        GameManager.GM.pauseUIInstance = null;
        Time.timeScale = 1;
        GameManager.Main.DestroyInputCurrent();
        GameManager.Main.EnableInput();
        GameManager.Main._input.SwitchCurrentActionMap("Player");
        GameManager.Main.CameraCs.CameraMove(true);
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= GoTutorialPage;
        GameManager.Main._input.actions.FindActionMap("UI")["BackBotton"].started -= BackTutorialPage;
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= CloseTutorialPage;
        //GameManager.Main._input.actions.FindActionMap("UI")["leftstick1"].started -= useStick;
        //GameManager.Main._input.actions.FindActionMap("UI")["leftstick1"].canceled -= useStick;
    }
    public void useStick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnConrole = true;
        }
        else if (context.canceled)
        {
            OnConrole = false;
            PageChangeCoolTime = 0;
        }
    }
    void PageScror()
    {
        if (OnConrole == false || PageChangeCoolTime > 0) return;
        Vector2 A = GameManager.Main._input.actions["leftstick1"].ReadValue<Vector2>();
        tutorialList[pageNum].gameObject.SetActive(false);
        pageNum += (int)A.x;
        Debug.Log(pageNum);
        if (pageNum < 0)
        {
            pageNum = 0;
            PageChangeCoolTime = 0.01f;
        }
        else if (pageNum > maxPageNum)
        {
            pageNum = maxPageNum;
            PageChangeCoolTime = 0.01f;
        }
        else
        {
            PageChangeCoolTime = 1;
        }
        tutorialList[pageNum].gameObject.SetActive(true);
    }

    public void GoPage()
    {
        tutorialList[pageNum].videoPlayer.Stop();
        pageNum++;
        ChangePageFirst = true;
        GameManager.Main._input.actions.FindActionMap("UI")["BackBotton"].started -= BackTutorialPage;
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= GoTutorialPage;
        if (animator.GetFloat("speed") == -Speed)
        {
            animator.SetFloat(Animator.StringToHash("speed"), Speed);
            PageStartTime = 0;
        }
        checkAnimation();
        GameManager.Sound.SetCueName("Talk_Window");
        GameManager.Sound.OnSound();
    }
    public void BackPage()
    {
        tutorialList[pageNum].videoPlayer.Stop();
        if (pageNum == maxPageNum) GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += CloseTutorialPage;
        pageNum--;
        ChangePageFirst = true;
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= CloseTutorialPage;
        GameManager.Main._input.actions.FindActionMap("UI")["BackBotton"].started -= BackTutorialPage;
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= GoTutorialPage;
        if (animator.GetFloat("speed") == Speed)
        {
            animator.SetFloat(Animator.StringToHash("speed"), -Speed);
            PageStartTime = 1;
        }
        checkAnimation();
        GameManager.Sound.SetCueName("Talk_Window");
        GameManager.Sound.OnSound();
    }
    void checkAnimation()
    {
        animator.SetBool("Rpage1", false);
        switch (pageNum)
        {
            case (0):
                stateName = "page1";
                StartCoroutine("Rpage1");
                if (PageStartTime == 0)
                {

                }
                else
                {
                    stateName = "page1-2";
                }
                break;
            case (1):
                if (PageStartTime == 0)
                {
                    stateName = "page1-2";
                }
                else
                {
                    stateName = "page2-3";
                }
                break;
            case (2):
                if (PageStartTime == 0)
                {
                    stateName = "page2-3";
                }
                else
                {
                    stateName = "page3-4";
                }
                break;
            case (3):
                if (PageStartTime == 0)
                {
                    stateName = "page3-4";
                }
                else
                {
                    stateName = "page4-5";
                }
                break;
            case (4):
                if (PageStartTime == 0)
                {
                    stateName = "page4-5";
                }
                else
                {
                    stateName = "page5-6";
                }
                break;
            case (5):
                if (PageStartTime == 0)
                {
                    stateName = "page5-6";
                }
                else
                {
                    stateName = "page6-7";
                }
                break;
            case (6):
                if (PageStartTime == 0)
                {
                    stateName = "page6-7";
                }
                else
                {
                    stateName = "page7-8";
                }
                break;
            case (7):
                if (PageStartTime == 0)
                {
                    stateName = "page7-8";
                }
                else
                {
                    stateName = "page8-9";
                }
                break;
            case (8):
                if (PageStartTime == 0)
                {
                    stateName = "page8-9";
                }
                else
                {
                    stateName = "page9-10";
                }
                break;
            case (9):
                if (PageStartTime == 0)
                {
                    stateName = "page9-10";
                }
                else
                {

                }
                break;
            default:
                break;
        }
        animator.Play(stateName, 0, PageStartTime);
    }
    IEnumerator Rpage1()
    {
        yield return new WaitForSecondsRealtime(1);
        animator.SetBool("Rpage1", true);
        yield return new WaitForSecondsRealtime(0.5f);
        animator.SetBool("Rpage1", false);
    }

    //InputSystemópä÷êî
    public void SetInput()
    {
        if (ChangePageFirst)
        {
            ChangePageFirst = false;
        }
        else
        {
            if (pageNum != 0)
            {
                GameManager.Main._input.actions.FindActionMap("UI")["BackBotton"].started += BackTutorialPage;
            }
            if (pageNum != maxPageNum)
            {
                GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += GoTutorialPage;
            }
            else
            {
                GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += CloseTutorialPage;
            }
            tutorialList[pageNum].videoPlayer.Play();
        }
    }
    public void GoTutorialPage(InputAction.CallbackContext context)
    {
        GoPage();
    }
    public void BackTutorialPage(InputAction.CallbackContext context)
    {
        BackPage();
    }
    public void CloseTutorialPage(InputAction.CallbackContext context)
    {
        ClosePage();
    }
}

