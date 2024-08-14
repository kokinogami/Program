using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public partial class YukinoMain : MonoBehaviour
{
    const int coItem = 5;
    bool EfCount = false;
    [System.NonSerialized] public float cancelHDorpCTime = 0;
    // Start is called before the first frame update
    void InputStart()
    {
        //InputEnable();
    }

    // Update is called once per frame
    void InputUpdate()
    {
        MoveControroller();
        if (GameManager.gameState == GameState.Nomal) Move = _input.actions["Move"].ReadValue<Vector2>();//移動操作操作取得
        Look = _input.actions["Look"].ReadValue<Vector2>();//カメラ操作取得
    }
    void notGroundMove()
    {
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var AddForceMove = horizontalRotation * new Vector3(Move.x, 0.0f, Move.y).normalized * Time.deltaTime * 5;
        rb.AddForce(AddForceMove, ForceMode.Impulse);
        var nowVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        if (nowVelocity.sqrMagnitude > Mathf.Pow(LimitSpeed, 2))
        {
            nowVelocity = nowVelocity.normalized * LimitSpeed;
            rb.velocity = new Vector3(nowVelocity.x, rb.velocity.y, nowVelocity.y);
        }
    }
    void MoveControroller()//移動操作スクリプト
    {
        if (inChargeBreak == false && Ground == false && Grider == false)
        {
            notGroundMove();
        }
    }
    public void JumpController(InputAction.CallbackContext context)
    {
        if (Ground && currentState != stateChargebrake && currentState != stateMovie)//ジャンプ発動時
        {
            Jump = true;
            Ground = false;
            rb.AddForce(0.0f, JumpPower, 0.0f, ForceMode.Impulse);
            rayCastCoolTime = 0.3f;
        }
        else if (Ground == false)
        {
            if (Grider)//グライダー解除
            {
                Grider = false;
            }
            else if (DoubleJumpPossible)//二段ジャンプ
            {
                DoubleJump = true;
                DoubleJumpPossible = false;
                float velo_x = rb.velocity.x;
                float velo_z = rb.velocity.z;
                rb.velocity = new Vector3(velo_x, 0, velo_z);
                rb.AddForce(0.0f, DoubleJumpPower, 0.0f, ForceMode.Impulse);
            }
        }
    }
    public void GriderContller(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Grider = true;
        }
        else if (context.canceled && Grider)
        {
            Grider = false;
        }
    }
    public void Change(InputAction.CallbackContext context)
    {
        Debug.Log("dushOn");
        if (context.started)
        {
            //DushCount = DushCountBackUp;
            if (DushCount <= 0)
            {
                Dush = true;
                DushCharge = true;
            }
        }
        if (context.performed)
        {

        }
        if (context.canceled)
        {
            Dush = false;
            DushCharge = false;
        }
    }
    public void EnergyChange(InputAction.CallbackContext context)
    {
        if (enCount > 0)
        {
            return;
        }
        if (CollectibleItem >= coItem)
        {
            enCount = 1.5f;
            CollectibleItem -= coItem;
            ItemCountUI.text = "×" + CollectibleItem;
        }
    }
    public void JumpPadSet(InputAction.CallbackContext context)//ジャンプ台設置
    {
        /*if (Ground && HP >= 10)
        {
            if (Physics.Raycast(this.transform.position + rb.velocity, Vector3.down, out hit, 3.0f))
            {
                Vector3 PadPosition = transform.position + 3.0f * Vector3.down + rb.velocity.normalized * 3.0f;
                Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
                HP -= 10;
                TimeCount = 3.0f;
            }
            else
            {
                Vector3 IcePosition = body.transform.position + bodyHeight + rb.velocity.normalized * 3.0f;
                Instantiate(Ice, IcePosition, Quaternion.identity);  //氷の床を生成
                Vector3 PadPosition = transform.position + 3.0f * Vector3.down + rb.velocity.normalized * 3.0f;
                Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));//ジャンプ台を生成
                HP -= 11;
                TimeCount = 3.0f;
            }
        }*/
        if (Ground && HP >= 10 && index == 1)
        {
            GameObject pad;
            pad = GameObject.FindGameObjectWithTag("JumpPad");
            Destroy(pad);
            Vector3 posH = new Vector3(rb.velocity.normalized.x * Time.timeScale, 0, rb.velocity.normalized.z * Time.timeScale);
            if (rb.velocity.magnitude == 0)
            {
                posH = transform.rotation * Vector3.forward * Time.timeScale;
            }
            RaycastHit hit;
            Physics.Raycast(this.transform.position, Vector3.down, out hit, 2.0f);
            RaycastHit hitF;
            Physics.Raycast(this.transform.position + posH, Vector3.down, out hitF, 5.0f);

            if (hit.distance <= hitF.distance + 0.1f || Physics.Raycast(this.transform.position + posH, Vector3.down, out hitF, 5.0f) == false)
            {
                Vector3 slopeHeight = new Vector3(0.0f, -hit.normal.y, 0.0f);
                Vector3 PadPosition = new Vector3();
                if (DushCharge)
                {
                    JumpPadScript.appearSpeed = EnergyDushSpeed / IceBallSpeed;
                    PadPosition = transform.position + 1.85f * Vector3.down + posH * 4.2f * (EnergyDushSpeed / IceBallSpeed) + slopeHeight;
                }
                else
                {
                    JumpPadScript.appearSpeed = 1.0f;
                    PadPosition = transform.position + 1.85f * Vector3.down + posH * 4.2f + slopeHeight;
                }
                Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
                GaugeDecrease(JumpPadGauge);
                GameManager.Sound.SetCueName("Create_Dai");
                GameManager.Sound.OnSound();
            }
        }
        else if (HP >= 10 && index == 0 && enCountJ <= 0)
        {
            enCountJ = 0.2f;
            GaugeDecrease(JumpPadGauge);
        }
    }
    public void HipDrop(InputAction.CallbackContext context)
    {
        if (Ground == false && Grider == false && Hipdrop == false)
        {
            Hipdrop = true;
            cancelHDorpCTime = 0.1f;
        }
        else if (Hipdrop && cancelHDorpCTime <= 0) Hipdrop = false;
    }
    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (currentState == stateMovie) return;
        //Gamemanager.ChangeGamestate(GameState.pouse);
        Gamemanager.PauseMenu();
        _input.SwitchCurrentActionMap("UI");
        CameraCs.CameraMove(false);
    }
    public void CloseMenu(InputAction.CallbackContext context)
    {
        if (currentState == stateMovie) return;
        //Gamemanager.ChangeGamestate(GameManager.backGameState);
        Gamemanager.ClosePauseMenu();
    }
    public void ChargeBreak(InputAction.CallbackContext context)
    {
        if (context.started/* && currentState == stateRunning*/)
        {
            inChargeBreak = true;
            //Instantiate(SnowballReleaseEf, this.gameObject.transform.position, Quaternion.identity);
            //chargeBreakCs.resetRo(this.transform.rotation)
        }
        if (context.canceled)
        {
            inChargeBreak = false;
        }
    }
    public void ItemConsumption(InputAction.CallbackContext context)
    {
        if (CollectibleItem > 0 && HP < HPmax)
        {
            CollectibleItem--;
            ItemCountUI.text = "×" + CollectibleItem;
            HP += healGauge;
            if (HP > HPmax)//最大HP制御
            {
                HP = HPmax;
            }
            Sound.SetCueName("Cure");
            Sound.OnSound();
        }
    }
    public void ResetCamera(InputAction.CallbackContext context)
    {
        CameraCs.onResetCamera();
    }
    public void GamePadCurrent(InputAction.CallbackContext context)//最後の入力がコントローラーの時に呼び出される
    {
        GameManager.currentInputHardware = GameManager.InputHardware.GamePad;
    }
    public void KeyBordCurrent(InputAction.CallbackContext context)//最後の入力がキーボードマウスの時に呼び出される
    {
        GameManager.currentInputHardware = GameManager.InputHardware.KeyboardMouse;
    }
    public void EnableInput()
    {
        _input.actions["Jump"].started += JumpController;
        _input.actions["Jump"].performed += GriderContller;
        _input.actions["Jump"].canceled += GriderContller;
        _input.actions["Change"].started += Change;
        _input.actions["Change"].performed += Change;
        _input.actions["Change"].canceled += Change;
        _input.actions["EnergyDush"].started += EnergyChange;
        _input.actions["JumpPadSet"].started += JumpPadSet;
        _input.actions["HipDrop"].started += HipDrop;
        _input.actions["OpenMenu"].started += OpenMenu;
        _input.actions["CloseMenu"].started += CloseMenu;
        _input.actions["ChargeBreak"].started += ChargeBreak;
        _input.actions["ChargeBreak"].canceled += ChargeBreak;
        _input.actions["ItemConsumption"].canceled += ItemConsumption;
        _input.actions["ResetCamera"].started += ResetCamera;
        _input.actions.FindActionMap("Player")["GamePadCurrent"].started += GamePadCurrent;
        _input.actions.FindActionMap("UI")["GamePadCurrent"].started += GamePadCurrent;
        _input.actions.FindActionMap("Player")["KeyBordCurrent"].started += KeyBordCurrent;
        _input.actions.FindActionMap("UI")["KeyBordCurrent"].started += KeyBordCurrent;
    }
    public void EnableInputCurrent()
    {
        _input.actions.FindActionMap("Player")["GamePadCurrent"].started += GamePadCurrent;
        _input.actions.FindActionMap("UI")["GamePadCurrent"].started += GamePadCurrent;
        _input.actions.FindActionMap("Player")["KeyBordCurrent"].started += KeyBordCurrent;
        _input.actions.FindActionMap("UI")["KeyBordCurrent"].started += KeyBordCurrent;
    }
    public void EnableInputMenu()
    {
        _input.actions["OpenMenu"].started += OpenMenu;
        _input.actions["CloseMenu"].started += CloseMenu;
    }

    //[SerializeField] Text testtext1;
    public void DestroyInput()//string name)
    {
        Debug.Log(name);
        //if (testtext1 != null)
        //{
        //    testtext1.text = name;
        //}
        _input.actions["Jump"].started -= JumpController;
        _input.actions["Jump"].performed -= GriderContller;
        _input.actions["Jump"].canceled -= GriderContller;
        _input.actions["Change"].started -= Change;
        _input.actions["Change"].performed -= Change;
        _input.actions["Change"].canceled -= Change;
        _input.actions["EnergyDush"].started -= EnergyChange;
        _input.actions["JumpPadSet"].canceled -= JumpPadSet;
        _input.actions["HipDrop"].started -= HipDrop;
        //_input.actions["OpenMenu"].started -= OpenMenu;
        _input.actions["ChargeBreak"].started -= ChargeBreak;
        _input.actions["ChargeBreak"].canceled -= ChargeBreak;
        _input.actions.FindActionMap("Player")["GamePadCurrent"].started -= GamePadCurrent;
        _input.actions.FindActionMap("UI")["GamePadCurrent"].started -= GamePadCurrent;
        _input.actions.FindActionMap("Player")["KeyBordCurrent"].started -= KeyBordCurrent;
        _input.actions.FindActionMap("UI")["KeyBordCurrent"].started -= KeyBordCurrent;
    }
    public void DestroyInputCurrent()
    {
        _input.actions.FindActionMap("Player")["GamePadCurrent"].started -= GamePadCurrent;
        _input.actions.FindActionMap("UI")["GamePadCurrent"].started -= GamePadCurrent;
        _input.actions.FindActionMap("Player")["KeyBordCurrent"].started -= KeyBordCurrent;
        _input.actions.FindActionMap("UI")["KeyBordCurrent"].started -= KeyBordCurrent;
    }
    public void DestroyInputMenu()
    {
        _input.actions["OpenMenu"].started -= OpenMenu;
        _input.actions["CloseMenu"].started -= CloseMenu;
    }
}