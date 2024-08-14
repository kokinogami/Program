using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2sphere : MonoBehaviour
{
    public Enemy2 Dscript;
    public bool movestop;
    Vector3 Knocktargetpos;
    Vector3 Mepos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //GameManager.Main.OnDamage(Dscript.RoundSphereDamage,true);
            movestop = true;//�{�̂̃L�����N�^�[�R���g���[���[���~�߂邽�߂Ɏg���A���L�m�����グ�h�~��
            if (Dscript.rush == true)
            {
                GameManager.Main.OnDamage(Dscript.RushDamage, true);
                /* //�@�e���Փ˂����ʒu�𒆐S�ɔ��aradius�ȓ��ɂ���R���C�_���擾
                 var colliders = Physics.OverlapSphere(transform.position, radius);
                 //�@�g��for���ŃR���C�_�����Ɏ�肾��
                 foreach (var collider in colliders)*/
                Knocktargetpos = new Vector3(collision.gameObject.transform.position.x, 0, collision.gameObject.transform.position.x);
                Mepos = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 knockdir = (Knocktargetpos - Mepos);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(-knockdir * Dscript.RushknockPower + new Vector3(0, Dscript.RushUpPower, 0), ForceMode.VelocityChange);
            }
            if (GameManager.Main.index == 1)
            {
                Dscript.nodamage = true;
                Dscript.nodamageTime = 0.5f;
                partKnockback(collision);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movestop = false;
        }
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