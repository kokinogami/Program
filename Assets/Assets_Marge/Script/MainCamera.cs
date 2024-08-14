using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject targetObj;
    Vector3 targetPos;
    Vector2 Look;
    public YukinoMain YukinoMain;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = targetObj.transform.position;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Look = YukinoMain.Look;
        // target�̈ړ��ʕ��A�����i�J�����j���ړ�����
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
        // �}�E�X�̉E�N���b�N�������Ă����
        // �}�E�X�̈ړ���
        float mouseInputX = Look.x;
        float mouseInputY = Look.y;
        // target�̈ʒu��Y���𒆐S�ɁA��]�i���]�j����
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
        // �J�����̐����ړ��i���p�x�����Ȃ��A�K�v��������΃R�����g�A�E�g�j
        transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 100f);

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }*/
        //Debug.Log(Look);
        if (Time.timeScale == 0f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}