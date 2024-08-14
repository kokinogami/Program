using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguage : MonoBehaviour
{
    Text text;
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

    private void OnEnable()
    {
        TryGetComponent(out text);

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

        Language();
    }

    void Update()
    {
        if (GameManager.currentInputHardware == GameManager.InputHardware.GamePad && InputControllerState == InputController.Keybord)
        {
            ChangedController();
            InputControllerState = InputController.Gamepad;
            Language();
        }
        else if (GameManager.currentInputHardware == GameManager.InputHardware.KeyboardMouse && InputControllerState == InputController.Gamepad)
        {
            ChangedKeybordMouse();
            InputControllerState = InputController.Keybord;
            Language();
        }
    }
    public void Language()
    {
        if (GameManager.language == GameManager.Language.Japanease)
        {
            text.text = Japanese;
        }
        else if (GameManager.language == GameManager.Language.English)
        {
            text.text = English;
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
