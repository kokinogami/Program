using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaSceanTest : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbitalTransposer;
    private Vector2 lastMousePosition;
    // �J�����̊p�x���i�[����ϐ��i�����l��0,0�����j
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

    // ���������̃J��������
    private void heightViewPoint()
    {
        //float y = (lastMousePosition.y - Input.mousePosition.y);
        float y = Input.GetAxis("Mouse Y");
        orbitalTransposer.m_FollowOffset.y += y * riseSpeed;
        // �}�E�X���W��ϐ�"lastMousePosition"�Ɋi�[
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
