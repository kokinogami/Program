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

    [SerializeField]public float movetime = 3.0f;//���L�m�̖ڐ��ɍ��킹��̂ɂ����鎞��
    float elapsedTime = 0f;//�o�ߎ���
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
        transform.position = new Vector3(Pos.x, Mathf.Clamp(Pos.y, 2.0f, 5.0f), Pos.z);//�ړ������i2.0�ȏ�A5.0�ȉ��j
        elapsedTime += Time.deltaTime;
        rate = Mathf.Clamp01(elapsedTime / movetime);

        if (Innear == true)//�ߋ����̂Ƃ����I
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

        //�G���ƃ��L�m�Ԃ̋������v�Z�@�����ŋ߂����Ɖ������̏ꍇ�������K�v�߂�����Innear, ��������far �܂��A�����̐ݒ������
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

    void BodyAttack()//�߂����ɂR���ł��̓���
    {
        //���L�m�̕��֌������A�ړ�����i�P���j
        Vector3 direction = target.position - this.transform.position;
        direction = direction.normalized;
        transform.LookAt(target);
        Vector3 velocity = direction * MoveSpeed;
        Controller.Move(velocity * 3.0f * Time.deltaTime);
    }
}
