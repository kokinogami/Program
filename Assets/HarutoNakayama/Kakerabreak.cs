using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kakerabreak : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform BunriKakera;
    [SerializeField] GameObject breakEffecr;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (BrokenRun())
            {
                KakeraBreak();
                return;
            }
        }
    }

    void KakeraBreak()
    {
        {
            Transform brokenTransfoem = Instantiate(BunriKakera, transform.position, transform.rotation);
            var BrokenScale_x = brokenTransfoem.localScale.x * this.transform.localScale.x;
            var BrokenScale_y = brokenTransfoem.localScale.y * this.transform.localScale.y;
            var BrokenScale_z = brokenTransfoem.localScale.z * this.transform.localScale.z;
            var BrokenScale = new Vector3(BrokenScale_x, BrokenScale_y, BrokenScale_z);
            brokenTransfoem.localScale = BrokenScale;
            brokenTransfoem.position = transform.position;

            foreach (Rigidbody rigidbody in brokenTransfoem.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.AddExplosionForce(150f, transform.position + Vector3.up * 0.5f, 5f);
            }
            Instantiate(breakEffecr, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    bool BrokenRun()
    {
        if (GameManager.Main.Dush == false) return false;
        if (GameManager.Main.DushCount > 0.0f) return false;
        //if (GameManager.Main.Move.magnitude <= 0) return false;
        return true;
    }

}
