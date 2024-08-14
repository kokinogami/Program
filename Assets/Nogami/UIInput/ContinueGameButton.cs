using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameButton : MonoBehaviour
{
    Text text;
    [SerializeField]float alpha =150;
    // Start is called before the first frame update
    void Start()
    {
        text=GetComponentInChildren<Text>();
        if (GameManager.GM.data.tutorial1Clear == false)//�Z�[�u�f�[�^���̃`���[�g���A��1�����N���A���������F��ω�������
        {
            Color textcolor = text.color;
            text.color = /*color;Color.red;*/new Color(textcolor.r, textcolor.g, textcolor.b, alpha/255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
