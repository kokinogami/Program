using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurinEnemy : MonoBehaviour
{
    float DetectDist = 10;
    float searchAngle = 45f;
    public bool InArea;
   // public bool Switch;
    Rigidbody rb;
    float speed;
    [SerializeField] public int shockDamage;
    public Transform target;
    private bool isGround;
    public GameObject shockwaves;
    // Start is called before the first frame update
    void Start()
    {
        isGround = false;
       /*Switch = false;(rb.velocity.yがtransformの方法だと使えないため（使えるならいらない使えないと思ってる）、
       代わりに頂上に行くまでがfalse,頂上まで行ったらtrueにして下降途中にUpが起動しないようにした*/
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //雑魚とユキノ間の距離を計算
        float DetectDistance = Mathf.Pow(DetectDist, 2);
        Vector3 Apos = this.transform.position;
        transform.position = new Vector3(Apos.x, Mathf.Clamp(Apos.y, 0.3f, 5.0f), Apos.z);//移動制限（0以上、5.0以下）
        Vector3 Bpos = target.transform.position;
        Vector3 Direction = Bpos - Apos;
        float distance = Direction.sqrMagnitude;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (distance < DetectDistance & targetAngle < searchAngle)
        {
            InArea = true;
        }
        else
        {
            InArea = false;
        }
       /* if(Apos.y >= 5.0f)
        {
            Switch = true;
        }*/
        if(InArea == true)
        {
            //  if(Switch == false)
            if (isGround == true)
            {
                shockwave();//検知範囲にいる時かつ地面に触れた時に衝撃波を出す
            }
        }
       // if (Switch == true)

            if (Apos.y >= 5.0f)//頂点までついたとき下降する
        {
            Down();
        }   
           
    }
    private void FixedUpdate()
    {
        if (InArea == true)
        {
            if (transform.position.y < 5.0f )//高さ5.0まで上昇
            {
                Up();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            //Switch = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    private void Up()
    {
       Vector3 up = new Vector3(0, 3.0f, 0);
       rb.MovePosition(up * Time.deltaTime );//ここの部分が上昇する時にがたつかない方法（isKinematicが必要かつFixedUpdate関数に要変更）
       // rb.AddForce(0, 9.83f, 0, ForceMode.VelocityChange);
    }
    private void Down()
    {
       // if (isGround == false)
        {
            /* Vector3 down = new Vector3(0, -6.0f, 0);
             rb.MovePosition(down * Time.deltaTime + transform.position);ここはアニメーション同様isGroundの判定がうまくいかないためUseGravityのみで下降させるか考えているが、
            下降途中のスピードいじれなくないか問題とisKinematicのため重力で動いてくれるんですか問題あり*/
            rb.AddForce(0, -15f, 0, ForceMode.VelocityChange);
        }
    }
    private void shockwave()
    {
        {
            Debug.Log("aaaa");
            var shock = Instantiate(shockwaves, transform.position, Quaternion.identity);//衝撃波生成
           // shock.GetComponent<Shockwaves>().damage = shockDamage;
        }
    }
}
