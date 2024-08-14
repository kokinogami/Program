using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWaves : MonoBehaviour
{
    private ParticleSystem ps;
    private float elapsedScaleUpTime = 0f;//�傫������p�̌o�ߎ���
    private float elapsedDeleteTime = 0f;//�p�[�e�B�N���폜�p�̌o�ߎ���
    [SerializeField] private float scaleUptime = 0.03f;//�傫������Ԋu����
    [SerializeField] private float scaleUprate = 0.1f;//�傫�����銄��
    [SerializeField] private float deleteTime = 3f;
    [SerializeField] int damage = 20;
    //�@Particle�^�̃C���X�^���X����
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.SetCollider(0, GameManager.Main.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        elapsedScaleUpTime += Time.deltaTime;
        elapsedDeleteTime += Time.deltaTime;
        if (elapsedDeleteTime >= deleteTime)//3�b��폜
        {
            Destroy(gameObject);
        }
        if (elapsedScaleUpTime > scaleUptime)
        {
            transform.localScale += new Vector3(scaleUprate, scaleUprate, scaleUprate);
            elapsedScaleUpTime = 0f;
        }
    }
    private void OnParticleTrigger()
    {
        if (ps != null)
        {
            //�@Inside�AEnter�̃p�[�e�B�N�����擾
            int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            if (numInside != 0 || numEnter != 0)
            {
                Debug.Log("wave");
                GameManager.Main.OnDamage(damage, true);//���L�m�Ƀ_���[�W
            }
            //�@�킩��₷���L�����N�^�[�ƐڐG�����p�[�e�B�N���̐F��ԂɕύX
            for (int i = 0; i < numInside; i++)
            {
                ParticleSystem.Particle p = inside[i];
                p.startColor = new Color32(255, 0, 0, 255);
                inside[i] = p;
            }

            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(255, 0, 0, 255);
                enter[i] = p;
            }
            //�@�p�[�e�B�N���f�[�^�̐ݒ� 
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        }
    }
}

