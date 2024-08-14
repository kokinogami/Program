using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenFloorWall : MonoBehaviour
{
    enum breakConditions
    {
        Run,
        HipDrop
    }
    [SerializeField] breakConditions BreakConditions = breakConditions.HipDrop;
    // Start is called before the first frame update
    [SerializeField] private Transform brokenPrefab;

    // Update is called once per frame
    private void Start()
    {
        if(BreakConditions == breakConditions.HipDrop)
        {
            gameObject.tag = "BrokenFloor";
        }
    }
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (BrokenRun(collision) || BrokenHipDrop(collision))
            {
                BrokenWall();
                return;
            }
        }

    }
    void BrokenWall()
    {
        Transform brokenTransfoem = Instantiate(brokenPrefab, transform.position, transform.rotation);
        var BrokenScale_x = this.transform.localScale.x;
        var BrokenScale_y = this.transform.localScale.y;
        var BrokenScale_z = this.transform.localScale.z;
        var BrokenScale = new Vector3(BrokenScale_x, BrokenScale_y, BrokenScale_z);
        brokenTransfoem.localScale = BrokenScale;
        brokenTransfoem.position = transform.position;

        foreach (Rigidbody rigidbody in brokenTransfoem.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddExplosionForce(150f, transform.position + Vector3.up * 0.5f, 5f);
        }

        Destroy(gameObject);
    }
    bool BrokenRun(Collision collision)
    {
        if (BreakConditions != breakConditions.Run) return false;
        if (GameManager.Main.Dush == false) return false;
        if (GameManager.Main.DushCount > 0.0f) return false;
        if (GameManager.Main.Move.magnitude <= 0) return false;
        return true;
    }
    bool BrokenHipDrop(Collision collision)
    {
        if (BreakConditions != breakConditions.HipDrop) return false;
        if (GameManager.Main.Hipdrop == false) return false;
        return true;
    }
}
