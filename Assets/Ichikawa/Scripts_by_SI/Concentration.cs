using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concentration : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject concentration; 
    YukinoMain YukinoScript;
    private GameObject Yukino;
    [SerializeField]  float Speed;
    void Start()
    {
        Yukino = GameObject.FindWithTag("Player");
        YukinoScript = Yukino.GetComponent<YukinoMain>();
    }

    // Update is called once per frame
    void Update()
    {
        if(YukinoScript.Aspeed.magnitude > Speed && YukinoScript.index == 1)
        {
            concentration.SetActive(true);
        }
        else
        {
            concentration.SetActive(false);
        }
    }
}
