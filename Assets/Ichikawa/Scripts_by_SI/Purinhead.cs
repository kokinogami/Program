using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purinhead : MonoBehaviour
{
    public PurinEnemy Script;
    public Material detectcolor;
    YukinoMain YukinoScript;
    private GameObject Yukino;
    // Start is called before the first frame update
    void Start()
    {
        Yukino = GameObject.FindWithTag("Player");
        YukinoScript = Yukino.GetComponent<YukinoMain>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Script.InArea == true)
        {
            GetComponent<Renderer>().material.color = detectcolor.color;
        }
        if (Script.InArea == false)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            if(YukinoScript.rb.velocity.y < -15 && YukinoScript.index == 0)//※ヒップドロップ時雪玉なれないため、慣れるようになったらindex=1
            {
                GameObject Boss = transform.parent.gameObject;
                Destroy(Boss);
                Debug.Log("a");
            }
        }
    }
}
