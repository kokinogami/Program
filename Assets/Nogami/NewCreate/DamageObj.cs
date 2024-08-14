using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    [SerializeField] int damage;//受けるダメージ
    [SerializeField] float damageoolTime;//ダメージ間隔
    bool hitDamage;//ダメージを受ける状態
    float damagecount;//ダメージ間隔カウント用
    float Collitioncount;//接触バグ用のカウント Stayが呼ばれなくなった後のカウント

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collitioncount -= Time.deltaTime;//接触バグ対処
        damagecount -= Time.deltaTime;//ダメージを受けるカウント

        if (hitDamage && damagecount < 0)
        {
            GameManager.Main.OnDamage(damage, false);//ダメージ関数
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
