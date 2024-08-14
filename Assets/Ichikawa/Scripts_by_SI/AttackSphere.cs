using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSphere : MonoBehaviour
{
    [SerializeField] private float deleteTime = 6f;
    YukinoMain YukinoScript;
    private GameObject Yukino;
    public int Damage;
    public GameObject DestroyEffect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0.0f)
        {
            DestroyEffect.transform.parent = null;
            DestroyEffect.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(DestroyEffect != null)
            {
                DestroyEffect.transform.parent = null;
                DestroyEffect.SetActive(true);
            }
            GameManager.Main.OnDamage(Damage, true);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "RespawnableStage")
        {
            if(DestroyEffect != null)
            {
                DestroyEffect.transform.parent = null;
                DestroyEffect.SetActive(true);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground")
        {
            if(DestroyEffect != null)
            {
                DestroyEffect.transform.parent = null;
                DestroyEffect.SetActive(true);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "enemy")
        {
            if(DestroyEffect != null)
            {
                DestroyEffect.transform.parent = null;
                DestroyEffect.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
