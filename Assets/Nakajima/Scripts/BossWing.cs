using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWing : MonoBehaviour
{
    public bool isWing;
    public float WingGroundTimer;
    public bool isWingAttack = false;
    public int Damage = 5;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(this.gameObject.name + " " + col.gameObject.name);
        if (LayerMask.LayerToName(col.gameObject.layer) != "Player") { isWing = true; }
        if (isWingAttack && LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            GameManager.Main.OnDamage(Damage, true);
            isWingAttack = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) == "Boss") return;
        if (LayerMask.LayerToName(col.gameObject.layer) != "Player") { isWing = true; }
        WingGroundTimer += Time.deltaTime;
        if (isWingAttack && LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            GameManager.Main.OnDamage(Damage, true);
            isWingAttack = false;
        }
    }
    void OnColliderEnter(Collider col)
    {
        Debug.Log(this.gameObject.name + " " + col.gameObject.name);
        if (LayerMask.LayerToName(col.gameObject.layer) != "Player") { isWing = true; }
        if (isWingAttack && LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            GameManager.Main.OnDamage(Damage, true);
            isWingAttack = false;
        }
    }

    void OnColliderStay(Collider col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) != "Player") { isWing = true; }
        WingGroundTimer += Time.deltaTime;
        if (isWingAttack && LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            GameManager.Main.OnDamage(Damage, true);
            isWingAttack = false;
        }
    }

    public void ResetWing()
    {
        isWing = false;
        WingGroundTimer = 0.0f;
    }
}
