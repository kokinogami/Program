using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shindo_Action : MonoBehaviour
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
        ConnectIce();
        Joushoukiryu();
        if (Main.Move.magnitude == 0 && Main.index == 0 && Main.Ground)
        {
            Main.rb.velocity = new Vector3(0.0f, Main.rb.velocity.y, 0.0f);
            Main.rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            //Main.yukinoanime.Walkfin();
        }
    }
    void ConnectIce()//ダッシュ中、空中にいれば氷の床を生成
    {
        if (Main.index == 1 && Main.connectCoolTime <= 0.0f) //ダッシュ中、空中にいれば氷の床を生成
        {
            if (Physics.Raycast(Main.body.transform.position, Vector3.down, out Main.hit, 5.1f) == false && Main.HP > 1)
            {
                Main.rb.AddForce(0.0f, -1 * Main.rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                Vector3 horisonMove = new Vector3(Main.rb.velocity.x, 0.0f, Main.rb.velocity.z);
                Vector3 verticalMove = new Vector3(0.0f, Main.rb.velocity.y, 0.0f);

                Vector3 IcePosition = Main.body.transform.position + Main.bodyHeight + horisonMove.normalized * 2.5f;
                Instantiate(Main.Ice, IcePosition, Quaternion.identity);  //氷の床を生成
                Main.HP -= 1;//設置するとHPが-1される
                Main.TimeCount = 3.0f;
            }
        }

        Main.connectCoolTime -= Time.deltaTime;
    }
    private void Joushoukiryu()
    {
        if (Main.Grider == true)
        {
            if (Main.tenjou == true)
            {
                Main.rb.AddForce(0.0f, 9.8f, 0.0f);
            }

            else if (Main.joushou == true)
            {
                Main.rb.velocity = new Vector3(0.0f, 9.83f, 0.0f);
            }
        }
    }
}
