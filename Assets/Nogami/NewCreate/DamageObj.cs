using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    [SerializeField] int damage;//�󂯂�_���[�W
    [SerializeField] float damageoolTime;//�_���[�W�Ԋu
    bool hitDamage;//�_���[�W���󂯂���
    float damagecount;//�_���[�W�Ԋu�J�E���g�p
    float Collitioncount;//�ڐG�o�O�p�̃J�E���g Stay���Ă΂�Ȃ��Ȃ�����̃J�E���g

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collitioncount -= Time.deltaTime;//�ڐG�o�O�Ώ�
        damagecount -= Time.deltaTime;//�_���[�W���󂯂�J�E���g

        if (hitDamage && damagecount < 0)
        {
            GameManager.Main.OnDamage(damage, false);//�_���[�W�֐�
            damagecount = damageoolTime;
        }

        if (Collitioncount < 0.0f)
        {
            hitDamage = false;
        }
    }
    private void FixedUpdate()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitDamage = true;
            damagecount = damageoolTime;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitDamage = false;
            damagecount = 0;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitDamage = true;
            Collitioncount = 0.1f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitDamage = true;
            damagecount = damageoolTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitDamage = false;
            damagecount = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitDamage = true;
            Collitioncount = 0.1f;
        }
    }
}
