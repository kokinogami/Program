using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shindo_Sound : MonoBehaviour
{
    Shindo_Main Main;
    // Start is called before the first frame update
    void Start()
    {
        Main = GetComponent<Shindo_Main>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSound(InputAction.CallbackContext context)
    {
        if (Main.index == 0)  //人型状態時
        {
            Main.a.PlayOneShot(Main.walk);
        }
        if (Main.index == 1)  //雪玉状態時
        {
            Main.a.PlayOneShot(Main.ball);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)//ジャンプ時
        {

        }
        if (Main.Grider)//グライダー使用時
        {

        }
        if (Main.Aspeed.magnitude <= 0 && Main.AudioEnd)
        {
            Main.AudioEnd = false;
        }
    }
}
