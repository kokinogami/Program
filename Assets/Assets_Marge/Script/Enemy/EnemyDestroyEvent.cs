using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDestroyEvent : MonoBehaviour
{
    /*[System.NonSerialized]*/public int listNum;
    bool onDestry = false;
    bool Unload =false;
    [SerializeField] GameObject destryEff;

    [SerializeField] List<Transform> dropKakera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestryEvent()
    {
        //Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        if (GameManager.ChangeSceenReset) return;
        if (onDestry == false) GameManager.EnemyKill();
        Instantiate(destryEff, this.transform.position, Quaternion.identity);
        GameManager.TaegwtMark.markedObj[listNum].none = true;
        GameManager.Sound.SetCueName("Enemy_Hit");
        GameManager.Sound.OnSound();
        CheckDushBuff();
    }
    void CheckDushBuff()
    {
        if (GameManager.GM.UnLoadStage) return;
        switch (GameManager.Main.nowDushBuff)
        {
            case (YukinoMain.dushBuffState.step1):
                GameManager.Sound.SetCueName("Drop_1");
                GameManager.Sound.OnSound();
                DropKakera(dropKakera[0]);
                break;
            case (YukinoMain.dushBuffState.step2):
                GameManager.Sound.SetCueName("Drop_2");
                GameManager.Sound.OnSound();
                DropKakera(dropKakera[1]);
                break;
            case (YukinoMain.dushBuffState.step3):
                GameManager.Sound.SetCueName("Drop_3");
                GameManager.Sound.OnSound();
                DropKakera(dropKakera[2]);
                break;
            default:
                break;
        }
    }
    void DropKakera(Transform kakera)
    {
        Transform brokenTransfoem = Instantiate(kakera, transform.position, transform.rotation);
        //var BrokenScale_x = brokenTransfoem.localScale.x * this.transform.localScale.x;
        //var BrokenScale_y = brokenTransfoem.localScale.y * this.transform.localScale.y;
        //var BrokenScale_z = brokenTransfoem.localScale.z * this.transform.localScale.z;
        //var BrokenScale = new Vector3(BrokenScale_x, BrokenScale_y, BrokenScale_z);
        //brokenTransfoem.localScale = BrokenScale;
        brokenTransfoem.position = transform.position;

        foreach (Rigidbody rigidbody in brokenTransfoem.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddExplosionForce(150f, transform.position + Vector3.up * 0.5f, 5f);
        }
        brokenTransfoem.DetachChildren();
        Destroy(brokenTransfoem);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.Main.index == 1)
        {
            //GameManager.Sound.SetCueName("Damage");
            //GameManager.Sound.OnSound();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameManager.Main.index == 1)
        {
            //GameManager.Sound.SetCueName("Enemy_Hit");
            //GameManager.Sound.OnSound();
        }
        if (other.gameObject.tag == "DeathZone") Destroy(this.gameObject);
    }
    public void DeathEnemy()
    {
        if (onDestry) return;
        EnemyDestroyEvent enemyDestroy = null;
        TryGetComponent(out enemyDestroy);
        GameManager.AllEnemy.Remove(enemyDestroy);
        GameManager.EnemyKill();
        onDestry = true;
    }
}
