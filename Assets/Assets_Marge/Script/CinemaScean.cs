using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaScean : MonoBehaviour
{
    float A = 0;
    [System.NonSerialized] public float cameraValue;
    [System.NonSerialized] public float iCeBallChangeTime = 3;
    public enum CameraState
    {
        yukinocamera,
        yukidamacamera,
        yukidamacameranear,
        chargebreakecamera
    }

    //カメラステート
    CameraState camerasState = CameraState.yukinocamera;
    [System.NonSerialized] public CameraState oldCamerasState;
    //シネマsceneブレイン
    CinemachineBrain cinemachineBrain;

    //人型カメラ
    public CinemachineVirtualCamera yukinoCamera;
    [System.NonSerialized] public CinemachinePOV yukinoCamera_POV;
    CinemachineFramingTransposer yukinoCamera_CFT;
    float defaltDistans_yukino;
    float minAngle_yukino;
    float maxAngle_yukino;

    //雪玉カメラ(遠)
    [SerializeField] CinemachineVirtualCamera yukidamaCamera;
    [System.NonSerialized] public CinemachinePOV yukidamaCamera_POV;
    [System.NonSerialized] public CinemachineFramingTransposer yukidamaCamera_CFT;
    CinemachineBasicMultiChannelPerlin yukidamaCamera_CBMP;
    float defaltDistans_yukidama;
    float minAngle_yukidama;
    float maxAngle_yukidama;

    //雪玉カメラ(近)
    [SerializeField] CinemachineVirtualCamera yukidamaCameraNear;
    [System.NonSerialized] public CinemachinePOV yukidamaCameraNear_POV;
    [System.NonSerialized] public CinemachineFramingTransposer yukidamaCameraNear_CFT;
    CinemachineBasicMultiChannelPerlin yukidamaCameraNear_CBMP;
    float defaltDistans_yukidamaNear;
    float minAngle_yukidamaNear;
    float maxAngle_yukidamaNear;

    //チャージブレーキカメラ
    public CinemachineVirtualCamera chargeBreakCamera;
    [System.NonSerialized] public CinemachinePOV chargeBreakCamera_POV;
    [System.NonSerialized] public CinemachineFramingTransposer chargeBreakCamera_CFT;
    float defaltDistans_chargeBreak;
    float minAngle_chargeBreak;
    float maxAngle_chargeBreak;

    //リセット用カメラ
    public CinemachineVirtualCamera ResetCamera;
    CinemachinePOV ResetCamera_POV;
    CinemachineFramingTransposer ResetCamera_CFT;

    [SerializeField] GameObject ChargeBreakNavi;
    [SerializeField] int CHARGE_BREAK_VERTICAL = 20;
    [SerializeField] float DAMPING = 0.5f;

    [System.NonSerialized] public float blendCount;
    [SerializeField] float BLENT_COUNT = 2;

    [SerializeField] float SHAKE_COUNT = 1.0f;

    [SerializeField] float HIT_ENEMY_AGAIN = 1.0f;//振れ幅
    [SerializeField] float HIT_ENEMY_FGAIN = 1.0f;//振動速度
    [SerializeField] float START_RUN_AGAIN = 0.5f;//振れ幅
    [SerializeField] float START_RUN_FGAIN = 0.5f;//振動速度
    bool isGainChange;

    public List<CinemachineInputProvider> CameraList;
    public List<CinemachinePOV> POVList;

    [System.NonSerialized] public bool createConnect = false;
    void Start()
    {
        yukinoCamera_CFT = yukinoCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        yukinoCamera_POV = yukinoCamera.GetCinemachineComponent<CinemachinePOV>();
        defaltDistans_yukino = yukinoCamera_CFT.m_CameraDistance;
        minAngle_yukino = yukinoCamera_POV.m_VerticalAxis.m_MinValue;
        maxAngle_yukino = yukinoCamera_POV.m_VerticalAxis.m_MaxValue;

        yukidamaCamera_POV = yukidamaCamera.GetCinemachineComponent<CinemachinePOV>();
        yukidamaCamera_CFT = yukidamaCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        yukidamaCamera_CBMP = yukidamaCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        defaltDistans_yukidama = yukidamaCamera_CFT.m_CameraDistance;
        minAngle_yukidama = yukidamaCamera_POV.m_VerticalAxis.m_MinValue;
        maxAngle_yukidama = yukidamaCamera_POV.m_VerticalAxis.m_MaxValue;

        yukidamaCameraNear_POV = yukidamaCameraNear.GetCinemachineComponent<CinemachinePOV>();
        yukidamaCameraNear_CFT = yukidamaCameraNear.GetCinemachineComponent<CinemachineFramingTransposer>();
        yukidamaCameraNear_CBMP = yukidamaCameraNear.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        defaltDistans_yukidamaNear = yukidamaCameraNear_CFT.m_CameraDistance;
        minAngle_yukidamaNear = yukidamaCameraNear_POV.m_VerticalAxis.m_MinValue;
        maxAngle_yukidamaNear = yukidamaCameraNear_POV.m_VerticalAxis.m_MaxValue;

        chargeBreakCamera_POV = chargeBreakCamera.GetCinemachineComponent<CinemachinePOV>();
        chargeBreakCamera_CFT = chargeBreakCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        defaltDistans_chargeBreak = chargeBreakCamera_CFT.m_CameraDistance;
        minAngle_chargeBreak = chargeBreakCamera_POV.m_VerticalAxis.m_MinValue;
        maxAngle_chargeBreak = chargeBreakCamera_POV.m_VerticalAxis.m_MaxValue;

        ResetCamera_POV = ResetCamera.GetCinemachineComponent<CinemachinePOV>();
        ResetCamera_CFT = ResetCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        yukinoCamera.Priority = 10;
        yukidamaCamera.Priority = 1;
        yukidamaCameraNear.Priority = 1;
        chargeBreakCamera.Priority = 1;
        ResetCamera.Priority = 1;

        yukinoCamera_CFT.m_XDamping = DAMPING;
        yukinoCamera_CFT.m_YDamping = DAMPING;
        yukinoCamera_CFT.m_ZDamping = DAMPING;

        //AllChangeSensitivity(GameManager.svValue);
        CameraChangeSensitivity(GameManager.svValue * GameManager.CmSvValue);
        ChargeBreakChangeSensitivity(GameManager.svValue * GameManager.ChSvValue);
    }

    // Update is called once per frame
    void Update()
    {
        //chargeBreakCamera_POV.m_VerticalAxis.Value = CHARGE_BREAK_VERTICAL;
        if (yukidamaCameraControl()) changePriority(CameraState.yukidamacameranear);
        if (camerasState == CameraState.yukidamacameranear && iCeBallChangeTime > 0)
        {
            iCeBallChangeTime -= Time.deltaTime;
            if (iCeBallChangeTime <= 0) iCeBallChangeTime = 0;
        }
        if (A <= 0) ResetCamera.Priority = 1;
        else A -= Time.deltaTime;
        if (createConnect) ConnectIceChangeCamera();
        changeDanping();
    }

    public void ChaneYukinoCamera()//人型カメラ変更
    {
        CameraState newCamera = CameraState.yukinocamera;
        changePriority(newCamera);
    }
    public void ChangeYukidamaCamera()//雪玉カメラ変更
    {
        if (GameManager.Main.index == 0) return;
        CameraState newCamera = CameraState.yukidamacamera;
        changePriority(newCamera);
        blendCount = BLENT_COUNT;
    }
    public void ChangeChargeBreakCamera()//チャージブレーキカメラ変更
    {
        //chargeBreakCamera_POV.m_VerticalAxis.Value = CHARGE_BREAK_VERTICAL;
        //chargeBreakCamera_POV.m_HorizontalAxis.Value = Mathf.Repeat(ChargeBreakNavi.transform.eulerAngles.y + 180, 360) - 180;

        CameraState newCamera = CameraState.chargebreakecamera;
        changePriority(newCamera);
    }
    public void onResetCamera()//カメラのリセット
    {
        ResetCamera.Priority = 20;
        A = 0.5f;
    }
    bool yukidamaCameraControl()//雪玉時一定時間経過後カメラを遠くする
    {
        if (camerasState != CameraState.yukidamacamera) return false;
        if (blendCount <= 0) return true;
        blendCount -= Time.deltaTime;
        return false;
    }
    void ConnectIceChangeCamera()
    {
        if (GameManager.Main.index == 0) createConnect = false;
        else createConnect = true;
        if (blendCount > 0) blendCount = BLENT_COUNT;
        else Invoke("ChangeYukidamaCamera", iCeBallChangeTime);
    }
    void changeDanping()
    {
        switch (camerasState)//変更前の優先度変更
        {
            case CameraState.yukinocamera:
                float lowAngleDistanceRatio_yno = 0.5f; // 低い時の距離の比率
                float highAngleDistanceRatio_yno = 1.5f; // 高い時の距離の比率
                float anglePhase_yd = (yukinoCamera_POV.m_VerticalAxis.Value - minAngle_yukino) / (maxAngle_yukino - minAngle_yukino);
                float lowCameraDistance_yd = defaltDistans_yukino * lowAngleDistanceRatio_yno;
                float highCameraDistance_yd = defaltDistans_yukino * highAngleDistanceRatio_yno;
                yukinoCamera_CFT.m_CameraDistance = lowCameraDistance_yd + (highCameraDistance_yd - lowCameraDistance_yd) * anglePhase_yd;
                break;
            case CameraState.yukidamacamera:
                float lowAngleDistanceRatio_yd = 0.5f; // 低い時の距離の比率
                float highAngleDistanceRatio_yd = 1.5f; // 高い時の距離の比率
                float anglePhase_yno = (yukidamaCamera_POV.m_VerticalAxis.Value - minAngle_yukidama) / (maxAngle_yukidama - minAngle_yukidama);
                float lowCameraDistance_yno = defaltDistans_yukidama * lowAngleDistanceRatio_yd;
                float highCameraDistance_yno = defaltDistans_yukidama * highAngleDistanceRatio_yd;
                yukidamaCamera_CFT.m_CameraDistance = lowCameraDistance_yno + (highCameraDistance_yno - lowCameraDistance_yno) * anglePhase_yno;
                break;
            case CameraState.yukidamacameranear:
                float lowAngleDistanceRatio_yN = 0.5f; // 低い時の距離の比率
                float highAngleDistanceRatio_yN = 1.5f; // 高い時の距離の比率
                float anglePhase_yN = (yukidamaCameraNear_POV.m_VerticalAxis.Value - minAngle_yukidamaNear) / (maxAngle_yukidamaNear - minAngle_yukidamaNear);
                float lowCameraDistance_yN = defaltDistans_yukidamaNear * lowAngleDistanceRatio_yN;
                float highCameraDistance_yN = defaltDistans_yukidamaNear * highAngleDistanceRatio_yN;
                yukidamaCameraNear_CFT.m_CameraDistance = lowCameraDistance_yN + (highCameraDistance_yN - lowCameraDistance_yN) * anglePhase_yN;
                break;
            case CameraState.chargebreakecamera:
                float lowAngleDistanceRatio_cb = 0.5f; // 低い時の距離の比率
                float highAngleDistanceRatio_cb = 1.5f; // 高い時の距離の比率
                float anglePhase_cb = (chargeBreakCamera_POV.m_VerticalAxis.Value - minAngle_chargeBreak) / (maxAngle_chargeBreak - minAngle_chargeBreak);
                float lowCameraDistance_cb = defaltDistans_chargeBreak * lowAngleDistanceRatio_cb;
                float highCameraDistance_cb = defaltDistans_chargeBreak * highAngleDistanceRatio_cb;
                chargeBreakCamera_CFT.m_CameraDistance = lowCameraDistance_cb + (highCameraDistance_cb - lowCameraDistance_cb) * anglePhase_cb;
                break;
        }
    }
    void changePriority(CameraState newCamera)
    {
        switch (camerasState)//変更前の優先度変更
        {
            case CameraState.yukinocamera:
                cameraValue = yukinoCamera_POV.m_HorizontalAxis.Value;
                yukinoCamera.Priority = 1;
                break;
            case CameraState.yukidamacamera:
                cameraValue = yukidamaCamera_POV.m_HorizontalAxis.Value;
                yukidamaCamera.Priority = 1;
                break;
            case CameraState.yukidamacameranear:
                cameraValue = yukidamaCameraNear_POV.m_HorizontalAxis.Value;
                yukidamaCameraNear.Priority = 1;
                break;
            case CameraState.chargebreakecamera:
                chargeBreakCamera.Priority = 1;
                break;
        }
        oldCamerasState = camerasState;
        switch (newCamera)//変更先の優先度変更
        {
            case CameraState.yukinocamera:
                yukinoCamera.Priority = 10;
                break;
            case CameraState.yukidamacamera:
                yukidamaCamera.Priority = 10;
                break;
            case CameraState.yukidamacameranear:
                yukidamaCameraNear.Priority = 10;
                iCeBallChangeTime = 3.0f;
                break;
            case CameraState.chargebreakecamera:
                switch (oldCamerasState)
                {
                    case CameraState.yukinocamera:
                        cameraValue = yukinoCamera_POV.m_HorizontalAxis.Value;
                        break;
                    case CameraState.yukidamacamera:
                        cameraValue = yukidamaCamera_POV.m_HorizontalAxis.Value;
                        break;
                    case CameraState.yukidamacameranear:
                        cameraValue = yukidamaCameraNear_POV.m_HorizontalAxis.Value;
                        yukidamaCameraNear.Priority = 1;
                        break;
                    default:
                        break;
                }
                chargeBreakCamera.Priority = 10;
                break;
        }
        camerasState = newCamera;
    }
    public void ShakeCamera(YukinoMain.ShakeCameraState State)
    {
        switch (State)
        {
            case YukinoMain.ShakeCameraState.none:
                return;
            case YukinoMain.ShakeCameraState.hitEnemy:
                GainChange(HIT_ENEMY_AGAIN, yukidamaCamera_CBMP);
                GainChange(HIT_ENEMY_FGAIN, yukidamaCameraNear_CBMP);
                break;
            case YukinoMain.ShakeCameraState.startRun:
                GainChange(START_RUN_AGAIN, yukidamaCamera_CBMP);
                GainChange(START_RUN_FGAIN, yukidamaCameraNear_CBMP);
                break;
        }
        if (isGainChange) StopCoroutine("GainCountContlor");
        StartCoroutine("GainCountContlor");
        isGainChange = true;
    }
    void GainChange(float gain, CinemachineBasicMultiChannelPerlin CBMP)
    {
        CBMP.m_AmplitudeGain = gain;//振れ幅
        CBMP.m_FrequencyGain = gain;//速度
    }
    IEnumerator GainCountContlor()
    {
        yield return new WaitForSeconds(SHAKE_COUNT);
        GainChange(0, yukidamaCamera_CBMP);
        GainChange(0, yukidamaCameraNear_CBMP);
        isGainChange = false;
    }
    public void CameraMove(bool OnOff)
    {
        int A = CameraList.Count;
        for (int i = 0; i < A; i++)
        {
            CameraList[i].enabled = OnOff;
        }
    }
    public void AllChangeSensitivity(float SensitivityValue)
    {
        yukinoCamera_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        yukinoCamera_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCamera_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCamera_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCameraNear_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCameraNear_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
        chargeBreakCamera_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        chargeBreakCamera_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
    }
    public void CameraChangeSensitivity(float SensitivityValue)
    {
        yukinoCamera_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        yukinoCamera_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCamera_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCamera_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCameraNear_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        yukidamaCameraNear_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
    }
    public void ChargeBreakChangeSensitivity(float SensitivityValue)
    {
        chargeBreakCamera_POV.m_VerticalAxis.m_MaxSpeed = SensitivityValue;
        chargeBreakCamera_POV.m_HorizontalAxis.m_MaxSpeed = SensitivityValue;
        //Debug.Log(chargeBreakCamera_POV.m_VerticalAxis.m_MaxSpeed);
        //Debug.Log(chargeBreakCamera_POV.m_HorizontalAxis.m_MaxSpeed);
    }
}
