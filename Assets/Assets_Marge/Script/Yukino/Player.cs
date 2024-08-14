using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoMain : MonoBehaviour
{
    // �X�e�[�g�̃C���X�^���X
    public static readonly StateLoad stateLoad = new StateLoad();
    public static readonly StateIdle stateIdle = new StateIdle();
    public static readonly StateWalking stateWalking = new StateWalking();
    public static readonly StateRunning stateRunning = new StateRunning();
    public static readonly StateJumping stateJumping = new StateJumping();
    public static readonly StateDoubleJumping stateDoubleJumping = new StateDoubleJumping();
    public static readonly StateForwardRolling stateForwardRolling = new StateForwardRolling();
    public static readonly StateHipdrop stateHipdrop = new StateHipdrop();
    public static readonly StateGlider stateGlider = new StateGlider();
    public static readonly StateChargebrake stateChargebrake = new StateChargebrake();
    public static readonly StateGigantic stateGigantic = new StateGigantic();
    public static readonly StateEnergyDush stateEnergyDush = new StateEnergyDush();
    public static readonly StateEnergyJump stateEnergyJump = new StateEnergyJump();
    public static readonly StateMovie stateMovie = new StateMovie();

    public enum YukinoState
    {
        Load,
        Idle,
        Walk,
        Run,
        Jump,
        DoubleJump,
        Rolling,
        Hipdrop,
        Glider,
        Chargebreake,
        EnergyDush,
        EnergyJump
    }
    public enum dushBuffState
    {
        step1 = 1,
        step2 = 2,
        step3 = 3
    }
    public static YukinoState Yukinocurrentstate = YukinoState.Load;
    [System.NonSerialized] public dushBuffState nowDushBuff = dushBuffState.step1;
    //private static readonly StateDead stateDead = new StateDead();

    static YukinoMain Main;
    static YukinoSound Sound;
    //public bool IsDead => currentState is StateDead;

    /// <summary>
    /// ���݂̃X�e�[�g
    /// </summary>
    public static PlayerStateBase currentState = stateLoad;

    // Start()����Ă΂��
    private void StateStart()
    {
        currentState = stateIdle;
        Main = GameManager.Main;
        TryGetComponent(out Sound);
        currentState.OnEnter(this, null);
    }

    // Update()����Ă΂��
    private void StateUpdate()
    {
        currentState.OnUpdate(this);
        //GriderMarkActive();
    }

    // �X�e�[�g�ύX
    public void ChangeState(PlayerStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }

    private void StateLateUpdate()
    {
        currentState.OnLateUpdate(this);
    }
    private void StateFixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }
    // ���S�������ɌĂ΂��
    //private void OnDeath()
    //{
    //	ChangeState(stateDead);
    //}

    // �n�ʂƂ̏Փ�
    /*private void OnCollisionEnter(Collision collision)
    {
        ChangeState(stateIdle);
    }*/
    public void changeLoad()
    {
        ChangeState(stateLoad);
    }
    private static void ChangeHuman()
    {
        GameManager.Main.childObject[GameManager.Main.index].SetActive(false);
        GameManager.Main.index = 0;
        GameManager.Main.childObject[GameManager.Main.index].SetActive(true);
        Instantiate(GameManager.Main.SnowballReleaseEf, GameManager.Main.gameObject.transform.position, Quaternion.identity);
        Main.groundRadius = 0.5f;
        Main.groundDistance = 0.45f;
        if (GameManager.Main.rb.velocity.y > 0)
        {
            GameManager.Main.rb.velocity = new Vector3(GameManager.Main.rb.velocity.x, 0, GameManager.Main.rb.velocity.z);
        }
    }
    private static void ChangeIceBall()
    {
        GameManager.Main.childObject[GameManager.Main.index].SetActive(false);
        GameManager.Main.index = 1;
        GameManager.Main.childObject[GameManager.Main.index].SetActive(true);
        GameManager.Main.CameraCs.ChangeYukidamaCamera();
        Main.groundRadius = 0.8f;
        Main.groundDistance = 0.19f;
    }
    private static void ChangeChargebrake()
    {
        GameManager.Main.childObject[GameManager.Main.index].SetActive(false);
        GameManager.Main.index = 0;
        GameManager.Main.childObject[GameManager.Main.index].SetActive(true);
        Instantiate(GameManager.Main.SnowballChargeBreakeReleaseEf, GameManager.Main.gameObject.transform.position, Quaternion.identity);
        Main.groundRadius = 0.5f;
        Main.groundDistance = 0.45f;
        if (GameManager.Main.rb.velocity.y > 0)
        {
            GameManager.Main.rb.velocity = new Vector3(GameManager.Main.rb.velocity.x, 0, GameManager.Main.rb.velocity.z);
        }
    }
}
