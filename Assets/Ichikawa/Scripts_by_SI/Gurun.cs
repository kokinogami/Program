using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gurun : MonoBehaviour
{
    public Enemy2 Dscript;
    public GameObject Sphere;
    Vector3 Knocktargetpos;
    Vector3 Mepos;
    public float SphereSpeed = 7f;
    public float time;
    public bool movestop;
    [SerializeField] float spherestoptime;
    [SerializeField] GameObject DamageEffect;
    [SerializeField] GameObject HitEffect;
    Rigidbody rb;
    // private float count;
    //private bool timestop = false;
    /*bool statechange;
    bool statechange2;*/
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*  time = 0;
          statechange2 = true;*/

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (Dscript.far == true || Dscript.shooting == true)
        {
            time += Time.deltaTime;
            if (time <= spherestoptime)
            {
                count += 1;
                if (count % 14 * Time.deltaTime == 0)//14�t���[�������Ƃ�10�_���[�W�U������
                {
                    SphereShot();
                }
            }
        }
        if(Dscript.far == false && Dscript.Innear == false && Dscript.shooting == false)//far�̋�����胆�L�m���O�ɍs�����Ƃ������܂��o��悤�ɂ���
        {
            time = 0;
            statechange = true;  
        }
        if (Dscript.far == true && Dscript.Innear == false && Dscript.shooting == false)//far�̋����Ƀ��L�m�����鎞���������ɂȂ�Ȃ��悤��񂾂�time=0��
        {
            if(statechange)
            {
                time = 0;
                statechange = false;
                statechange2 = true;   
            }
        }
        if (Dscript.far == false && Dscript.Innear == true && Dscript.shooting == true)//�ߋ����Ƀ��L�m�����鎞���I��shooting�ɂȂ��������������ɂȂ�Ȃ��悤�ɂ���
        {
            if(statechange2)
            {
                time = 0;
                statechange2 = false;
                statechange = true;
            }
        }

            if (Input.GetKeyDown(KeyCode.Escape))//���������Ԏ~�߂�֌W�̕ύX�_
        {
            if(timestop == false)
            {
                timestop = true;
                Time.timeScale = 0;
            }
            if (timestop == true)
            {
                timestop = false;
                Time.timeScale = 1;
            }
        }*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.Main.index == 1)
            {
                Dscript.nodamage = true;
                Dscript.nodamageTime = 0.5f;
                partKnockback(collision);
                Vector3 Apos = this.transform.position;
                Instantiate(DamageEffect, Apos, Quaternion.identity);
                Instantiate(HitEffect, Apos, Quaternion.identity);
                Destroy(this.gameObject);
            }
            if (Dscript.rush == true)
            {
                GameManager.Main.OnDamage(Dscript.RushDamage, true);
                Knocktargetpos = new Vector3(collision.gameObject.transform.position.x, 0, collision.gameObject.transform.position.x);
                Mepos = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 knockdir = (Knocktargetpos - Mepos);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(knockdir * Dscript.RushknockPower + new Vector3(0, Dscript.RushUpPower, 0), ForceMode.VelocityChange);
            }
            // GameManager.Main.OnDamage(Dscript.RoundSphereDamage,true);
            movestop = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movestop = false;
        }
    }
    public void SphereShot()
    {
        //var Shot = Instantiate(Sphere, transform.position+transform.forward*1.5f, Quaternion.identity);
        var Shot = Instantiate(Sphere, transform.position + transform.forward * 1.5f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));//�΂̋ʃV�F�[�_�[�̌������ߌ�
        Shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * SphereSpeed;
        Shot.GetComponent<AttackSphere>().Damage = Dscript.AttackSphereDamage;
    }
    private void partKnockback(Collision collision)
    {
        GameManager.Main.rb.velocity = Vector3.zero;
        Knocktargetpos = new Vector3(collision.gameObject.transform.position.x, 0, collision.gameObject.transform.position.x);
        Mepos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 knockdir = (Knocktargetpos - Mepos).normalized;
        GameManager.Main.rb.AddForce(knockdir * Dscript.partKnockPower + new Vector3(0, Dscript.partKnockUpPower, 0), ForceMode.VelocityChange);
        GameManager.Main.connectCoolTime = GameManager.Main.connectCoolTimeBackUp;
    }
}
