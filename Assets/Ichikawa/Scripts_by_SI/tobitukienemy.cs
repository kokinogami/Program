using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tobitukienemy : MonoBehaviour
{
    YukinoMain YukinoScript;//（変更）主にYukinoMainスクリプトの中の数値（変数）を参照するためのもの
    private GameObject Yukino;
    [SerializeField] int DetectDist = 20;
    [SerializeField] public float searchAngle = 45f;
    [SerializeField] float Knockstopdistance;
    public bool InArea;
    public Transform target;
    private bool knockback;
    private bool isGround;//使うとしたら、飛びついて着地したときにクールタイム作ったり,止めたり。改めて飛びつく方向をキャッシュする方面かな
    public bool attack;
    public bool one;
    private Vector3 targetposition;
    private Vector3 Meposition;
    private Vector3 preposition;
    private Vector3 Targetposition;
    Rigidbody rb;
    [SerializeField] int Hp; //変更点
    // Start is called before the first frame update
    void Start()
    {
        Yukino = GameObject.FindWithTag("Player");
        YukinoScript = Yukino.GetComponent<YukinoMain>();
        rb = GetComponent<Rigidbody>();
        isGround = true;
        one = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Apos = transform.position;
        targetposition = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        Meposition = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position = new Vector3(Apos.x, Mathf.Clamp(Apos.y, 0.0f, 2.0f), Apos.z);//移動制限（0.5以上、5.0以下）
        if (InArea == true)
        {
            //ユキノの方へ常に向かせる
           // transform.LookAt(targetposition);
            if (attack == false) //一回だけ
            {
                if (one)
                {
                    Targetposition = targetposition;//飛びつく位置を記憶させる
                    Vector3 attackdir = (Targetposition - Meposition);
                    rb.velocity = attackdir.normalized * 10f + new Vector3(0, 9.81f, 0);
                    Debug.Log(Targetposition);
                    one = false;
                }
                attack = true;
            }    
        }
        else
        {
            one = true;
            attack = false;
            this.transform.rotation = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y, 0.0f);
        }

        //雑魚とユキノ間の距離を計算
        Vector3 Bpos = target.transform.position;
        float distance = Vector3.Distance(Apos, Bpos);
        Vector3 Direction = target.position - Apos;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (distance <= DetectDist & searchAngle > targetAngle)
        {
            InArea = true;
        }
        else
        {
            InArea = false;
        }
        //ノックバックの処理
        if(knockback == true)
        {
            preposition = new Vector3(transform.position.x,0f,transform.position.z);//ノックバックした瞬間の位置を記憶させる
        }
        float knockdistance = Vector3.Distance(preposition, Meposition);//ぶっ飛んでる最中のエネミー1の現在地とノックバックしたときの距離を測る
        if (knockdistance >= Knockstopdistance)
        {
            rb.velocity = Vector3.zero;//止まる
            rb.velocity = new Vector3(0, -10f, 0);
        }

        if(isGround == true)
        {
            if (attack == true)//飛びつきの着地時処理
            {
                rb.velocity = Vector3.zero;//止まる
                attack = false;
                one = true;
                Debug.Log(attack);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//雪だるま状態かつ動いている状態で雑魚的に当たった時、敵をノックバック
        {
            if (Hp > 0 & YukinoScript.index == 1)//変更点
            {
                knockback = true;
                Vector3 knockbackdir = (targetposition - Meposition) ;
                rb.velocity = -knockbackdir *10f + new Vector3(0,9.81f,0);
                Debug.Log(knockbackdir);
            }
        if(Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag =="Ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Hp > 0 & YukinoScript.index == 1)
            {
                knockback = false;
                Hp -= 1;
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
        
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            
        }
    }
}
