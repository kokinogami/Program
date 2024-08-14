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
            movestop = true;//本体のキャラクターコントローラーを止めるために使う、ユキノ持ち上げ防止策
            if (Dscript.rush == true)
            {
                GameManager.Main.OnDamage(Dscript.RushDamage, true);
                /* //　弾が衝突した位置を中心に半径radius以内にあるコライダを取得
                 var colliders = Physics.OverlapSphere(transform.position, radius);
                 //　拡張for文でコライダを順に取りだす
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