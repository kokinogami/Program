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
        if (Main.index == 0)  //�l�^��Ԏ�
        {
            Main.a.PlayOneShot(Main.walk);
        }
        if (Main.index == 1)  //��ʏ�Ԏ�
        {
            Main.a.PlayOneShot(Main.ball);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)//�W�����v��
        {

        }
        if (Main.Grider)//�O���C�_�[�g�p��
        {

        }
        if (Main.Aspeed.magnitude <= 0 && Main.AudioEnd)
        {
            Main.AudioEnd = false;
        }
    }
}
