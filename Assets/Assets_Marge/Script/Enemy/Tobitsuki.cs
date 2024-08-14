using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobitsuki : MonoBehaviour
{
    private Animator animator;
    private string tobuStr = "tobituki";
    private string waitStr = "wait";
    int DetectDist = 10;
    int HP = 1;
    public float searchAngle = 45f;
    public bool InArea;

    public Transform target;
    private GameObject Yukino;
    public YukinoMain script;
    private GameObject gameSystem;
    private EnemyCounter enemyCount;
    [SerializeField] private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.animator.SetBool(waitStr, true);

        Yukino = GameObject.FindWithTag("Player");
        target = Yukino.transform;
        script = Yukino.GetComponent<YukinoMain>();
        gameSystem = GameObject.Find("GameSystem");
        enemyCount = gameSystem.GetComponent<EnemyCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K) && script.index == 1)
        {
            Destroy(this.gameObject);
        }
        if (InArea == true)
        {
            //ƒ†ƒLƒm‚Ì•û‚Öí‚ÉŒü‚©‚¹‚é
            transform.LookAt(target);
            this.animator.SetBool(tobuStr, true);
            this.animator.SetBool(waitStr, false);
        }
        else
        {
            this.animator.SetBool(tobuStr, false);
            this.animator.SetBool(waitStr, true);
        }
        /* if (InArea == false)
         {
             this.animator.SetBool(tobuStr, false);
             this.animator.SetBool(waitStr, true);
         }*/

        //ŽG‹›‚Æƒ†ƒLƒmŠÔ‚Ì‹——£‚ðŒvŽZ
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = target.transform.position;
        float distance = Vector3.Distance(Apos, Bpos);
        Vector3 Direction = target.position - Apos;
        float targetAngle = Vector3.Angle(transform.forward, Direction);
        if (distance > DetectDist & searchAngle > targetAngle)
        {
            InArea = false;
        }
        if (distance < DetectDist & searchAngle < targetAngle)
        {
            InArea = true;
        }

        if (distance < 2.0f && script.index == 1)
        {
            HP -= 1;
            Instantiate(hitEffect, Apos, Quaternion.identity);
            if (HP <= 0)
            {
                enemyCount.enemyNum -= 1;
                Destroy(this.gameObject);
            }
        }
    }
    /*public void attack()
    {
        Debug.Log("UŒ‚I");
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (script.index == 1)
            {
                HP -= 1;
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}