using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwaves : MonoBehaviour
{
    public GameObject enemy3;
    Enemy3 script;
    private float elapsedScaleUpTime = 0f;//‘å‚«‚­‚·‚é—p‚ÌŒo‰ßŽžŠÔ
    private float elapsedDeleteTime = 0f;//íœ—p‚ÌŒo‰ßŽžŠÔ
    [SerializeField] private float scaleUptime = 0.1f;//‘å‚«‚­‚·‚éŠÔŠuŽžŠÔ
    [SerializeField] private float scaleUprate = 0.05f;//‘å‚«‚­‚·‚éŠ„‡
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
        if (elapsedDeleteTime >= deleteTime)//˜Z•bŒãíœ
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
