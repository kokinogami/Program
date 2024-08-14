using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BossStartGate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI explanationText;
    [SerializeField] GameObject Timeline_Instance_BossBefor;
    [SerializeField] MeshCollider boxCollider;
    //[SerializeField] FadeOutDebug fade;
    string selectedScene;
    // Start is called before the first frame update

    private void Awake()
    {
        if (GameManager.retryBoss)
        {
            boxCollider.enabled = false;
            explanationText.enabled = false;
            //GetComponent<BossStartGate>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        //_input.enabled = true;
        explanationText.text = "Boss戦スタート";
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")//プレイヤーが入ったとき
        {
            //explanationUI.SetActive(true);
            explanationText.enabled = true;
            GameManager.Main._input.actions["ChangeStage"].started += BossStartInput;//移動用キーを割り当てる
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //explanationUI.SetActive(false);
            explanationText.enabled = false;
            GameManager.Main._input.actions["ChangeStage"].started -= BossStartInput;
        }
    }
    public void BossStartInput(InputAction.CallbackContext context)
    {
        GameManager.Main._input.actions["ChangeStage"].started -= BossStartInput;
        boxCollider.enabled = false;
        explanationText.enabled = false;
        Timeline_Instance_BossBefor.SetActive(true);
        //GetComponent<BossStartGate>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
