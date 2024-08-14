using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public int GaugeQuantity;
    public Slider GaugeMain;//各ゲージ
    public Slider Gauge2;
    public Slider Gauge3;
    public int HP;//㏋数値
    public int HPmax;
    public float TimeCount;//自動回復インターバル
    public bool Count;//インターバル管理
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        GaugeQuantity = 2;
        GaugeMain.value = 100;
        GaugeMain.maxValue = 100;
        Gauge2.value = 0;
        Gauge2.maxValue = 100;
        Gauge3.value = 0;
        Gauge3.maxValue = 100;
        HP = 100;
        HPmax=100*GaugeQuantity;
        TimeCount = 3.0f;
        Count = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))//HP数値変更
        {
            HP += 10;
        }
        if (Input.GetKey(KeyCode.Q)&HP>=10)
        {
            HP -= 10;
            TimeCount = 3.0f;
        }
        if (Input.GetKey(KeyCode.O)&GaugeQuantity<3)//HPバー増加
        {
            UI.SetActive(true);
            GaugeQuantity++;
            HPmax = 100 * GaugeQuantity;
        }
        if (HP>HPmax)//最大HP制御
        {
            HP = HPmax;
        }
        if (HP < 100&Count==false)//自動回復と制御
        {
            HP = HP + 1;
        }
        GaugeMain.value = HP;//UIの変更
        Gauge2.value = HP - 100;
        Gauge3.value = HP - 200;
        if (TimeCount > 0.0f)//自動回復のインターバル制御
        {
            TimeCount -= Time.deltaTime;
            Count = true;
        }
        else {
            Count = false;
        }
    }
}