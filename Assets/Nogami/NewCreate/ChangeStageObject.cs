using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ChangeStageObject : MonoBehaviour
{
    public enum ChangeStage
    {
        Debug,
        Sample,
        Stage1,
        Stage2,
        Stage3,
        Tutorial1,
        Tutorial2
    }
    [SerializeField] ChangeStage stageSelect = ChangeStage.Debug;
    //[SerializeField] PlayerInput _input;
    //[SerializeField] GameObject explanationUI;
    [SerializeField] TextMeshProUGUI explanationText;
    [SerializeField] TextMeshProUGUI explanationLookText;

    [SerializeField] FadeIn_Out FadeScreen;
    //[SerializeField] FadeOutDebug fade;
    string selectedScene;
    // Start is called before the first frame update

    bool setInput = false;

    private void Start()
    {
        GameManager.Main._input.actions["ChangeStage"].started -= ChangeStageInput;
        switch (stageSelect)
        {
            case (ChangeStage.Stage2):
                if (GameManager.stage1Clear == false)
                {

                    explanationText.enabled = false;
                    explanationLookText.enabled = true;
                }
                break;
            case (ChangeStage.Stage3):
                if (GameManager.stage2Clear == false)
                {
                    explanationText.enabled = false;
                    explanationLookText.enabled = true;
                }
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Main._input.actions["ChangeStage"].started += ChangeStageInput;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Main._input.actions["ChangeStage"].started -= ChangeStageInput;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && setInput == false)//プレイヤーが入ったとき
        {
            //explanationUI.SetActive(true);
            //explanationText.enabled = true;
            GameManager.Main._input.actions["ChangeStage"].started += ChangeStageInput;//移動用キーを割り当てる
            setInput = true;
            switch (stageSelect)
            {
                case (ChangeStage.Debug):
                    selectedScene = "Debug";
                    //explanationText.text = "デバックに移動";
                    break;
                case (ChangeStage.Sample):
                    selectedScene = "Sample";
                    //explanationText.text = "サンプルに移動";
                    break;
                case (ChangeStage.Stage1):
                    selectedScene = "Stage1";
                    //explanationText.text = "ステージ1に移動";
                    break;
                case (ChangeStage.Stage2):
                    if (GameManager.stage1Clear)
                    {
                        selectedScene = "Stage2";
                        //explanationText.text = "ステージ2に移動";
                    }
                    else
                    {
                        //explanationText.text = "ステージ1クリア後開放";
                    }
                    break;
                case (ChangeStage.Stage3):
                    if (GameManager.stage2Clear)
                    {
                        selectedScene = "Stage3";
                        //explanationText.text = "ステージ3に移動";
                    }
                    else
                    {
                        //explanationText.text = "ステージ2クリア後開放";
                    }
                    break;
                case (ChangeStage.Tutorial1):
                    selectedScene = "Stage_tutorial1_verTGS";
                    //explanationText.text = "チュートリアルステージ1に移動";
                    break;
                case (ChangeStage.Tutorial2):
                    selectedScene = "TutorialStage2_verTGS";
                    //explanationText.text = "チュートリアルステージ2に移動";
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //explanationUI.SetActive(false);
            //explanationText.enabled = false;
            GameManager.Main._input.actions["ChangeStage"].started -= ChangeStageInput;
            setInput = false;
        }
    }
    public void ChangeStageInput(InputAction.CallbackContext context)
    {
        //SceneManager.LoadScene(selectedScene);
        if (GameManager.Main.Ground)
        {
            switch (stageSelect)
            {
                case (ChangeStage.Stage2):
                    if (GameManager.stage1Clear)
                    {
                        GameManager.Main._input.actions["ChangeStage"].started -= ChangeStageInput;
                        FadeScreen.fadeOutBool(selectedScene);
                    }
                    break;
                case (ChangeStage.Stage3):
                    if (GameManager.stage2Clear)
                    {
                        GameManager.Main._input.actions["ChangeStage"].started -= ChangeStageInput;
                        FadeScreen.fadeOutBool(selectedScene);
                    }
                    break;
                default:
                    GameManager.Main._input.actions["ChangeStage"].started -= ChangeStageInput;
                    FadeScreen.fadeOutBool(selectedScene);
                    break;
            }
            GameManager.Sound.StopBGM();
        }
    }
}
