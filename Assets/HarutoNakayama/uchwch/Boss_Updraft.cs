using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Updraft : MonoBehaviour
{
    [SerializeField] private GameObject UpdraftA;
    [SerializeField] private GameObject UpdraftA2;
    [SerializeField] private GameObject UpdraftA3;
    [SerializeField] private GameObject UpdraftB;
    [SerializeField] private GameObject UpdraftB2;
    [SerializeField] private GameObject UpdraftB3;
    [SerializeField] private GameObject UpdraftC;
    [SerializeField] private GameObject UpdraftC2;
    [SerializeField] private GameObject UpdraftC3;

    // Start is called before the first frame update
    void Start()
    {
        UpdraftA.SetActive(false);
        UpdraftA2.SetActive(false);
        UpdraftA3.SetActive(false);
        UpdraftB.SetActive(false);
        UpdraftB2.SetActive(false);
        UpdraftB3.SetActive(false);
        UpdraftC.SetActive(false);
        UpdraftC2.SetActive(false);
        UpdraftC3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("1st Step");
            UpdraftA.SetActive(true);
            UpdraftB.SetActive(true);
            UpdraftC.SetActive(true);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("2nd Step");
            UpdraftA.SetActive(false);
            UpdraftB.SetActive(false);
            UpdraftC.SetActive(false);
            UpdraftA2.SetActive(true);
            UpdraftB2.SetActive(true);
            UpdraftC2.SetActive(true);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("3rd Step");
            UpdraftA2.SetActive(false);
            UpdraftB2.SetActive(false);
            UpdraftC2.SetActive(false);
            UpdraftA3.SetActive(true);
            UpdraftB3.SetActive(true);
            UpdraftC3.SetActive(true);
        }
     }
}
