using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shindo_UI : MonoBehaviour
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
        HPGaugeControl();
    }
    void HPGaugeControl()
    {
        if (Input.GetKey(KeyCode.Q) & Main.HP >= 10)
        {
            Main.HP -= 10;
            Main.TimeCount = 3.0f;
        }

        if (Input.GetKey(KeyCode.O) & Main.GaugeQuantity < 3)//HPバー増加
        {
            Main.UI3.SetActive(true);
            Main.GaugeQuantity++;
            Main.HPmax = 100 * Main.GaugeQuantity;
        }
        if (Main.HP > Main.HPmax)//最大HP制御
        {
            Main.HP = Main.HPmax;
        }
        if (Main.HP < 100 & Main.Count == false)//自動回復と制御
        {
            Main.HP = Main.HP + 1;
        }
        Main.GaugeMain.value = Main.HP;//UIの変更
        Main.Gauge2.value = Main.HP - 100;
        Main.Gauge3.value = Main.HP - 200;
        if (Main.TimeCount > 0.0f)//自動回復のインターバル制御
        {
            Main.TimeCount -= Time.deltaTime;
            Main.Count = true;
        }
        else
        {
            Main.Count = false;
        }
    }
}
