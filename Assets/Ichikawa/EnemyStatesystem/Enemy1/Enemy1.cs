using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy1
{
    [System.NonSerialized] public Rigidbody rb;
    [System.NonSerialized] public Vector3 preposition;
    [System.NonSerialized] public Vector3 Targetposition;
    [System.NonSerialized] public Vector3 Knocktargetpos;
    [System.NonSerialized] public Vector3 KnockMepos;
    [System.NonSerialized] public float deathCount=2;
    [System.NonSerialized] public float preAttack = 1.1f;
    [System.NonSerialized] public bool InArea;
    [System.NonSerialized] public bool attack;
    [System.NonSerialized] public bool knockback;
    [System.NonSerialized] public bool isGround;
    [System.NonSerialized] public bool breaktime;
    public float elapsedtime = 0f;
    public float rate;
    public bool look;
    public bool Insight;
    public bool set;
    [SerializeField] int DetectDist = 20;
    [SerializeField] public float searchAngle = 45f;
    [SerializeField] int Hp;
    [SerializeField] int PounceDamage;
    [SerializeField] public float PouncePower = 8f;
    [SerializeField] public float PounceUpPower = 5f;
    [SerializeField] public float RotateSpeed = 1.0f;//検知後、ユキノに向くスピード(小さくすればするほど速くなる）
    [SerializeField] float Delaytime = 0.5f;
    [SerializeField] public GameObject HitEffect;
    [SerializeField] public GameObject DamageEffect;
    [SerializeField] private GameObject PreAttackEffect;
    EnemyDestroyEvent destroyEvent;
    Vector3 raypos;
    Transform target;
    Animator animator;
    [System.NonSerialized] public string stopStr = "stopping";
    [System.NonSerialized] public string idleStr = "idling";
    // ステートのインスタンス
    private static readonly StateWait1 statewait = new StateWait1();
    private static readonly StatePounce statepounce = new StatePounce();
    private static readonly StateDetect statedetect = new StateDetect();
    /// <summary>
    /// 現在のステート
    /// </summary>
    private Enemy1StateBase currentState = statewait;
    // Start()から呼ばれる
    private void OnStart()
    {
        TryGetComponent(out destroyEvent);
        target = GameManager.Main.transform;
        rb = GetComponent<Rigidbody>();
        this.animator = GetComponentInChildren<Animator>();
        this.animator.SetBool(idleStr, true);
        this.animator.SetBool(stopStr, false);
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20.0f))
        {
            raypos = hit.point;
        }
        currentState.OnEnter(this, null);
    }
    private void OnUpdate()
    {
        if(InArea)
        {
            this.animator.SetBool(stopStr, true);
            this.animator.SetBool(idleStr, false);
        }
        else
        {
            this.animator.SetBool(idleStr, true);
            this.animator.SetBool(stopStr, false);
        }
        if(look == true)//lookは飛びつき攻撃(StatePounce)まで終わると起動
        {
            waitDetect();
        }
      if (look == true & Insight == true)//waitDetectで検知できると、時間をかけてユキノへ向く
        {
            Look();
        }
        if(Hp <= 0)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < raypos.y - 50f)//落下してった時用の処理
        { Destroy(gameObject); }
        currentState.OnUpdate(this);
    }
    private void OnFixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }
    private void ChangeState(Enemy1StateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }
    public void Detect()//検知関数
    {
        float detectDistance = Mathf.Pow(DetectDist, 2);//処理負荷低減（sqrMagnitude)適用済み
        Vector3 Apos = transform.position;
        Vector3 Bpos = target.transform.position;
        Vector3 Direction = Bpos - Apos;
        float distance = Direction.sqrMagnitude;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (distance <= detectDistance & searchAngle > targetAngle)
        {
            InArea = true;
        }
        else
        {
            InArea = false;
        }
    }
    public void waitDetect()//検知関数との違いは、一回見つけてるため、距離が離れる以外目では追い続ける仕様にしたいから、距離の条件のみ
    {
        float dis = Mathf.Pow(DetectDist, 2);
        Vector3 apos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 bpos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 measure = bpos - apos;
        float measuredist = measure.sqrMagnitude;
        if (measuredist <= dis)
        {
            Insight = true;
        }
        else
        {
            Insight = false;
        }
    }
    public void Look()//Update()内で使用することが条件
    {
        Vector3 sightPos = new Vector3(target.position.x, transform.position.y,target.position.z);//下向かないようにyのみエネミー1の高さ
        Quaternion targetRotation = Quaternion.LookRotation(sightPos - transform.position);
        elapsedtime += Time.deltaTime;
        rate = Mathf.Clamp01(elapsedtime / RotateSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation,rate);
    }
    public void Startcoroutine()//StateWait1()で呼べるようにするためのコルーチン開始関数
    {
        StartCoroutine(DerayDetect());
    }
    public IEnumerator DerayDetect()//飛びついた後三秒間何もしないでその後また検知
    {
        yield return new WaitForSeconds(3);
        breaktime = false;
    }
    public void Shortcoroutine()
    {
        StartCoroutine(ShortDeray());
    }
    public IEnumerator ShortDeray()//1秒間検知してから飛びつくまでに遅延
    {
        yield return new WaitForSeconds(Delaytime);
        set = true;
    }

    public void Waitfreeze()//体向けられるようにする
    {
        //(演算子 | を使うことによってconstraintsの複数にチェック可能）
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    public void holdfreeze()//Constraintsの中のPositionY以外にチェックつける関数（歩いて当たると動く問題対策）
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//雪だるま状態かつ動いている状態で雑魚的に当たった時、敵をノックバック
        {
            if (Hp > 0 & GameManager.Main.index == 1 || GameManager.Main.DushCount > 0.0f)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                playerHit();
            }
                if (attack)//飛びつき中に当たった時ダメージが入る
            {
                GameManager.Main.OnDamage(PounceDamage ,true);
            }
            if (transform.position.y > collision.gameObject.transform.position.y)//エネミー１がユキノの上に乗った場合の処理
            {
                Vector3 colPos = new Vector3(target.position.x, 0f, target.position.z);
                Vector3 thisPos = new Vector3(transform.position.x, 0f, transform.position.z);
                Vector3 dir = colPos - thisPos;
                rb.velocity = Vector3.zero;
                rb.AddForce(-dir * 3f, ForceMode.Impulse);
                rb.AddForce(0f, 5f, 0f, ForceMode.Impulse);
                GameManager.Main.rb.velocity = Vector3.zero;
                collision.gameObject.GetComponent<Rigidbody>().AddForce(10f * dir + new Vector3(0f,5f,0f), ForceMode.Impulse);
                if (transform.position.x == collision.gameObject.transform.position.x & transform.position.z == collision.gameObject.transform.position.z)
                {
                    rb.AddForce(10f, 10f, 0f, ForceMode.Impulse);
                }
            }
        }
        if (collision.gameObject.tag == "RespawnableStage")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
            if (collision.gameObject.tag == "RespawnableStage")
        {
            isGround = false;
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//ユキノ本体に当たった時
        {
            if (Hp > 0 & GameManager.Main.index == 1 || GameManager.Main.DushCount > 0.0f)
            {
                playerHit();
            }
        }
        if (other.tag == "HipDropCollider")//ヒップドロップで当たった時
        {
            destroyEvent.DeathEnemy();
            Hp -= 1;
            Vector3 Apos = this.transform.position;
            Instantiate(HitEffect, Apos, Quaternion.identity);
        }
    }
    void playerHit()//ユキノに当たった時の処理内容
    {
        destroyEvent.DeathEnemy();
        Hp -= 1;
        if (GameManager.Main.index != 1 && GameManager.Main.DushCount <= 0.0f) return;
        Vector3 Apos = this.transform.position;
        Instantiate(DamageEffect, Apos, Quaternion.identity);
        GameObject A =Instantiate(HitEffect, Apos, Quaternion.identity);
        A.transform.localScale = new Vector3(1, 1, 1) * 5;
    }
}
