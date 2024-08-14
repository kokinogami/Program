using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwaves : MonoBehaviour
{
    public GameObject enemy3;
    Enemy3 script;
    private float elapsedScaleUpTime = 0f;//大きくする用の経過時間
    private float elapsedDeleteTime = 0f;//削除用の経過時間
    [SerializeField] private float scaleUptime = 0.1f;//大きくする間隔時間
    [SerializeField] private float scaleUprate = 0.05f;//大きくする割合
    [SerializeField] private float deleteTime = 2f;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedScaleUpTime += Time.deltaTime;
        elapsedDeleteTime += Time.deltaTime;
        if (elapsedDeleteTime >= deleteTime)//六秒後削除
        {
            Destroy(gameObject);
        }
        if (elapsedScaleUpTime > scaleUptime)
        {
            transform.localScale += new Vector3(scaleUprate, scaleUprate, scaleUprate);
            elapsedScaleUpTime = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        if (other.CompareTag("Player"))
        {
            Debug.Log("2");
            GameManager.Main.OnDamage(damage, true);
            Debug.Log("ouch");
        }
    }
}
