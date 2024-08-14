using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// この２行のコードでコンポーネントが自動的に追加されます。
[RequireComponent(typeof(CharacterController))]
public class InputSystem : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClipList = new List<AudioClip>();

    [SerializeField] private AudioSource a;//AudioSource型の変数aを宣言 使用するAudioSourceコンポーネントをアタッチ必要

    [SerializeField] private AudioClip walk;//AudioClip型の変数b1を宣言 使用するAudioClipをアタッチ必要
    [SerializeField] private AudioClip ball;



    public void OnSound(InputAction.CallbackContext context)
    {
        if (index == 0)  //人型状態時
        {
            a.PlayOneShot(walk);
        }
        if (index == 1)  //雪玉状態時
        {
            a.PlayOneShot(ball);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)//ジャンプ時
        {

        }
        if (Grider)//グライダー使用時
        {

        }
    }
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed;//加速度
    public float LimitSpeed;//速度上限
    public float Jump;//ジャンプ力
    public float TimeCount;//自動回復インターバル
    private float mushiCount;
    public float DushCount;
    public int HP;//?数値
    public int HPmax;
    public int GaugeQuantity;
    public int mushi;//翔蟲の回数制限
    public int index = 0;//人と雪玉の切り替え
    private int o_max = 0;

    public Vector3 Rotate;

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

    public GameObject body;
    public GameObject Ice;
    public GameObject JumpPadPrefab;
    public GameObject explosionPrefab;
    public GameObject griderObj;
    public GameObject mushiObj;
    GameObject[] childObject;

    private Vector3 bodyHeight = new Vector3(0.0f, -1.000001f, 0.0f);  //氷を生成する位置に関係。体の大きさと氷の厚さに依存

    RaycastHit hit;

    //InputSystem
    private InputAction moveAction;
    private InputAction lookAction;
    private Vector2 Move;
    public Vector2 Look;

    Vector2 Aspeed = Vector2.zero;

    public YukinoAnime yukinoanime;

    void Start()
    {
        speed = 20.0f;
        LimitSpeed = 25.0f;
        Jump = 10.0f;

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
        Count = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        Move = moveAction.ReadValue<Vector2>();//移動操作操作取得
        Look = lookAction.ReadValue<Vector2>();//カメラ操作取得

        debugcom();
        ConnectIce();
        //Freez();
        HPGaugeControl();
        Joushoukiryu();

        Aspeed = new Vector2(rb.velocity.x, rb.velocity.z);//速度ベクトル

        MoveControroller();
        if (Move.magnitude == 0 && index == 0 && Ground)
        {
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            //yukinoanime.Walkfin();
        }

        /*if ((Mathf.Abs(Aspeed.magnitude) >= LimitSpeed))//最大速度
        {
            Vector3 Vero = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.velocity = Vero.normalized * LimitSpeed;
        }
        if (mushiCount <= 0)
        {
            mushiObj.SetActive(false);
        }
        else
            mushiCount -= Time.deltaTime;
        /*if (Grider)
        {
            speed = 50.0f;
        }
        else
        {
            speed = 10.0f;
        }*/
        /*if (Input.GetKey("left shift"))
        {
            speed = 200.0f;
            //if (HP >= 1)
            //    rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
        }
        else
        {
            speed = 100.0f;
        }

        if (Input.GetKey("space") && Ground == true)
        {
            rb.AddForce(0.0f, Jump, 0.0f, ForceMode.Impulse);
            Ground = false;
        }

        if (Input.GetKey("1") && mushi == 1)
        {
            Ground = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.AddForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
            mushi = 0; ;
            Ground = false;
        }
        if (Input.GetMouseButtonDown(1) && Ground == false)
        {
            if (Grider == false)
            {
                rb.drag = 8;
                Grider = true;
            }
            else
            {
                rb.drag = 0.5f;
                Grider = false;
            }
        }*/
    }
    private void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * Move.y + Camera.main.transform.right * Move.x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        //rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//地面との接触判定
        {
            Ground = true;
            Grider = false;
            rb.drag = 0.5f;
            mushi = 1;
            griderObj.SetActive(false);
        }
        if (collision.gameObject.tag == "enemy")
        {
            if (index == 0)
            {
                HP -= 10;
                TimeCount = 3.0f;
            }
        }
        if (collision.gameObject.tag == "JumpPad")//ジャンプ台
        {
            Vector3 JumpDir = collision.transform.rotation * new Vector3(0.0f, 8.0f, -24.0f);
            if (Physics.Raycast(body.transform.position + 0.6f * bodyHeight, new Vector3(rb.velocity.x, 0.0f, rb.velocity.z), out hit, 4.0f) && hit.normal.y >= 0.8f)
            {
                Ground = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(JumpDir, ForceMode.Impulse);
            }
        }
        if (collision.gameObject.tag == "BrokenObject")
        {
            if (index == 1)
            {
                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);//エフェクトの大きさ設定

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("heal") == true)//回復アイテム
        {
            HP += 100;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("joushou"))
        {
            joushou = true;
        }
        if (other.CompareTag("tenjou"))
        {
            tenjou = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("joushou"))
        {
            joushou = false;
        }
    }

    void MoveControroller()//移動操作スクリプト
    {
        if (Ground == true || Grider == true)//移動制限
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
            if (tenjou == true)
            {
                rb.constraints |= RigidbodyConstraints.FreezePositionY;
            }

            if (DushCount > 0.0f && Move.magnitude > 0.0f)
            {
                LimitSpeed = 60.0f;
                DushCount -= 1.0f * Time.deltaTime;
            }
            else if (Dush == true && Move.magnitude > 0.0f)
            {
                LimitSpeed = 50.0f;
                childObject[index].SetActive(false);
                index = 1;
                childObject[index].SetActive(true);
            }
            else
            {
                LimitSpeed = 25.0f;
                childObject[index].SetActive(false);
                index = 0;
                childObject[index].SetActive(true);
            }
            rb.AddRelativeForce(Vector3.forward * Move.magnitude * (LimitSpeed - Aspeed.magnitude) * speed, ForceMode.Force);

            if (Ground && Move.magnitude > 0)
            {
                //yukinoanime.Walkstr();
            }
        }
    }
    public void JumpController(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            if (Ground)
            {
                childObject[index].SetActive(false);
                index = 0;
                childObject[index].SetActive(true);
                rb.AddForce(0.0f, Jump, 0.0f, ForceMode.Impulse);
                Ground = false;
                //yukinoanime.Jumpstr();
            }
            else if (Ground == false)
            {

                if (Grider)
                {
                    rb.drag = 0.5f;
                    Grider = false;
                    griderObj.SetActive(false);
                    childObject[index].SetActive(false);
                    index = 0;
                    childObject[index].SetActive(true);
                }
                else if (mushi == 1)
                {
                    Mushi();
                }
            }

        }
        if (context.performed && Grider == false)
        {
            rb.drag = 8;
            Grider = true;
            griderObj.SetActive(true);
        }
    }
    private void Freez()//WASD入力がない時動かないようにする（慣性消去）
    {
        if (Move.magnitude == 0 || Ground == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
    }


    void ConnectIce()//ダッシュ中、空中にいれば氷の床を生成
    {
        if (index == 1)
        {
            if (Physics.Raycast(body.transform.position, Vector3.down, out hit, 1.1f) == false && Ground && HP > 1)
            {
                //rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                rb.useGravity = false;
                Vector3 horisonMove = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                Vector3 verticalMove = new Vector3(0.0f, rb.velocity.y, 0.0f);

                Vector3 IcePosition = body.transform.position + bodyHeight + horisonMove.normalized * 2.5f + new Vector3(0.0f, 0.25f, 0.0f);
                Instantiate(Ice, IcePosition, Quaternion.identity);  //氷の床を生成
                HP -= 1;//設置するとHPが-1される
                TimeCount = 3.0f;
            }
            else { rb.useGravity = true; }
        }
    }

    void HPGaugeControl()
    {
        if (Input.GetKey(KeyCode.Q) & HP >= 10)
        {
            HP -= 10;
            TimeCount = 3.0f;
        }

        if (Input.GetKey(KeyCode.O) & GaugeQuantity < 3)//HPバー増加
        {
            UI3.SetActive(true);
            GaugeQuantity++;
            HPmax = 100 * GaugeQuantity;
        }
        if (HP > HPmax)//最大HP制御
        {
            HP = HPmax;
        }
        if (HP < 100 & Count == false)//自動回復と制御
        {
            HP = HP + 1;
        }
        GaugeMain.value = HP;//UIの変更
        Gauge2.value = HP - 100;
        Gauge3.value = HP - 200;
        if (TimeCount > 0.0f)//自動回復のインターバル制御
        {
            TimeCount -= Time.deltaTime;
            Count = true;
        }
        else
        {
            Count = false;
        }
    }

    public void Change(InputAction.CallbackContext context)
    {
        /*//現在のアクティブな子オブジェクトを非アクティブ
        childObject[index].SetActive(false);
        if (context.started)
        {
            LimitSpeed = 50;
            index = 1;
            //rb.mass = 10;
        }
        if (context.canceled)
        {
            LimitSpeed = 25;
            index = 0;
            //rb.mass = 50;
        }
        //次のオブジェクトをアクティブ化
        childObject[index].SetActive(true);*/
        if (context.started)
        {
            DushCount = 0.5f;
        }
        if (context.performed)
        {
            Dush = true;
        }
        if (context.canceled)
        {
            DushCount = 0.0f;
            Dush = false;
        }
    }
    public void Mushi()
    {
        if (mushi == 1)
        {
            Ground = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
            mushiObj.SetActive(true);
            mushiCount = 0.6f;
            mushi = 0;
            Ground = false;
        }
    }
    public void JumpPadSet(InputAction.CallbackContext context)//ジャンプ台設置
    {
        if (context.started)
        {
            if (Ground && HP >= 10)
            {
                Vector3 PadPosition = transform.position + 3.0f * Vector3.down + rb.velocity.normalized * 3.0f;
                Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
                HP -= 10;
                TimeCount = 3.0f;
            }
        }
    }
    private void Joushoukiryu()
    {
        if (joushou == true && Grider == true)
        {
            rb.velocity = new Vector3(0.0f, 9.83f, 0.0f);
        }
    }

    void debugcom()
    {
        //Debug.Log(index);
        //Debug.Log(rb.velocity.magnitude);
        //Debug.Log(Move);
        //Debug.Log(Move.magnitude);
        //Debug.Log(LimitSpeed);
    }
}
