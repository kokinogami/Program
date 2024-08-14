using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PageMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Main._input.actions.FindActionMap("UI")["KeyBordCurrent"].started += PageScror;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PageScror(InputAction.CallbackContext context)
    {

    }
}
