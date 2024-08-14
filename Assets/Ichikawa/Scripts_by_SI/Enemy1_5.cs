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
    private int randomAt;//�A�j���[�V�����U���������
    private float currentTime = 0f;
    private float changetime = 0f;
    Vector3 Knocktargetpos;
    Vector3 KnockMepos;
    Vector3 preposition;
    public bool InArea;
    bool spherestart;
    bool death = false;
    bool knockback = false;
    //�A�j���[�V�����؂�ւ�bool
    bool stop;
    bool downL;
    bool downR;
    bool upL;
    bool upR;
    bool one;
    //�������܂�
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
            //���L�m�̕��֏�Ɍ�������
            transform.LookAt(GameManager.Main.transform);
            if (currentTime > span)//3�b�����Ƃ�10�_���[�W�U������
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
        //�G���ƃ��L�m�Ԃ̋������v�Z
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
        //�m�b�N�o�b�N�̏�����ɐ������ł���~�߂邽�߂̂���
        if (knockback == true)
        {
            preposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);//�m�b�N�o�b�N���Ă���Ƃ��̍X�V����錻�ݒn
        }
        float knockdistance = Vector3.Distance(preposition, KnockMepos);//�Ԃ����ł�Œ��̃G�l�~�[1�̌��ݒn�ƃm�b�N�o�b�N�����Ƃ��̋����𑪂�
        if (knockdistance >= Knockstopdistance && knockback == true)
        {
            rb.velocity = Vector3.zero;//�~�܂�
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
        var Shot = Instantiate(Sphere, transform.position + transform.forward * 7f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));//�΂̋ʃV�F�[�_�[�̌������ߌ�
        Shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * SphereSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//�Ⴞ��܏�Ԃ������Ă����ԂŎG���I�ɓ����������A�G���m�b�N�o�b�N
        {
            PlayerHit();
        }
        if (collision.gameObject.tag == "Ground" && death == true)//�m�b�N�o�b�N���Ēn�ʊђʂ���O�ɔj�󂷂�i�G�l�~�[1.5�͏㉺�ɂ��m�b�N�o�b�N���ėǂ��Ǝv�����j
        {
            Vector3 Apos = this.transform.position;
            Instantiate(HitEffect, Apos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//�Ⴞ��܏�Ԃ������Ă����ԂŎG���I�ɓ����������A�G���m�b�N�o�b�N
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
                //�m�b�N�o�b�N�ǋL
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

