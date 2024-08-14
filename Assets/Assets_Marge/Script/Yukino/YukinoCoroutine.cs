using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoMain
{
    float coroutinCount;
    IEnumerator ChangeDushBuff()
    {
        stateRunning.ChangeBuff(dushBuffState.step1);

        coroutinCount = dushBuffTime1;
        while (coroutinCount > 0) //1ïbí‚é~
        {
            if (Time.timeScale != 0)
            {
                coroutinCount -= Time.unscaledDeltaTime;
            }

            yield return null;
        }


        //yield return new WaitForSecondsRealtime(dushBuffTime1);              //1ïbí‚é~
        Debug.Log(dushBuffTime1 + "s");

        stateRunning.ChangeBuff(dushBuffState.step2);//ÇQíiäKñ⁄Ç…à⁄çs

        coroutinCount = dushBuffTime2;
        while (coroutinCount > 0) //1ïbí‚é~
        {
            if (Time.timeScale != 0)
            {
                coroutinCount -= Time.unscaledDeltaTime;
            }

            yield return null;
        }

        //yield return new WaitForSecondsRealtime(dushBuffTime2);              //1ïbí‚é~
        Debug.Log(dushBuffTime1 + dushBuffTime2 + "s");
        stateRunning.ChangeBuff(dushBuffState.step3);//3íiäKñ⁄Ç…à⁄çs

        yield break;
    }
    public void StartCoroutineFunction(string name)
    {
        StartCoroutine(name);
    }
    public void StopCoroutineFunction(string name)
    {
        StopCoroutine(name);
    }
}
