using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamDamage : MonoBehaviour
{
    bool damageActivated = true; //ダメージ判定有無
    [SerializeField] int damage = 5;
    [SerializeField] float damageInterval = 3.0f;
    [SerializeField] float knockBackForward = 300.0f;//ビーム照射方向への吹っ飛び
    [SerializeField] float knockBackUp = 10.0f;//上方向への吹っ飛び
    [SerializeField] CapsuleCollider yukinoCollider;
    [SerializeField] SphereCollider yukidamaCollider;

    float interval;
    GameObject player;
    Rigidbody rb;
    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        interval = damageInterval;
        ps = GetComponent<ParticleSystem>();


        if (yukinoCollider == null)
        {
            yukinoCollider = GameManager.Main.childObject[0].GetComponent<CapsuleCollider>();
        }

        if (yukidamaCollider == null)
        {
            yukidamaCollider = GameManager.Main.childObject[1].GetComponent<SphereCollider>();
        }

        ps.trigger.AddCollider(yukinoCollider);
        ps.trigger.AddCollider(yukidamaCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (damageActivated == false)
        {
            interval -= Time.deltaTime * Time.timeScale;
            if (interval <= 0)
            {
                damageActivated = true;
            }
        }
    }

    private void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if (numEnter > 0 && damageActivated)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(knockBackForward * this.transform.forward + knockBackUp * this.transform.up, ForceMode.VelocityChange);
            interval = damageInterval;
            damageActivated = false;
            GameManager.Main.OnDamage(damage, false);
        }
    }
}
