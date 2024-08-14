using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class TitleUIController : MonoBehaviour
{
    //[System.NonSerialized] public static bool InControllerInput = false;//コントローラー判定
    //[System.NonSerialized] public static bool InKeyBordMouse = false;//コントローラー判定
    [System.NonSerialized] private bool StartSet = false;
    [System.NonSerialized] private int MenuCount;
    [System.NonSerialized] public PlayerInput _input;
    public GameObject TitleFirstButton;
    public GameObject TitleSecondButton;
    public GameObject OptionButton;
    public GameObject StaffRollButton;
    // Start is called before the first frame update
    private void Awake()
    {
        MenuCount = 0;
        TryGetComponent(out _input);
        _input.actions.FindActionMap("UI")["GamePadCurrent"].started += GamePadCurrent;
        _input.actions.FindActionMap("UI")["KeyBordCurrent"].started += KeyBordCurrent;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad && StartSet == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (MenuCount == 1)
            {
                EventSystem.current.SetSelectedGameObject(TitleFirstButton);
            }
            if (MenuCount == 2)
            {
                EventSystem.current.SetSelectedGameObject(TitleSecondButton);
            }
            if (MenuCount == 3)
            {
                EventSystem.current.SetSelectedGameObject(OptionButton);
            }
            if (MenuCount == 3)
            {
                EventSystem.current.SetSelectedGameObject(StaffRollButton);
            }
            StartSet = true;
        }
        if (GameManager.currentInputHardware == GameManager.InputHardware.KeyboardMouse)
        {
            EventSystem.current.SetSelectedGameObject(null);
            StartSet = false;
        }
    }
    public void GamePadCurrent(InputAction.CallbackContext context)
    {
        GameManager.currentInputHardware = GameManager.InputHardware.GamePad;
    }
    public void KeyBordCurrent(InputAction.CallbackContext context)
    {
        GameManager.currentInputHardware = GameManager.InputHardware.KeyboardMouse;
    }
    public void StartChangd()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(TitleFirstButton);
        }
        MenuCount = 1;
        Debug.Log("Tstart");
    }
    public void StageChangd()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(TitleSecondButton);
        }
        MenuCount = 2;
    }
    public void OptionChangd()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(OptionButton);
        }
        MenuCount = 3;
    }
    public void StaffRollChangd()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(StaffRollButton);
        }
        MenuCount = 4;
    }
}
