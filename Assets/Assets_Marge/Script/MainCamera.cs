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
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
        // マウスの右クリックを押している間
        // マウスの移動量
        float mouseInputX = Look.x;
        float mouseInputY = Look.y;
        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
        // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
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