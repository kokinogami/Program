using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Demo : MonoBehaviour
{
    int HP = 1;
    private GameObject Yukino;
    public YukinoMain script;
    private GameObject gameSystem;
    private EnemyCounter enemyCount;
    [SerializeField] private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        script = script = GameManager.Main;
        Yukino = script.gameObject;
    }
    private void Update()
    {
        transform.LookAt(Yukino.transform.position);
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = Yukino.transform.position;
        float distance = Vector3.Distance(Apos, Bpos);
        /*if (distance < 2.0f)
        {
            if (script.index == 1 || script.DushCount > 0.0f)
            {
                HP -= 1;
                Instantiate(hitEffect, Apos, Quaternion.identity);
                if (HP <= 0)
                {
                    Time.timeScale = script.hitStopSpeed;
                    script.hitStop = 0.025f;
                    enemyCount.enemyNum -= 1;
                    Destroy(this.gameObject);
                }
            }
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (script.index == 1 || script.Dush)
            if(script.Dush)
            {
                Vector3 Apos = this.transform.position;
                HP -= 1;
                Instantiate(hitEffect, Apos, Quaternion.identity);
                if (HP <= 0)
                {
                    //enemyCount.enemyNum -= 1;
                    Time.timeScale = script.hitStopSpeed;
                    script.hitStop = 0.025f;
                    Destroy(this.gameObject);
                }
            }
        }
        if (other.CompareTag("HipDropCollider")){
            Vector3 Apos = this.transform.position;
            HP -= 1;
            Instantiate(hitEffect, Apos, Quaternion.identity);
            if (HP <= 0)
            {
                //enemyCount.enemyNum -= 1;
                Time.timeScale = script.hitStopSpeed;
                script.hitStop = 0.025f;
                Destroy(this.gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        //GameManager.EnemyKill();
    }
}
