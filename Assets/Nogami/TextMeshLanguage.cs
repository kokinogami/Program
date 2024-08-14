using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshLanguage : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    [SerializeField, TextArea] string Japanese;
    [SerializeField, TextArea] string English;
    [SerializeField, TextArea] string JapaneseKeybord;
    [SerializeField, TextArea] string JapaneseController;
    [SerializeField, TextArea] string EnglishKeybord;
    [SerializeField, TextArea] string EnglishController;

    [SerializeField] bool controllerChange = true;//コントローラーによって表示が変わるか
    [SerializeField] bool updateString = false;//差し替え前に文字が消える問題の対処
    // Start is called before the first frame update

    InputController InputControllerState = InputController.Gamepad;
    enum InputController
    {
        Gamepad,
        Keybord
    }
    void OnEnable()
    {
        TryGetComponent(out textMeshPro);

        if (controllerChange == false || updateString == false)
        {
            JapaneseController = Japanese;
            JapaneseKeybord = Japanese;
            EnglishController = English;
            EnglishKeybord = English;
        }

        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad)
        {
            ChangedController();
            InputControllerState = InputController.Gamepad;
        }
        else
        {
            ChangedKeybordMouse();
            InputControllerState = InputController.Keybord;
        }

        changeLanguage();

    }

    void Update()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad && InputControllerState == InputController.Keybord)
        {
            ChangedController();
            InputControllerState = InputController.Gamepad;
            changeLanguage();
        }
        else if (GameManager.currentInputHardware == GameManager.InputHardware.KeyboardMouse && InputControllerState == InputController.Gamepad)
        {
            ChangedKeybordMouse();
            InputControllerState = InputController.Keybord;
            changeLanguage();
        }
    }

    // Update is called once per frame
    public void changeLanguage()
    {
        if (GameManager.language == GameManager.Language.Japanease)
        {
            textMeshPro.text = Japanese;
        }
        else if (GameManager.language == GameManager.Language.English)
        {
            textMeshPro.text = English;
        }
    }
    public void ChangedKeybordMouse()
    {
        Japanese = JapaneseKeybord;
        English = EnglishKeybord;
    }
    public void ChangedController()
    {
        Japanese = JapaneseController;
        English = EnglishController;
    }
}
