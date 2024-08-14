using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tobitukienemy : MonoBehaviour
{
    YukinoMain YukinoScript;//�i�ύX�j���YukinoMain�X�N���v�g�̒��̐��l�i�ϐ��j���Q�Ƃ��邽�߂̂���
    private GameObject Yukino;
    [SerializeField] int DetectDist = 20;
    [SerializeField] public float searchAngle = 45f;
    [SerializeField] float Knockstopdistance;
    public bool InArea;
    public Transform target;
    private bool knockback;
    private bool isGround;//�g���Ƃ�����A��т��Ē��n�����Ƃ��ɃN�[���^�C���������,�~�߂���B���߂Ĕ�т��������L���b�V��������ʂ���
    public bool attack;
    public bool one;
    private Vector3 targetposition;
    private Vector3 Meposition;
    private Vector3 preposition;
    private Vector3 Targetposition;
    Rigidbody rb;
    [SerializeField] int Hp; //�ύX�_
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
        transform.position = new Vector3(Apos.x, Mathf.Clamp(Apos.y, 0.0f, 2.0f), Apos.z);//�ړ������i0.5�ȏ�A5.0�ȉ��j
        if (InArea == true)
        {
            //���L�m�̕��֏�Ɍ�������
           // transform.LookAt(targetposition);
            if (attack == false) //��񂾂�
            {
                if (one)
                {
                    Targetposition = targetposition;//��т��ʒu���L��������
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

        //�G���ƃ��L�m�Ԃ̋������v�Z
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
        //�m�b�N�o�b�N�̏���
        if(knockback == true)
        {
            preposition = new Vector3(transform.position.x,0f,transform.position.z);//�m�b�N�o�b�N�����u�Ԃ̈ʒu���L��������
        }
        float knockdistance = Vector3.Distance(preposition, Meposition);//�Ԃ����ł�Œ��̃G�l�~�[1�̌��ݒn�ƃm�b�N�o�b�N�����Ƃ��̋����𑪂�
        if (knockdistance >= Knockstopdistance)
        {
            rb.velocity = Vector3.zero;//�~�܂�
            rb.velocity = new Vector3(0, -10f, 0);
        }

        if(isGround == true)
        {
            if (attack == true)//��т��̒��n������
            {
                rb.velocity = Vector3.zero;//�~�܂�
                attack = false;
                one = true;
                Debug.Log(attack);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")//�Ⴞ��܏�Ԃ������Ă����ԂŎG���I�ɓ����������A�G���m�b�N�o�b�N
        {
            if (Hp > 0 & YukinoScript.index == 1)//�ύX�_
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
