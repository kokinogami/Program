using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearMessageInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InputEnableClearUI()
    {
            GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started += MainMapBackcs;
    }
    public void MainMapBackcs(InputAction.CallbackContext context)
    {
        TitleBack();
    }
    public void TitleBack()
    {
        GameManager.Main.fadeInOut.fadeOutBool("MainMenu");
        GameManager.Main._input.actions.FindActionMap("UI")["Submit"].started -= MainMapBackcs;
        GameManager.GM.UnLoadStage = true;
        GameManager.titleBack = true;
    }
}
