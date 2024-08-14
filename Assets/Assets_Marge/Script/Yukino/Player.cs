using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoMain : MonoBehaviour
{
    // ステートのインスタンス
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
    /// 現在のステート
    /// </summary>
    public static PlayerStateBase currentState = stateLoad;

    // Start()から呼ばれる
    private void StateStart()
    {
        currentState = stateIdle;
        Main = GameManager.Main;
        TryGetComponent(out Sound);
        currentState.OnEnter(this, null);
    }

    // Update()から呼ばれる
    private void StateUpdate()
    {
        currentState.OnUpdate(this);
        //GriderMarkActive();
    }

    // ステート変更
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
    // 死亡した時に呼ばれる
    //private void OnDeath()
    //{
    //	ChangeState(stateDead);
    //}

    // 地面との衝突
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
