using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// この２行のコードでコンポーネントが自動的に追加されます。
[RequireComponent(typeof(CharacterController))]
public class YukiController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed = 10.0f;//加速度
    public float LimitSpeed = 50.0f;//速度上限
    public float Jump = 10.0f;//ジャンプ力
    public float TimeCount;//自動回復インターバル
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

    public GameObject body;
    public GameObject Ice;
    GameObject[] childObject;

    private Vector3 bodyHeight = new Vector3(0.0f, -1.000001f, 0.0f);  //氷を生成する位置に関係。体の大きさと氷の厚さに依存

    RaycastHit hit;

    //InputSystem
    private InputAction moveAction;
    private InputAction lookAction;
    private Vector2 Move;
    public Vector2 Look;

    Vector2 Aspeed = Vector2.zero;

    void Start()
    {
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
        childObject[2].SetActive(true);

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
        Kirikae();

        Aspeed = new Vector2(rb.velocity.x, rb.velocity.z);//速度ベクトル

        if (Ground == true || Grider == true)//移動制限
        {
            Controroller();
        }

        if ((Mathf.Abs(Aspeed.magnitude) >= LimitSpeed))//最大速度
        {
            Vector3 Vero = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.velocity = Vero.normalized * LimitSpeed;
        }

        if (Input.GetKey("left shift"))
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
        if (Input.GetKey("H") && Ground == false)
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
        }
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
        }
        if (collision.gameObject.tag == "JumpPad")//ジャンプ台
        {
            rb.AddForce(0, Jump, 0, ForceMode.Impulse);
            Ground = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("heal") == true)//回復アイテム
        {
            HP += 100;
            Destroy(other.gameObject);
        }
    }

    void Controroller()//移動操作スクリプト
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        //if ((Mathf.Abs(Aspeed.magnitude) < LimitSpeed)){
        rb.AddRelativeForce(0.0f, 0.0f, Move.magnitude * speed, ForceMode.Force);
    }
    private void Freez()//WASD入力がない時動かないようにする（慣性消去）
    {
        if (Move.magnitude == 0 || Ground == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
    }


    void ConnectIce()
    {
        if (Input.GetKey(KeyCode.LeftShift)) //ダッシュ中、空中にいれば氷の床を生成
        {
            if (Physics.Raycast(body.transform.position, Vector3.down, out hit, 1.1f) == false && Ground && HP > 1)
            {
                rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                Vector3 horisonMove = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                Vector3 verticalMove = new Vector3(0.0f, rb.velocity.y, 0.0f);

                Vector3 IcePosition = body.transform.position + bodyHeight + horisonMove.normalized * 2.5f;
                Instantiate(Ice, IcePosition, Quaternion.identity);  //氷の床を生成
                HP -= 1;//設置するとHPが-1される
                TimeCount = 3.0f;
            }
        }
    }

    void HPGaugeControl()
    {
        if (Input.GetKey(KeyCode.F))//ジャンプ台設置の時、HP変更
        {
            HP -= 10;
            TimeCount = 3.0f;
        }
        if (Input.GetKey(KeyCode.P))//HP数値変更
        {
            HP += 10;
        }
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

    void Kirikae()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            //現在のアクティブな子オブジェクトを非アクティブ
            childObject[index].SetActive(false);
            if (index == 0)
            {
                LimitSpeed = 50;
                index = 1;
            }
            else if (index == 1)
            {
                LimitSpeed = 25;
                index = 0;
            }

            //次のオブジェクトをアクティブ化
            childObject[index].SetActive(true);
        }
    }

    void debugcom()
    {
        //Debug.Log(index);
        Debug.Log(rb.velocity.magnitude);
        //Debug.Log(Move);
        //Debug.Log(Move.magnitude);
    }
}
