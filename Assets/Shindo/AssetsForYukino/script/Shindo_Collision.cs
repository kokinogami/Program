using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shindo_Collision : MonoBehaviour
{
    Shindo_Main Main;
    // Start is called before the first frame update
    void Start()
    {
        Main = GetComponent<Shindo_Main>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//地面との接触判定
        {
            Main.Ground = true;
            Main.Grider = false;
            Main.rb.drag = 0.5f;
            Main.mushi = 1;
            Main.griderObj.SetActive(false);
            Main.mushiObj.SetActive(false);
            if (Main.Hipdrop == true)
            {
                Main.Hipdrop = false;
                Main.childObject[Main.index].SetActive(false);
                Main.index = 0;
                Main.childObject[Main.index].SetActive(true);
            }
        }
        if (collision.gameObject.tag == "enemy")
        {
            if (Main.index == 0)
            {
                Main.HP -= 10;
                Main.TimeCount = 3.0f;
            }
        }
        if (collision.gameObject.tag == "JumpPad")//ジャンプ台
        {
            Main.Ground = false;
            Main.connectCoolTime = 1.0f;
            /*Vector3 JumpDir = collision.transform.rotation * new Vector3(0.0f, 8.0f, -24.0f);
            if (Physics.Raycast(Main.body.transform.position + 0.6f * Main.bodyHeight, new Vector3(Main.rb.velocity.x, 0.0f, Main.rb.velocity.z), out Main.hit, 4.0f) && Main.hit.normal.y >= 0.8f)
            {
                Main.Ground = false;
                Main.rb.velocity = Vector3.zero;
                Main.rb.AddForce(JumpDir, ForceMode.Impulse);
            }*/
            Main.rb.constraints |= RigidbodyConstraints.FreezeRotationY;
        }
        if (collision.gameObject.tag == "BrokenObject")
        {
            if (Main.index == 1)
            {
                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(Main.explosionPrefab, collision.transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);//エフェクトの大きさ設定

            }
        }
        if (collision.gameObject.tag == "zako")
        {
            Main.HP -= 10;
            Main.TimeCount = 3.0f;
        }
        if (collision.gameObject.tag == "DeathZone")//落下判定とリスポーン処理
        {
            this.transform.position = Main.Startpos;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//地面との接触判定
        {
            Main.Ground = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("heal") == true)//回復アイテム
        {
            Main.HP += 100;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("joushou"))
        {
            Main.joushou = true;
        }
        if (other.CompareTag("tenjou"))
        {
            Main.tenjou = true;
        }
        if (other.CompareTag("checkPoint"))//リスポーン地点の変更
        {
            Main.Startpos = other.transform.position;
        }
        if (other.CompareTag("DeathZone"))//落下判定とリスポーン処理
        {
            Main.rb.velocity = Vector3.zero;
            this.transform.position = Main.Startpos;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("joushou"))
        {
            Main.joushou = false;
        }

        if (other.CompareTag("tenjou"))
        {
            Main.tenjou = false;
        }
        if (other.CompareTag("Ground"))
        {
            Main.Ground = false;
        }
    }
}
