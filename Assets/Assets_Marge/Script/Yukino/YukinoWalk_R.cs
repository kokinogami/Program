using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukinoWalk_R : MonoBehaviour
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
        if (other.tag == "Player" || YukinoMain.currentState == YukinoMain.stateMovie) return;
        //Debug.Log("Walk_R" + other.tag);
        GameManager.Sound.SetCueName("Walk_Nomal_R");
        GameManager.Sound.OnSound();
        isgraund = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isgraund = false;
    }
}
