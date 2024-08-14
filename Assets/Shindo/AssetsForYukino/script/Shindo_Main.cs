using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class Shindo_Main : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClipList = new List<AudioClip>();

    [SerializeField] public AudioSource a;//AudioSource型の変数aを宣言 使用するAudioSourceコンポーネントをアタッチ必要

    [SerializeField] public AudioClip walk;//AudioClip型の変数b1を宣言 使用するAudioClipをアタッチ必要
    [SerializeField] public AudioClip ball;
    public Rigidbody rb;
    public float speed;//加速度
    public float LimitSpeed;//速度上限
    public float Jump;//ジャンプ力
    public float TimeCount;//自動回復インターバル
    public float mushiCount;
    public float DushCount;
    public float hipdrop;
    public float connectCoolTime;//ジャンプ後、足場生成開始までのクールタイム
    public int HP;//?数値
    public int HPmax;
    public int GaugeQuantity;
    public int mushi;//翔蟲の回数制限
    public int index = 0;//人と雪玉の切り替え
    public int o_max = 0;
    public int enemyNum;//敵の数

    public Vector3 Rotate;
    public Vector3 Startpos;//リスポーン地点

    public Slider GaugeMain;//各ゲージ
    public Slider Gauge2;
    public Slider Gauge3;
    public GameObject UI3;

    public bool Ground;//地面との接触判定
    public bool Grider;//グライダー使用中かどうかの判定
    public bool Count;//回復インターバル管理
    public bool joushou;
    public bool tenjou;
    public bool Dush;
    public bool Hipdrop;
    public bool AudioEnd;
    public bool Result = false;//リザルト表示中か否か

    public GameObject body;
    public GameObject Ice;
    public GameObject JumpPadPrefab;
    public GameObject explosionPrefab;
    public GameObject griderObj;
    public GameObject mushiObj;
    public GameObject resultObj;
    public GameObject[] childObject;
    public GameObject[] enemies;
    [SerializeField] GameObject rolleffect;

    public Vector3 bodyHeight = new Vector3(0.0f, -1.000001f, 0.0f);  //氷を生成する位置に関係。体の大きさと氷の厚さに依存

    public RaycastHit hit;

    //InputSystem
    public InputAction moveAction;
    public InputAction lookAction;
    public Vector2 Move;
    public Vector2 Look;

    public Vector2 Aspeed = Vector2.zero;

    public YukinoAnime yukinoanime;
    // Start is called before the first frame update
    void Start()
    {
        speed = 20.0f;
        LimitSpeed = 25.0f;
        Jump = 10.0f;
        hipdrop = -30.0f;

        rb = GetComponent<Rigidbody>();
        Ground = true;

        GaugeQuantity = 2;
        GaugeMain.value = 100;
        GaugeMain.maxValue = 100;
        Gauge2.value = 0;
        Gauge2.maxValue = 100;
        Gauge3.value = 0;
        Gauge3.maxValue = 100;
        HP = 100;
        HPmax = 100 * GaugeQuantity;
        mushi = 1;
        TimeCount = 3.0f;
        connectCoolTime = 0.0f;
        Count = true;
        AudioEnd = false;
        Hipdrop = false;

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
        //InputSystem
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        moveAction = actionMap["Move"];
        lookAction = actionMap["Look"];

        Startpos = this.transform.position + new Vector3(0.0f, 3.0f, 0.0f);

        enemies = GameObject.FindGameObjectsWithTag("zako");
        enemyNum = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Freez();
        debugcom();
        effect();
        Aspeed = new Vector2(rb.velocity.x, rb.velocity.z);//速度ベクトル

        if (enemyNum == 0 && Result == false)
        {
            resultObj.SetActive(true);
            Result = true;
        }
    }
    private void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * Move.y + Camera.main.transform.right * Move.x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        if (moveForward != Vector3.zero)
        {
            if (index == 0)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
    }
    void debugcom()
    {
        //Debug.Log(Ground);
        //Debug.Log(rb.velocity.magnitude);
        //Debug.Log(Move);
        //Debug.Log(Move.magnitude);
        //Debug.Log(LimitSpeed);
        Debug.Log(Move);
    }
    private void Freez()//WASD入力がない時動かないようにする（慣性消去）
    {
        if (Move.magnitude == 0 || Ground == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
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
}