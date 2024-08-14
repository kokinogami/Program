using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaSceanTest : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbitalTransposer;
    private Vector2 lastMousePosition;
    // カメラの角度を格納する変数（初期値に0,0を代入）
    private Vector2 cameraAngle = new Vector2(0, 0);

    public float forwardSpeed;
    public float riseSpeed;
    void Start()
    {
        this.virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        this.orbitalTransposer = this.virtualCamera.GetComponentInChildren<CinemachineOrbitalTransposer>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        heightViewPoint();
        //orbitalTransposer.m_XAxis.m_InputAxisValue = Main.Look; 
    }

    // 垂直方向のカメラ操作
    private void heightViewPoint()
    {
        //float y = (lastMousePosition.y - Input.mousePosition.y);
        float y = Input.GetAxis("Mouse Y");
        orbitalTransposer.m_FollowOffset.y += y * riseSpeed;
        // マウス座標を変数"lastMousePosition"に格納
        lastMousePosition = Input.mousePosition;
        if (orbitalTransposer.m_FollowOffset.y >= 8)
        {
            orbitalTransposer.m_FollowOffset.y = 8;
        }
        else if(orbitalTransposer.m_FollowOffset.y <= -8)
        {
            orbitalTransposer.m_FollowOffset.y = -8;
        }
    }
}
