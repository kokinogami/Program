using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoMain : MonoBehaviour
{
    void EventUpdate()
    {
        if(DebugMode&&Input.GetKey(KeyCode.Y)&& Input.GetKeyDown(KeyCode.B))
        {
            GetRefrigerator();
        }
    }
    void GetRefrigerator()
    {
        carriedRefrigerator.SetActive(true);
    }
}
