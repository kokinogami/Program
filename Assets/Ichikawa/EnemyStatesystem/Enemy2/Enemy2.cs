using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy2
{
    [SerializeField] float DetectDist = 20;//至近検知距離
    [SerializeField] float farDist = 40;//最大検知距離
    [SerializeField] public int RoundSphereDamage;
    [SerializeField] public int AttackSphereDamage;
    [SerializeField] public int RushDamage;
    [SerializeField] public float RushknockPower = 3f;//突進攻撃時のノックバックさせる力
    [SerializeField] public float partKnockPower = 51f;//部位破壊時のノックバックさせる力
    [SerializeField] public float partKnockUpPower = 15f;//部位破壊時のノックバックで上に加える力
    [SerializeField] public float RushUpPower = 5f;//突進攻撃時のノックバックで上に加える力
    [SerializeField] public float Rushdist;//突進攻撃で進む距離
    [SerializeField] public float spherestoptime;//弾攻撃継続時間
    [SerializeField] public float MoveSpeed;//突進時の速さ
    [SerializeField] public float RotateSpeed = 1.0f;//小さいほど振り向く速度が速くなる
    [SerializeField] public float movetime;//ユキノの目線に合わせるのにかける時間
    [SerializeField] public float Roundspheremove;
    [SerializeField] private Collider player;
    [SerializeField] private Collider body;
    [SerializeField] GameObject RushEffect;
    [SerializeField] GameObject HitEffect;
    [SerializeField] public float searchAngle = 45f;
    public Dou_ene_Empty emptyscript;
    public Enemy2sphere spherescript1;
    public Enemy2sphere spherescript2;
    public Enemy2sphere spherescript3;
    public Enemy2sphere spherescript4;
    public Enemy2sphere spherescript5;
    public Enemy2sphere spherescript6;
    public Enemy2sphere spherescript7;
    public Enemy2sphere spherescript8;
    public Gurun subscript1;
    public Gurun subscript2;
    public Gurun subscript3;
    public Gurun subscript4;
    // private GameObject Yukino;
    //YukinoMain YukinoScript;
    [System.NonSerialized] public bool far;//遠方距離で検知したとき用
    [System.NonSerialized] public bool near;//至近距離で検知したとき用
    [System.NonSerialized] public bool match;//ユキノの高さに合わせ終わった時用
    [System.NonSerialized] public bool breaktime;//弾幕攻撃後の無防備時間用
    [System.NonSerialized] public bool longbreaktime;//突進攻撃後の無防備時間用
    [System.NonSerialized] public bool rush;// 主に突進中にGurunやRoundsphereが当たった時にRushDamageを与えるため
    [System.NonSerialized] public bool set;//高さを合わせてから攻撃をするため
    [System.NonSerialized] public bool waitset;//高さを合わせてから攻撃をするため
    [System.NonSerialized] public bool nodamage;
    [System.NonSerialized] public bool Insight; //距離のみで検知するとき用
    [System.NonSerialized] public bool look;//突進攻撃が終わるとtrueに
    [System.NonSerialized] public bool backArea;
    [System.NonSerialized] public bool end;//周りの球が全滅したとき用
    [System.NonSerialized] public bool holdview;//Look()が終わった判定用
    bool blind;
    [System.NonSerialized] Vector3 postpositon;
    [System.NonSerialized] Vector3 YukinoEyepos;
    //Vector3 StartEyepos; //遠距離攻撃時用ユキノの立地時の目の位置保管用
    public float nodamageTime = 0.5f;
    float elapsedTime = 0f;//経過時間
    public float elapsedtime;
    public float backtime;
    float rate;
    Vector3 raypos;
    Transform target;
    [System.NonSerialized] public Rigidbody rb;
    public List<GameObject> Others;
    SphereCollider Scol;
    // ステートのインスタンス
    private static readonly StateWait statewait = new StateWait();
    private static readonly Statenear statenear = new Statenear();
    private static readonly StateSphereattack statesphereattack = new StateSphereattack();
    private static readonly StateDetect2 statedetect = new StateDetect2();
    private static readonly StateRush staterush = new StateRush();


    /// <summary>
    /// 現在のステート
    /// </summary>
    private Enemy2StateBase currentState = statewait;

    // Start()から呼ばれる
    private void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        Scol = GetComponent<SphereCollider>();
        currentState.OnEnter(this, null);
        target = GameManager.Main.transform;
        elapsedtime = 0f;
        rush = false;
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20.0f))
        {
            raypos = hit.point;
        }
    }

    // Update()から呼ばれる
    private void OnUpdate()
    {
        currentState.OnUpdate(this);
        Vector3 Pos = transform.position;
        if (!end)
        { transform.position = new Vector3(Pos.x, Mathf.Clamp(Pos.y, raypos.y + 2.0f, raypos.y + 4.0f), Pos.z); }//移動制限
        if (transform.position.y < raypos.y - 50f)//落下してった時用の処理
        { Destroy(gameObject); }
        if (GameManager.Main.index == 1)
        { YukinoEyepos = GameManager.Main.transform.position + new Vector3(0f, 0.8789997f, 0f); }
        if (GameManager.Main.index == 0)
        { YukinoEyepos = GameManager.Main.transform.position; }
        if (rush || end)//突進時のrb.velocityと周りが無くなった時に落下させる用
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
        if (nodamage == true)
        {
            nodamageTime -= Time.deltaTime;
            if (nodamageTime <= 0.0f)
            {
                nodamage = false;
            }
        }
        if (look == true & currentState == statewait)
        {
            waitDetect();//距離だけの検知
        }
        Scol.isTrigger = false;
        rb.useGravity = true;//周りの球が無くなった時落ちるように
        end = true;
        for (int i = 0; i < Others.Count; i++)
        {
            if (Others[i] != null)
            {
                Scol.isTrigger = true;
                rb.useGravity = false;
                end = false;
            }
        }
        if (end == true)
        {
            ChangeState(statewait);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        if (elapsedtime >= RotateSpeed & currentState != staterush)
        {
            if (blind == false)
            {
                holdview = true;
                HoldLook();
            }
        }
        if (blind & !near)//検知外に行ったとき
        {
            elapsedtime = 0f;
        }
        if (!far & !Insight)//検知外から始まるから、blindはtrueでスタートする
        { blind = true; }
        else
        { blind = false; }
    }

    private void OnFixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }

    private void ChangeState(Enemy2StateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && nodamage == false)
        {
            if (GameManager.Main.index == 1 || GameManager.Main.Dush)
            {
                GameObject hitef = Instantiate(HitEffect, gameObject.transform.position, Quaternion.identity);
                hitef.transform.localScale = Vector3.one * 1.5f;
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && nodamage == false && end && GameManager.Main.index == 1)
        {
            Instantiate(HitEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void Detect()//検知関数（far,near,backAreaの場合のbool)
    {
        float detectDistance = Mathf.Pow(DetectDist, 2);//処理負荷低減（sqrMagnitude)適用済み
        float FarDistance = Mathf.Pow(farDist, 2);
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = GameManager.Main.transform.position;
        Vector3 Direction = Bpos - Apos;
        float distance = Direction.sqrMagnitude;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (detectDistance < distance & distance <= FarDistance & searchAngle > targetAngle)
        {
            far = true;
        }
        else if (distance <= detectDistance & searchAngle > targetAngle)
        {
            near = true;
        }
        else if (distance < detectDistance & targetAngle >= searchAngle)//背後の検知
        {
            backArea = true;
        }
        else
        {
            far = false;
            near = false;
            backArea = false;
        }
    }
    public void waitDetect()//距離が離れる以外目では追い続ける、距離の条件のみ
    {
        float dis = Mathf.Pow(farDist, 2);
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
    public void Look()//攻撃する方向を向く（検知で気づく動き）
    {
        Vector3 sightPos = new Vector3(target.position.x, transform.position.y, target.position.z);//下向かないようにyのみエネミー1の高さ
        Quaternion targetRotation = Quaternion.LookRotation(sightPos - transform.position);
        elapsedtime += Time.deltaTime;
        rate = Mathf.Clamp01(elapsedtime / RotateSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rate);
    }
    public void HoldLook()//時間かけないでユキノに向く（基本的にLook()が終わった後に使う）
    {
        if (holdview)
        {
            Vector3 sightPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(sightPos);
        }
    }
    public void Startshortcoroutine()//StateWait()で呼べるようにするためのコルーチン開始関数
    {
        StartCoroutine(DerayDetect());
    }
    public IEnumerator DerayDetect()//攻撃完了後三秒間何もせずその後検知
    {
        yield return new WaitForSeconds(3);
        breaktime = false;
    }
    public void matchheight()//Enemy2の高さをユキノの視点に合わせる
    {
        if (set == false)
        {
            Vector3 pos = transform.position;
            elapsedTime += Time.deltaTime;
            rate = Mathf.Clamp01(elapsedTime / movetime);
            postpositon = new Vector3(pos.x, YukinoEyepos.y + 1.0f, pos.z);
            transform.position = Vector3.Lerp(pos, postpositon, rate);
        }
        if (elapsedTime >= movetime)
        {
            set = true;//移動が完了してから近接攻撃に移れるようにする
        }
    }
    public void matchwaitheight()//攻撃後の何もしない時間に滞空させる処理
    {
        Vector3 pos = transform.position;
        backtime += Time.deltaTime;
        rate = Mathf.Clamp01(backtime / movetime);
        postpositon = new Vector3(pos.x, raypos.y + 4.0f, pos.z);
        transform.position = Vector3.Lerp(pos, postpositon, rate);
        if (backtime >= movetime)
        {
            waitset = true;
        }
    }
}
