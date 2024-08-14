using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukinoWalk_L : MonoBehaviour
{
    bool isgraund;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || YukinoMain.currentState == YukinoMain.stateMovie|| isgraund==true) return;
        //Debug.Log("Walk_L"+other.tag);
        GameManager.Sound.SetCueName("Walk_Nomal_L");
        GameManager.Sound.OnSound();
        isgraund = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isgraund = false;
    }
}
