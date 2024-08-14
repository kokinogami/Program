using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWaves : MonoBehaviour
{
    private ParticleSystem ps;
    private float elapsedScaleUpTime = 0f;//大きくする用の経過時間
    private float elapsedDeleteTime = 0f;//パーティクル削除用の経過時間
    [SerializeField] private float scaleUptime = 0.03f;//大きくする間隔時間
    [SerializeField] private float scaleUprate = 0.1f;//大きくする割合
    [SerializeField] private float deleteTime = 3f;
    [SerializeField] int damage = 20;
    //　Particle型のインスタンス生成
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
        if (elapsedDeleteTime >= deleteTime)//3秒後削除
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
            //　Inside、Enterのパーティクルを取得
            int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            if (numInside != 0 || numEnter != 0)
            {
                Debug.Log("wave");
                GameManager.Main.OnDamage(damage, true);//ユキノにダメージ
            }
            //　わかりやすくキャラクターと接触したパーティクルの色を赤に変更
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
            //　パーティクルデータの設定 
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        }
    }
}

