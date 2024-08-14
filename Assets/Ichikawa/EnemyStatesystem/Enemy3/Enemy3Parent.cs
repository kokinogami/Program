using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Parent : MonoBehaviour
{
    public List<GameObject> Others;
    EnemyDestroyEvent destroyEvent;
    [SerializeField] public GameObject HitEffect;
    public Enemy3 script;
    public float elapsedtime = 0f;
    float rate;
    public float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out destroyEvent);
        RotateSpeed = script.LookSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.look == true & script.Insight == true)
        {
            Vector3 sightPos = new Vector3(script.target.position.x, transform.position.y, script.target.position.z);//下向かないようにyのみエネミー3の高さ
            Quaternion targetRotation = Quaternion.LookRotation(sightPos - transform.position);
            elapsedtime += Time.deltaTime;
            rate = Mathf.Clamp01(elapsedtime / RotateSpeed);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rate);
        }
        if(script.InArea == false & script.backArea == false)
        {
            elapsedtime = 0f;
        }
    }
    public void Detect()//検知関数
    { //雑魚とユキノ間の距離を計算
        float DetectDistance = Mathf.Pow(script.DetectDist, 2);
        Vector3 Apos = this.transform.position;
        // transform.position = new Vector3(Apos.x, Mathf.Clamp(Apos.y, raypos.y, raypos.y + 7.0f), Apos.z);//移動制限（0以上、7.0以下）
        Vector3 Bpos = script.target.transform.position;
        Vector3 Direction = Bpos - Apos;
        float distance = Direction.sqrMagnitude;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (distance < DetectDistance & targetAngle < script.searchAngle)
        {
            script.InArea = true;
        }
        else if (distance < DetectDistance / 2 & targetAngle >= script.searchAngle)//背後の検知
        {
            script.backArea = true;
        }
        else
        {
            script.InArea = false;
            script.backArea = false;
        }
    }
    public void waitDetect()//検知関数との違いは、一回見つけてるため、距離が離れる以外目では追い続ける仕様にしたいから、距離の条件のみ
    {
        float dis = Mathf.Pow(script.DetectDist, 2);
        Vector3 apos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 bpos = new Vector3(script.target.position.x, 0f, script.target.position.z);
        Vector3 measure = bpos - apos;
        float measuredist = measure.sqrMagnitude;
        if (measuredist <= dis)
        {
            script.Insight = true;
        }
        else
        {
            script.Insight = false;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < Others.Count; i++)
        {
            Destroy(Others[i]);
            Instantiate(HitEffect, transform.position, Quaternion.identity);
        }
        destroyEvent.DeathEnemy();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            if (GameManager.Main.index == 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
