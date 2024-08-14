using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class BossHPController : MonoBehaviour
{
    public int HP = 240;                          //�{�X��HP��
    const int MaxHP = 240;                        //�{�X�̍ő�HP�l
    float HP_Gauge_Value = 240;                   //�\�����Ă���HP�Q�[�W�̒l
    float start_gauge_Value = 0;                  //�����A�j���[�V�����̊J�n�n�_
    float gauge_reduce_count = 0f;                //�J�E���g�_�E���p
    [SerializeField] float gauge_reduce_time;     //�Q�[�W���������I��鎞��

    float invincible_count = 0;                   //�J�E���g�_�E���p
    [SerializeField] float invincible_time = 5f;  //���G���莞��
    float DamagereactionCount = 0;                //�J�E���g�_�E���p
    float DamagereactionTime = 0.3f;              //�_���[�W���A�N�V��������

    Material[] FirstMats;                         //���L�m�̌��̃}�e���A���z��i�_���[�W���A�N�V�����p�ɕۑ�
    Material[] CurrentMats;                       //���L�m�̌��݂̃}�e���A���z��i�_���[�W���A�N�V�����p�Ɏ擾�j
    Material[] ChangeMats = new Material[1];      //���L�m�̌��݂̃}�e���A���z��i�_���[�W���A�N�V�����p�Ɏ擾�j
    [SerializeField] Material DamageMat;          //��_���[�W���̃��L�m�̃}�e���A��
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
        //�_���[�W���A�N�V����
        DamagereactionCount = DamagereactionTime;
        for (int i = 0; i < FirstMats.Length; i++)
        {
            CurrentMats[i] = DamageMat;
            ChangeMats[0] = CurrentMats[i];
            BossMeshRenderer[i].materials = ChangeMats;
        }
    }
    void DamageReactionJudge()//�_���[�W���A�N�V�����̌p������
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
