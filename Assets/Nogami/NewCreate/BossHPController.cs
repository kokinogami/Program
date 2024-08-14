using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class BossHPController : MonoBehaviour
{
    public int HP = 240;                          //ボスのHP量
    const int MaxHP = 240;                        //ボスの最大HP値
    float HP_Gauge_Value = 240;                   //表示しているHPゲージの値
    float start_gauge_Value = 0;                  //減少アニメーションの開始地点
    float gauge_reduce_count = 0f;                //カウントダウン用
    [SerializeField] float gauge_reduce_time;     //ゲージが減少し終わる時間

    float invincible_count = 0;                   //カウントダウン用
    [SerializeField] float invincible_time = 5f;  //無敵判定時間
    float DamagereactionCount = 0;                //カウントダウン用
    float DamagereactionTime = 0.3f;              //ダメージリアクション時間

    Material[] FirstMats;                         //ユキノの元のマテリアル配列（ダメージリアクション用に保存
    Material[] CurrentMats;                       //ユキノの現在のマテリアル配列（ダメージリアクション用に取得）
    Material[] ChangeMats = new Material[1];      //ユキノの現在のマテリアル配列（ダメージリアクション用に取得）
    [SerializeField] Material DamageMat;          //被ダメージ時のユキノのマテリアル
    MeshRenderer[] BossMeshRenderer;

    [SerializeField] Slider slider;

    bool firstMoveGauge = true;

    [SerializeField] GameObject TimelineInstanceBossafter;
    [SerializeField] GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        slider.maxValue = MaxHP;
        slider.value = 0;
        BossMeshRenderer = GetComponentsInChildren<MeshRenderer>();
        FirstMats = new Material[BossMeshRenderer.Length];
        CurrentMats = new Material[BossMeshRenderer.Length];
        Debug.Log(FirstMats);
        for (int i = 0; i < BossMeshRenderer.Length; i++)
        {
            FirstMats[i] = BossMeshRenderer[i].materials[0];
            CurrentMats[i] = BossMeshRenderer[i].materials[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.retryBoss) { return; }
        if (firstMoveGauge)
        {
            gauge_reduce_count += Time.deltaTime / 0.5f;
            HP_Gauge_Value = Mathf.Lerp(0, HP, gauge_reduce_count);
            slider.value = HP_Gauge_Value;
            if (MaxHP == slider.value) firstMoveGauge = false;
            return;
        }

        invincible_count -= Time.deltaTime;
        if (HP < HP_Gauge_Value)
        {
            gauge_reduce_count += Time.deltaTime / gauge_reduce_time;
            HP_Gauge_Value = Mathf.Lerp(start_gauge_Value, HP, gauge_reduce_count);
            slider.value = HP_Gauge_Value;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnDamage(Vector3.zero);
        }
    }
    private void LateUpdate()
    {
        DamageReactionJudge();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (GameManager.Main.index == 1)
    //        {
    //            OnDamage();
    //        }
    //    }
    //}
    public void OnDamage(Vector3 hitpoint)
    {
        if (invincible_count <= 0 && HP > 0)
        {
            int damage = 10;
            if (GameManager.Main.nowDushBuff == YukinoMain.dushBuffState.step2)
            {
                float Cdamega = damage * 1.5f;
                damage = (int)Cdamega;
            }
            else if (GameManager.Main.nowDushBuff == YukinoMain.dushBuffState.step3)
            {
                damage = damage * 2;
            }

            HP -= damage;
            gauge_reduce_count = 0;
            start_gauge_Value = HP_Gauge_Value;
            invincible_count = invincible_time;
            DamageReaction();
            GameObject hitEf = Instantiate(HitEffect, hitpoint, Quaternion.identity);
            hitEf.transform.localScale = Vector3.one * 1.7f;

            GameManager.Sound.SetCueName("Boss_Damage");
            GameManager.Sound.OnSound();

            if (HP <= 0)
            {
                TimelineInstanceBossafter.SetActive(true);
                GameManager.Main.gameObject.SetActive(false);
            }
        }
    }
    void DamageReaction()
    {
        //ダメージリアクション
        DamagereactionCount = DamagereactionTime;
        for (int i = 0; i < FirstMats.Length; i++)
        {
            CurrentMats[i] = DamageMat;
            ChangeMats[0] = CurrentMats[i];
            BossMeshRenderer[i].materials = ChangeMats;
        }
    }
    void DamageReactionJudge()//ダメージリアクションの継続判定
    {
        if (DamagereactionCount > 0)
        {
            DamagereactionCount -= Time.deltaTime;
        }
        else if (CurrentMats[0] != FirstMats[0])
        {
            for (int i = 0; i < FirstMats.Length; i++)
            {
                CurrentMats[i] = FirstMats[i];
                ChangeMats[0] = CurrentMats[i];
                BossMeshRenderer[i].materials = ChangeMats;
            }
        }
    }
    public void firstGauge()
    {
        gauge_reduce_count = 0;
        firstMoveGauge = true;
    }
}
