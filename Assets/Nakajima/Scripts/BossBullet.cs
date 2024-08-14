using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float BulletSpead;

    [SerializeField]
    private float DestroyTime;

    private Vector3 PlayerVec;

    private float CountTime;

    public GameObject BOSS;
    private GameObject Player;
    private Vector3 pos;

    void Start()
    {
        Player = BOSS.GetComponent<Boss>().PlayerObject;
        pos = -(this.gameObject.transform.position - Player.transform.position).normalized;
        this.gameObject.transform.LookAt(Player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            CountTime += Time.deltaTime;
            rb.position += pos * BulletSpead;
            if(CountTime >= DestroyTime)
            {
                //Destroy(this.gameObject);
            }
        }
    }
}
