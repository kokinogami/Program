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
       /*Switch = false;(rb.velocity.y��transform�̕��@���Ǝg���Ȃ����߁i�g����Ȃ炢��Ȃ��g���Ȃ��Ǝv���Ă�j�A
       ����ɒ���ɍs���܂ł�false,����܂ōs������true�ɂ��ĉ��~�r����Up���N�����Ȃ��悤�ɂ���*/
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //�G���ƃ��L�m�Ԃ̋������v�Z
        float DetectDistance = Mathf.Pow(DetectDist, 2);
        Vector3 Apos = this.transform.position;
        transform.position = new Vector3(Apos.x, Mathf.Clamp(Apos.y, 0.3f, 5.0f), Apos.z);//�ړ������i0�ȏ�A5.0�ȉ��j
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
                shockwave();//���m�͈͂ɂ��鎞���n�ʂɐG�ꂽ���ɏՌ��g���o��
            }
        }
       // if (Switch == true)

            if (Apos.y >= 5.0f)//���_�܂ł����Ƃ����~����
        {
            Down();
        }   
           
    }
    private void FixedUpdate()
    {
        if (InArea == true)
        {
            if (transform.position.y < 5.0f )//����5.0�܂ŏ㏸
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
       rb.MovePosition(up * Time.deltaTime );//�����̕������㏸���鎞�ɂ������Ȃ����@�iisKinematic���K�v����FixedUpdate�֐��ɗv�ύX�j
       // rb.AddForce(0, 9.83f, 0, ForceMode.VelocityChange);
    }
    private void Down()
    {
       // if (isGround == false)
        {
            /* Vector3 down = new Vector3(0, -6.0f, 0);
             rb.MovePosition(down * Time.deltaTime + transform.position);�����̓A�j���[�V�������lisGround�̔��肪���܂������Ȃ�����UseGravity�݂̂ŉ��~�����邩�l���Ă��邪�A
            ���~�r���̃X�s�[�h������Ȃ��Ȃ�������isKinematic�̂��ߏd�͂œ����Ă�����ł�����肠��*/
            rb.AddForce(0, -15f, 0, ForceMode.VelocityChange);
        }
    }
    private void shockwave()
    {
        {
            Debug.Log("aaaa");
            var shock = Instantiate(shockwaves, transform.position, Quaternion.identity);//�Ռ��g����
           // shock.GetComponent<Shockwaves>().damage = shockDamage;
        }
    }
}
