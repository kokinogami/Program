using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DounateEnemy : MonoBehaviour
{
    CharacterController Controller;
    YukinoMain YukinoScript;
    private GameObject Yukino;
    [SerializeField] int DetectDist = 20;
    [SerializeField] int farDist = 40;
    [SerializeField]public int RoundSphereDamage;
    [SerializeField] public int AttackSphereDamage;
    float MoveSpeed = 2.0f;
    public float searchAngle = 45f;
    private int randomAt;
    private float Attackcount;

    [SerializeField]public float movetime = 3.0f;//ユキノの目線に合わせるのにかける時間
    float elapsedTime = 0f;//経過時間
    float rate;
    Vector3 postpositon;

    public bool InArea;
    public bool far;
    public bool Innear;

    bool one;

    public bool shooting;
    public bool bodyattack;
    bool lottery;

    public Transform target;
    public Transform YukinoEye;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Yukino = GameObject.FindWithTag("Player");
        YukinoScript = Yukino.GetComponent<YukinoMain>();
        one = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = transform.position; 
        transform.position = new Vector3(Pos.x, Mathf.Clamp(Pos.y, 2.0f, 5.0f), Pos.z);//移動制限（2.0以上、5.0以下）
        elapsedTime += Time.deltaTime;
        rate = Mathf.Clamp01(elapsedTime / movetime);

        if (Innear == true)//近距離のとき抽選
        {
            if (lottery == true)
            {
                randomAt = Random.Range(0, 9);
                lottery = false;
            }
           if(randomAt<3)
            {
                bodyattack = true;
                BodyAttack();
            }
           else if(randomAt >= 3)
            {
                shooting = true;
                postpositon = new Vector3(Pos.x, YukinoEye.position.y, Pos.z);
                transform.position = Vector3.Lerp(Pos, postpositon, rate);  
            }
        }
        
        if (far == true)
        {
            if(one)
            {
                postpositon = new Vector3(Pos.x, YukinoEye.position.y, Pos.z);
                transform.position = Vector3.Lerp(Pos, postpositon, rate);
                one = false;
            }
        }

        //雑魚とユキノ間の距離を計算　ここで近い時と遠い時の場合分けが必要近い時はInnear, 遠い時はfar まず、ここの設定をする
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = target.transform.position;
        float distance = Vector3.Distance(Apos, Bpos);
        Vector3 Direction = target.position - Apos;
        float targetAngle = Vector3.Angle(Direction, transform.forward);

        if (distance <= DetectDist & searchAngle > targetAngle)
        {
            Innear = true;
            InArea = true;
        }
        else
        {
            Innear = false;
            lottery = true;
            shooting = false;
            bodyattack = false;
        }

        if(DetectDist <distance & distance <= farDist & searchAngle > targetAngle)
        {
            far = true;
            InArea = true;
            one = true;
        }
        else
        {
            far = false;
        }

        if(farDist < distance || targetAngle < searchAngle)
        {
            InArea = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (YukinoScript.index == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    void BodyAttack()//近い時に３割でこの動き
    {
        //ユキノの方へ向かせ、移動する（襲う）
        Vector3 direction = target.position - this.transform.position;
        direction = direction.normalized;
        transform.LookAt(target);
        Vector3 velocity = direction * MoveSpeed;
        Controller.Move(velocity * 3.0f * Time.deltaTime);
    }
}
