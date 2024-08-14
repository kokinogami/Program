using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakeraAbsorb : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    [SerializeField] float absorbspeed = 40.0f;
    [SerializeField] float dis_Player = 10.0f;
    [SerializeField] float getCoolTime = 3;
    [SerializeField] float timeDelta = 20;
    [SerializeField] GameObject getEffect;
    bool playerTrigger = false;
    bool tuizyuu = false;
    bool gravity = false;
    [SerializeField] bool childObject = false;

    Rigidbody rb;
    [SerializeField]MeshCollider meshCollider;
    void Start()
    {
        target = GameManager.Main.gameObject;
        TryGetComponent(out rb);
        if (rb == null) return;
        gravity = rb.useGravity;
        if (gravity == false)
        {
            rb = null;
        }
    }

    //Update is called once per frame
    void Update()
    {
        Vector3 Player = target.transform.position;
        float dis = Vector3.Distance(Player, this.transform.position);

        if (dis < dis_Player)
        {
            tuizyuu = true;
        }
        if (getCoolTime > 0)
        {
            getCoolTime -= Time.deltaTime;
            return;
        }
        if (this.gameObject.layer == 6)
        {
            this.gameObject.layer = 0;
            if (childObject == true)
            {
                GameObject childObjectKakera = this.gameObject.transform.GetChild(0).gameObject;
                childObjectKakera.layer = 0;
                childObject = false;
            }
        }
        if (tuizyuu == true)
        {
            timeDelta += Time.deltaTime;

            float speed = absorbspeed / dis;

            if (speed < timeDelta)
            {
                if (timeDelta > absorbspeed * 3)
                {
                    speed = absorbspeed;
                }
                else
                {
                    speed = timeDelta;
                }
            }

            transform.position = Vector3.MoveTowards(
            transform.position, target.transform.position,
            speed * Time.deltaTime * 3);

            if (gravity)
            {
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                gravity = false;

                if (meshCollider == null)
                {
                    meshCollider = GetComponent<MeshCollider>();
                }
                meshCollider.isTrigger = true;
            }
        }
    }

    //ìñÇΩÇ¡ÇΩÇÁè¡Ç¶ÇÈ
    void OnCollisionEnter(Collision collision)
    {
        if (CheckCountCollision(collision)) return;
        GameManager.Main.TridderCollectibleItem(this.gameObject);
        playerTrigger = true;
        GameManager.Sound.SetCueName("Get_Cure");
        GameManager.Sound.OnSound();
        Instantiate(getEffect, this.transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            Destroy(this.gameObject);
        }
        if (CheckCountTrigger(other)) return;
        GameManager.Main.TridderCollectibleItem(this.gameObject);
        playerTrigger = true;
        GameManager.Sound.SetCueName("Get_Cure");
        GameManager.Sound.OnSound();
        Instantiate(getEffect, this.transform.position, Quaternion.identity);
    }
    bool CheckCountTrigger(Collider other)
    {
        if (other.tag != "Player") return true;
        if (playerTrigger == true) return true;
        if (getCoolTime > 0) return true;
        return false;
    }
    bool CheckCountCollision(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return true;
        if (playerTrigger == true) return true;
        if (getCoolTime > 0) return true;
        return false;
    }
    private void OnCollisionStay(Collision collision)//EnterÇ™ê≥èÌÇ…ìÆçÏÇµÇ»Ç©Ç¡ÇΩéûÇÃó\îı
    {
        if (CheckCountCollision(collision)) return;
        GameManager.Main.TridderCollectibleItem(this.gameObject);
        playerTrigger = true;
        GameManager.Sound.SetCueName("Get_Cure");
        GameManager.Sound.OnSound();
        Instantiate(getEffect, this.transform.position, Quaternion.identity);
    }
    private void OnTriggerStay(Collider other)//EnterÇ™ê≥èÌÇ…ìÆçÏÇµÇ»Ç©Ç¡ÇΩéûÇÃó\îı
    {
        if (CheckCountTrigger(other)) return;
        GameManager.Main.TridderCollectibleItem(this.gameObject);
        playerTrigger = true;
        GameManager.Sound.SetCueName("Get_Cure");
        GameManager.Sound.OnSound();
        Instantiate(getEffect, this.transform.position, Quaternion.identity);
    }
}
