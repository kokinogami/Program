using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public void SelectSound()
    {
        if (currentInputHardware == InputHardware.GamePad)
        {
            Sound.SetCueName("Cursor");
            Sound.OnSound();
        }
    }
    public void PointerEnterSound()
    {
        if (currentInputHardware == InputHardware.KeyboardMouse)
        {
            Sound.SetCueName("Cursor");
            Sound.OnSound();
        }
    }
    public void ClickSound()
    {
        Sound.SetCueName("Yes");
        Sound.OnSound();
    }
    public void BackSound()
    {
        Sound.SetCueName("No");
        Sound.OnSound();
    }
    public void SelectSound_title()
    {
        if (currentInputHardware == InputHardware.GamePad)
        {
            Sound.SetCueName("Cursor");
            Sound.OnSound();
        }
    }
    public void PointerEnterSound_title()
    {
        if (currentInputHardware == InputHardware.KeyboardMouse)
        {
            Sound.SetCueName("Cursor");
            Sound.OnSound();
        }
    }
}
