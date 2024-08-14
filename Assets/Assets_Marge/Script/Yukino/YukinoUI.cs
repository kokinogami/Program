using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class YukinoMain :MonoBehaviour
{
    // Start is called before the first frame update
    void UIstart()
    {
        HPmax = HP * GaugeQuantity;
        GaugeMain.value = HP;
        GaugeMain.maxValue = HP;
        Gauge2.value = 0;
        Gauge2.maxValue = HP;
        Gauge3.value = 0;
        Gauge3.maxValue = HP;
    }
    // Update is called once per frame
    void UIUpdate()
    {
        HPGaugeControl();
    }
    void HPGaugeControl()
    {
        //if (Input.GetKey(KeyCode.Q) & HP >= 10)
        //{
        //    HP -= 10;
        //    HealTimeCount = HealTimeCountBackUp;
        //}

        //if (Input.GetKey(KeyCode.O) & GaugeQuantity < 3)//HPバー増加
        //{
        //    UI3.SetActive(true);
        //    GaugeQuantity++;
        //    HPmax = 100 * GaugeQuantity;
        //}
        if (healcheck())//自動回復と制御
        {
            HP = HP + 1;
        }
        if (HP > HPmax)//最大HP制御
        {
            HP = HPmax;
        }
        GaugeMain.value = HP;//UIの変更
        Gauge2.value = HP - 100;
        Gauge3.value = HP - 200;
        if (auteHealCount > 0.0f)//自動回復のインターバル制御
        {
            auteHealCount -= Time.deltaTime;
        }
        if(attackedAuteHealCount > 0.0f)
        {
            attackedAuteHealCount -= Time.deltaTime;
        }
    }
    bool healcheck()
    {
        if (Time.timeScale < 1) return false;
        if (HP >= 100) return false;
        if (Ground == false) return false;
        if (auteHealCount > 0.0f) return false;
        if (attackedAuteHealCount > 0.0f) return false;
        return true;
    }
    void GaugeDecrease(int Decrease)
    {
        HP -= Decrease;
        auteHealCount = HealTimeCount;
    }
}
