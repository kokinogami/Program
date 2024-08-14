using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_5 : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int DetectDist = 20;
    [SerializeField] float searchAngle = 45f;
    [SerializeField] float SphereSpeed = 5f;
    [SerializeField] float span;
    [SerializeField] float Knockbackpower = 10f;
    [SerializeField] float Knockstopdistance;
    private int randomAt;//アニメーション振り向き乱数
    private float currentTime = 0f;
    private float changetime = 0f;
    Vector3 Knocktargetpos;
    Vector3 KnockMepos;
    Vector3 preposition;
    public bool InArea;
    bool spherestart;
    bool death = false;
    bool knockback = false;
    //アニメーション切り替えbool
    bool stop;
    bool downL;
    bool downR;
    bool upL;
    bool upR;
    bool one;
    //↑ここまで
    public Transform target;
    public GameObject Sphere;
    private Animator animator;
    private string stopStr = "isstop";
    private string dLStr = "isdownL";
    private string uRStr = "isupR";
    private string dRStr = "isdownR";
    private string uLStr = "isupL";
    [SerializeField] GameObject HitEffect;
    [SerializeField] GameObject DamageEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spherestart = true;
        this.animator = GetComponentInChildren<Animator>();
        animator.SetBool(dLStr, true);
        animator.SetBool(stopStr, false);
        animator.SetBool(uRStr, false);
        animator.SetBool(dRStr, false);
        animator.SetBool(uLStr, false);
        downL = true;
        one = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(changetime);
        if (InArea == true)
        {
            if (spherestart)
            {
                currentTime = 3f;
                spherestart = false;
            }
            currentTime += Time.deltaTime;
            //ユキノの方へ常に向かせる
            transform.LookAt(GameManager.Main.transform);
            if (currentTime > span)//3秒毎ごとに10ダメージ攻撃発射
            {
                SphereShot();
                currentTime = 0f;
            }
        }
        if (InArea == false)
        {
            spherestart = true;
            if (changetime < 1.0f)
            {
                changetime += Time.deltaTime;
            }
            if (changetime >= 1.0f)
            {
                ChangeAnimations();
            }
        }
        if (InArea == true)
        {
            stop = true;
            animator.SetBool(stopStr, true);
            animator.SetBool(dLStr, false);
            animator.SetBool(uRStr, false);
            animator.SetBool(dRStr, false);
            animator.SetBool(uLStr, false);
        }
        //雑魚とユキノ間の距離を計算
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = GameManager.Main.transform.transform.position;
        float distance = Vector3.Distance(Apos, Bpos);
        Vector3 Direction = GameManager.Main.transform.position - Apos;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (distance <= DetectDist & searchAngle > targetAngle)
        {
            InArea = true;
        }
        else
        {
            InArea = false;
        }
        //ノックバックの処理主に吹っ飛んでから止めるためのもの
        if (knockback == true)
        {
            preposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);//ノックバックしているときの更新される現在地
        }
        float knockdistance = Vector3.Distance(preposition, KnockMepos);//ぶっ飛んでる最中のエネミー1の現在地とノックバックしたときの距離を測る
        if (knockdistance >= Knockstopdistance && knockback == true)
        {
            rb.velocity = Vector3.zero;//止まる
            //Vector3 Apos = this.transform.position;
            Instantiate(DamageEffect, Apos, Quaternion.identity);
            Instantiate(HitEffect, Apos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    void ChangeAnimations()
    {
        if (stop)
        {
            //Debug.Log("stop");
            animator.SetBool(stopStr, false);
            animator.SetBool(dRStr, true);
            downR = true;
            stop = false;
        }
        else if (downL)
        {
            //Debug.Log("downL");
            animator.SetBool(uRStr, true);
            animator.SetBool(dLStr, false);
            upR = true;
            downL = false;
            //Debug.Log(upR);
            changetime = 0f;
        }
        else if (downR)
        {
            //Debug.Log("downR");
            animator.SetBool(uLStr, true);
            animator.SetBool(dRStr, false);
            upL = true;
            downR = false;
            changetime = 0f;
        }
        else if (upR)
        {
            //Debug.Log("upR");
            animator.SetBool(dRStr, true);
            animator.SetBool(uRStr, false);
            downR = true;
            upR = false;
            changetime = 0f;
        }
        else if (upL)
        {
            //Debug.Log("upL");
            animator.SetBool(dLStr, true);
            animator.SetBool(uLStr, false);
            downL = true;
            upL = false;
            changetime = 0f;
        }
    }
    public void SphereShot()
    {
        //var Shot = Instantiate(Sphere, transform.position + transform.forward * 1.5f, Quaternion.identity);
        var Shot = Instantiate(Sphere, transform.position + transform.forward * 7f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));//火の玉シェーダーの向き調節後
        Shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * SphereSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//雪だるま状態かつ動いている状態で雑魚的に当たった時、敵をノックバック
        {
            PlayerHit();
        }
        if (collision.gameObject.tag == "Ground" && death == true)//ノックバックして地面貫通する前に破壊する（エネミー1.5は上下にもノックバックして良いと思った）
        {
            Vector3 Apos = this.transform.position;
            Instantiate(HitEffect, Apos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//雪だるま状態かつ動いている状態で雑魚的に当たった時、敵をノックバック
        {
            PlayerHit();
        }
        if (other.tag == "HipDropCollider")
        {
            Vector3 Apos = this.transform.position;
            Instantiate(HitEffect, Apos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    void PlayerHit()
    {
        if (death == false)
        {
            if (GameManager.Main.index == 1 || GameManager.Main.DushCount > 0.0f)
            {
                //ノックバック追記
                knockback = true;
                Knocktargetpos = new Vector3(GameManager.Main.transform.position.x, GameManager.Main.transform.position.y, GameManager.Main.transform.position.z);//=target.transform.position
                KnockMepos = new Vector3(transform.position.x, transform.position.y, transform.position.z);//=this.transform.position
                Vector3 knockbackdir = (Knocktargetpos - KnockMepos);
                rb.velocity = -knockbackdir.normalized * Knockbackpower;

                /*  Vector3 Apos = this.transform.position;
                  Instantiate(HitEffect, Apos, Quaternion.identity);
                  Destroy(this.gameObject);*/
                death = true;
            }
        }
    }
}

