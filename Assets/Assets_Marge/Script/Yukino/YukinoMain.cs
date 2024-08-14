using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public partial class YukinoMain : MonoBehaviour
{
    [HeaderAttribute("ユキノパラメーター")]
    [TooltipAttribute("人型の速度")] public float HumanoidSpeed = 10.0f;//人型の速度
    [TooltipAttribute("雪玉の速度")] public float IceBallSpeed = 50.0f;//雪玉の速度
    [TooltipAttribute("雪玉の速度")] public float EnergyDushSpeed = 70.0f;//雪玉の速度
    [TooltipAttribute("前転の速度")] public float ForwardSpeed = 60.0f;//前転の速度
    [TooltipAttribute("前転にかかる時間")] public float DushCountBackUp = 0.8f;//前転にかかる時間(調整用)
    [TooltipAttribute("ジャンプの強さ")] public float JumpPower = 8.0f;//ジャンプ力
    [TooltipAttribute("ダブルジャンプの強さ")] public float DoubleJumpPower = 8.0f;//ジャンプ力
    [TooltipAttribute("重力")] public float Gravity = 7.0f;//重力
    [TooltipAttribute("ヒップドロップの強さ")] public float hipdrop = -30.0f;//ヒップドロップの速さ
    [TooltipAttribute("ゲージ1つ分の数値")] public int HP = 100;//Hp数値
    [TooltipAttribute("アイテム使用の回復量")] public int healGauge = 100;//Hp数値
    [TooltipAttribute("自動回復のクールタイム")] public float HealTimeCount = 3.0f;//自動回復インターバル(設定用)
    [TooltipAttribute("自動回復のクールタイム")] public float attackedHealTimeCount = 7.0f;//敵から攻撃を受けた後の自動回復インターバル(設定用)
    [TooltipAttribute("ダメージリアクションの持続時間")] public float DamagereactionTime = 0.3f;//ダメージリアクションの持続時間
    [TooltipAttribute("足場設置のクールタイム")] public float connectCoolTimeBackUp = 1.0f;//ジャンプ後、足場生成開始までのクールタイム(調整用)
    [TooltipAttribute("ジャンプ→接地判定のクールタイム")] public float rayCastCoolTime = 0.3f;//ジャンプ後、RayCastによる接地判定再開までのクールタイム
    [TooltipAttribute("バレットタイム中のゲーム速度")] public float bulletTimeSpeed = 0.2f;//バレットタイム中のゲーム速度
    [TooltipAttribute("ジャンプ台生成時のゲーム消費量")] public int JumpPadGauge = 10;//ジャンプ台生成時のゲージ消費量
    [TooltipAttribute("橋生成時のゲーム消費量")] public int ConnectIceGauge = 2;//橋生成時のゲージ消費量
    [TooltipAttribute("前転時敵にホーミングする距離")] public int homingDestans = 20;//前転時敵にホーミングする距離
    [TooltipAttribute("前転時敵にホーミングする距離")] public int homingAngle = 90;//前転時敵にホーミングする距離
    [TooltipAttribute("ダッシュ継続バフの2段階目")] public float dushBuffTime1 = 1.3f;//
    [TooltipAttribute("ダッシュ継続バフの3段階目")] public float dushBuffTime2 = 1.3f;//

    [HeaderAttribute("HPゲージ")]
    public Slider GaugeMain;//各ゲージ
    public Slider Gauge2;
    public Slider Gauge3;
    public GameObject UI3;
    [System.NonSerialized] public float auteHealCount;//ゲージ使用後の自動回復インターバル
    [System.NonSerialized] public float attackedAuteHealCount;//攻撃を受けた後の自動回復インターバル
    [HeaderAttribute("GameObjectアタッチ")]
    public GameObject body;
    public GameObject IceL;//
    public GameObject IceM;//橋オブジェクト（サイズ別）
    public GameObject IceS;//
    public GameObject IceCatapult;//
    public GameObject JumpPadPrefab;
    public GameObject Hisora;
    public GameObject Yukino1;//被ダメージ時のマテリアル変更オブジェクト
    public GameObject carriedRefrigerator;//背負っている冷蔵庫
    public GameObject explosionPrefab;
    public GameObject GriderMark;//グライダー時下に表示されるマーク
    public GameObject SnowballChargeBreakeReleaseEf;//
    public GameObject SnowballReleaseEf;//
    public GameObject SnowballStartEf;//
    public GameObject SnowballLoopEf;//
    public GameObject ChaergeBreakEf;//
    public GameObject RunStartEf;
    public GameObject HipDropEf;//
    public GameObject roll2Ef;//
    public GameObject roll3Ef;//
    public GameObject SnowBall;
    public GameObject EnergyDushEf;//エネルギーダッシュ用エフェクト
    public GameObject DamageEf;//被ダメージ時に発生するエフェクト
    public GameObject BuffEf2;
    public GameObject BuffEf3;
    public Material DamageMat;//被ダメージ時のユキノのマテリアル
    public GameObject Monochrome;
    public GameObject ChargebreakOffscreen;
    public UniversalAdditionalCameraData cameraData;
    public Text ItemCountUI;
    [System.NonSerialized] public GameObject[] childObject;
    [SerializeField] GameObject rolleffect;
    public BoxCollider yukidamaBoxCollider;

    [HeaderAttribute("script")]
    public YukinoAnime yukinoanime;
    public YukidamaAnimation YukidamaAnimation;
    public CinemaScean CameraCs;
    public GameManager Gamemanager;
    public ChargeBreakNavi chargeBreakCs;
    public Image fadeIn;
    public FadeIn_Out fadeInOut;
    public FadeIn_Out_W fadeInOut_W;
    public HipDropCollider HipDropCollider;//

    public Vector3 bodyHeight = new Vector3(0.0f, 0.0f, 0.0f);  //氷を生成する位置に関係。体の大きさと氷の厚さに依存


    public RaycastHit hit;

    [System.NonSerialized] public Rigidbody rb;
    [System.NonSerialized] public Rigidbody Hisorarb;
    [System.NonSerialized] public SphereCollider yukidamaSphereCollider;
    [System.NonSerialized] public float LimitSpeed;//速度上限

    [System.NonSerialized] SkinnedMeshRenderer YukinoRenderer;
    [System.NonSerialized] public Material[] FirstMats = new Material[9];//ユキノの元のマテリアル配列（ダメージリアクション用に保存）
    [System.NonSerialized] public Material[] CurrentMats = new Material[9];//ユキノの現在のマテリアル配列（ダメージリアクション用に取得）
    [System.NonSerialized] SkinnedMeshRenderer SnowBallRenderer;
    [System.NonSerialized] public Material[] SBFirstMats = new Material[2];//ユキノの元のマテリアル配列（ダメージリアクション用に保存）
    [System.NonSerialized] public Material[] SBCurrentMats = new Material[2];//ユキノの現在のマテリアル配列（ダメージリアクション用に取得）


    [System.NonSerialized] public float mushiCount;
    [System.NonSerialized] public float DushCount;//前転にかかる時間(カウント用)
    [System.NonSerialized] public float connectCoolTime = 0.0f;//ジャンプ後、足場生成開始までのクールタイム（カウント用）
    [System.NonSerialized] public float hitStop;//ヒットストップ判定（0以上でヒットストップ）
    [System.NonSerialized] public float hitStopSpeed = 0.3f;//ヒットストップ時のゲームスピード
    [System.NonSerialized] public float iceconnectSoundCoolTime = 0;
    [System.NonSerialized] public float SoundCoolTime = 0.5f;
    [System.NonSerialized] public int HPmax;
    [System.NonSerialized] public int GaugeQuantity = 2;//ゲージ数カウント
    [System.NonSerialized] public int index = 0;//人と雪玉の切り替え
    [System.NonSerialized] public int o_max = 0;
    [System.NonSerialized] public int CollectibleItem = 0;//アイテム数
    [System.NonSerialized] public float JumpPadObj;//接触判定
    [System.NonSerialized] public JumpPadAppear JumpPadScript;//接触判定
    [System.NonSerialized] public float enCount = 0;
    [System.NonSerialized] public float enCountJ = 0;
    float rollingCooltime = 0;
    float groundRadius = 0.5f;
    float groundDistance = 0.93f;

    [System.NonSerialized] public Vector3 Rotate;
    [System.NonSerialized] public Vector3 Startpos;//リスポーン地点

    [System.NonSerialized] public bool Ground = true;//地面との接触判定
    [System.NonSerialized] public bool Grider;//グライダー使用中かどうかの判定
    //[System.NonSerialized] public bool Count = true;//回復インターバル管理
    [System.NonSerialized] public bool Jump = false;//ジャンプしたかの判定
    [System.NonSerialized] public bool DoubleJump = false;//２段ジャンプの判定
    [System.NonSerialized] public bool DoubleJumpPossible = true;//２段ジャンプが可能かどうか
    [System.NonSerialized] public bool joushou;
    [System.NonSerialized] public bool tenjou;
    [System.NonSerialized] public bool Dush;
    [System.NonSerialized] public bool DushCharge;
    [System.NonSerialized] public bool Hipdrop = false;
    [System.NonSerialized] public bool AudioEnd = false;
    [System.NonSerialized] public bool inChargeBreak = false;//チャージブレーキ起動判定
    //[System.NonSerialized] public bool InControllerInput = false;//コントローラー判定
    //[System.NonSerialized] public bool InKeyBordMouse = false;//コントローラー判定
    [System.NonSerialized] public bool invinciblePlayer = false;//無敵判定
    [System.NonSerialized] public bool onFadeIn = true;
    [System.NonSerialized] public bool OnWhiteFade = false;

    //InputSystem
    [System.NonSerialized] public PlayerInput _input;
    [System.NonSerialized] public Vector2 Move;
    [System.NonSerialized] public Vector2 Look;

    [System.NonSerialized] public Vector2 Aspeed = Vector2.zero;

    public List<float> homingdis;
    public List<float> homingangle;

    [HeaderAttribute("デバッグ・調整用変数")]
    [TooltipAttribute("足場の形を板状にする")] public bool plate;//橋を板状のオブジェクトにするかどうか
    [TooltipAttribute("板状の足場")] public GameObject IcePlate;
    [TooltipAttribute("デバック用")] public bool GriderDebag;
    [TooltipAttribute("デバックモード")] public bool DebugMode;
    [TooltipAttribute("テストモード")] public bool TestMode;

    public enum Form
    {
        Humanoid = 0,
        IceBall = 1
    }
    public enum ShakeCameraState
    {
        none,
        hitEnemy,
        startRun
    }

    [System.NonSerialized] public ShakeCameraState shakeCameraState = ShakeCameraState.none;
    [System.NonSerialized] public float defalutbufftime = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        TryGetComponent(out _input);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Hisorarb = Hisora.GetComponent<Rigidbody>();

        Rotate = Vector3.zero;

        o_max = this.transform.childCount;//子オブジェクトの個数取得
        childObject = new GameObject[o_max];//インスタンス作成

        for (int i = 0; i < o_max; i++)
        {
            childObject[i] = transform.GetChild(i).gameObject;//すべての子オブジェクト取得
        }
        //すべての子オブジェクトを非アクティブ
        foreach (GameObject gamObj in childObject)
        {
            gamObj.SetActive(false);
        }
        //最初はひとつだけアクティブ化しておく
        childObject[index].SetActive(true);

        Startpos = this.transform.position + new Vector3(0.0f, 3.0f, 0.0f);
        CollitionStart();
        UIstart();
        InputStart();
        StateStart();
        CameraCs.CameraMove(false);
        rolleffect.SetActive(false);
        GriderMark.SetActive(false);
        childObject[1].TryGetComponent(out yukidamaSphereCollider);

        YukinoRenderer = Yukino1.GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < YukinoRenderer.materials.Length; i++)
        {
            FirstMats[i] = YukinoRenderer.materials[i];
            CurrentMats[i] = YukinoRenderer.materials[i];
        }

        SnowBallRenderer = SnowBall.GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < SnowBallRenderer.materials.Length; i++)
        {
            SBFirstMats[i] = SnowBallRenderer.materials[i];
            SBCurrentMats[i] = SnowBallRenderer.materials[i];
        }

        JumpPadScript = JumpPadPrefab.GetComponent<JumpPadAppear>();
    }

    // Update is called once per frame
    void Update()
    {
        ActionUpdate();
        CollirionUpdate();
        InputUpdate();
        UIUpdate();
        StateUpdate();
        //BoundStop();
        debugcom();
        //effect();
        FadeinInput();
        Aspeed = new Vector2(rb.velocity.x, rb.velocity.z);//速度ベクトル
        hitStop -= Time.deltaTime;
        if (rb.velocity.y < 0.0f && index == 0)
        {
            rb.AddForce(0.0f, -0.2f * Time.deltaTime, 0.0f, ForceMode.Impulse);
        }
        if (Time.timeScale == hitStopSpeed && hitStop < 0)
        {
            Time.timeScale = 1;
        }
        if (JumpPadObj > 0)
        {
            JumpPadObj -= Time.deltaTime;
        }
        if (TestMode)
        {
            if (rb.velocity.y >= 0)
            {
                //rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
            }
        }
        if (Input.GetKey(KeyCode.L))
        {
            YukinoSound Sound;
            TryGetComponent(out Sound);
            Sound.StopSound();
        }
        EventUpdate();
        rollingCooltime -= Time.deltaTime;
    }
    private void LateUpdate()
    {
        CollirionLateUpdate();
        StateLateUpdate();
    }
    private void FixedUpdate()
    {
        if (currentState != stateGlider && currentState != stateForwardRolling && currentState != stateHipdrop) yukinoGravity();
        OnGroundJudge();
        StateFixedUpdate();
    }

    void debugcom()
    {
        //Debug.Log(Ground);
        //Debug.Log(rb.velocity);
        //Debug.Log(Move);
        //Debug.Log(Move.magnitude);
        //Debug.Log(LimitSpeed);
        //Debug.Log(cancelHDorpCTime);
        //Debug.Log(Hipdrop);
    }

    void yukinoGravity()
    {
        //float velo_x = rb.velocity.x;
        //float velo_y = rb.velocity.y;
        //float velo_z = rb.velocity.z;
        //float Delta = 0.02f;
        //float gravity_Velo = velo_y - Mathf.Pow(Gravity, 2) * Delta;
        //rb.velocity = new Vector3(velo_x, gravity_Velo, velo_z);
        if (rb.velocity.y <= -35)
        {
            rb.velocity = new Vector3(rb.velocity.x, -35, rb.velocity.z);
            return;
        }
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1f) && Ground)
        {
            if (hit.collider.gameObject.layer != 6)
            {
                return;
            }
        }
        rb.AddForce(0, -Mathf.Pow(Gravity, 2) /* 0.02f*/, 0, ForceMode.Force);
        if (rb.velocity.y <= -35)
        {
            rb.velocity = new Vector3(rb.velocity.x, -35, rb.velocity.z);
            return;
        }
    }
    private void Freez()//WASD入力がない時動かないようにする（慣性消去）
    {
        if (Move.magnitude == 0 || Ground == true)
        {
            //rb.constraints |= RigidbodyConstraints.FreezeRotationY;
        }
    }
    private void effect()
    {
        if (index == 1)
        {
            rolleffect.SetActive(true);
        }
        else
        {
            rolleffect.SetActive(false);
        }
    }
    private void FadeinInput()
    {
        if (fadeIn.color.a <= 0.0f && onFadeIn == true && StartPlatform.OnPlayer == true)
        {
            Gamemanager.explanationOpen();
            onFadeIn = false;
        }
    }
    public void ChangeIdele()
    {
        ChangeState(stateIdle);
    }
}
