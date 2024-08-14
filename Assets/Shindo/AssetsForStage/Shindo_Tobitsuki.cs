using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shindo_Tobitsuki : MonoBehaviour
{
    int HP = 1;
    public float searchAngle = 45f;
    public bool InArea;

    public Transform target;
    private GameObject Yukino;
    YukinoMain script;
    // Start is called before the first frame update
    void Start()
    {
        Yukino = GameObject.FindWithTag("Player");
        script = Yukino.GetComponent<YukinoMain>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*public void attack()
    {
        Debug.Log("çUåÇÅI");
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (script.index == 1)
            {
                HP -= 1;
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}