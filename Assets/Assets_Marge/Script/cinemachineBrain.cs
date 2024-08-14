using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachineBrain : MonoBehaviour
{
    private CinemachineBrain Brain;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out Brain);
        //Yukino = GameObject.FindWithTag("Player");
        //script = Yukino.GetComponent<YukinoMain>();
        //StartCoroutine("CameraUp");
    }
    IEnumerator CameraUp()
    {
        while (Time.timeScale != 0)
        {
            Brain.ManualUpdate();
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Main.inChargeBreak && Brain.m_UpdateMethod == CinemachineBrain.UpdateMethod.SmartUpdate)
        {
            Brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.ManualUpdate;
        }
        else if (GameManager.Main.inChargeBreak == false && Brain.m_UpdateMethod == CinemachineBrain.UpdateMethod.ManualUpdate)
        {
            Brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        }
        Brain.ManualUpdate();
    }
    private void LateUpdate()
    {
        //Brain.ManualUpdate();
        //Debug.Log("late");
    }
    private void FixedUpdate()
    {
        //Brain.ManualUpdate();
        //Debug.Log("fix");
    }
}
