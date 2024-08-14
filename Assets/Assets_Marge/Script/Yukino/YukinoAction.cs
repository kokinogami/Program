using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoMain : MonoBehaviour
{
    public bool tunaguSound = false;
    // Update is called once per frame
    void ActionUpdate()
    {
        if (Time.timeScale == 0 && tunaguSound)
        {
            Sound.SetCueName("Running_Tsunagu");
            Sound.StopSound();
            iceconnectSoundCoolTime = 0;
            tunaguSound = false;
            Debug.Log("Stoptunag");
        }
        else if (iceconnectSoundCoolTime > 0) { iceconnectSoundCoolTime -= Time.deltaTime; }
        else if (tunaguSound)
        {
            Sound.SetCueName("Running_Tsunagu");
            Sound.StopSound();
            iceconnectSoundCoolTime = 0;
            tunaguSound = false;
        }
        ConnectIce();
        Joushoukiryu();
        if (Move.magnitude <= 0 && index == 0 && Ground)
        {
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            //yukinoanime.Walkfin();
        }
    }
    void ConnectIce()//ダッシュ中、空中にいれば氷の床を生成
    {
        /*if (index == 1) //ダッシュ中、空中にいれば氷の床を生成
        {
            if (Physics.Raycast(body.transform.position, Vector3.down, out hit, 5.1f) == false && Ground && HP > 1)
            {
                rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                Vector3 horisonMove = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                Vector3 verticalMove = new Vector3(0.0f, rb.velocity.y, 0.0f);

                Vector3 IcePosition = body.transform.position + bodyHeight + horisonMove.normalized * 2.5f;
                Instantiate(IceL, IcePosition, Quaternion.identity);  //氷の床を生成
                HP -= 1;//設置するとHPが-1される
                HealTimeCount = HealTimeCountBackUp;
            }
        }*/
        if (/*index == 1 && Hipdrop == false*/currentState == stateRunning || currentState == stateEnergyDush) //ダッシュ中、空中にいれば氷の床を生成
        {
            if (connectCoolTime > 0.0f) { }
            else if ((Physics.Raycast(body.transform.position, Vector3.down, out hit, 40.0f) == false || (hit.distance > 5.1f && hit.normal.y > 0.9995f)) && HP > 1)
            {
                planeVector = Vector3.up;
                if (rb.velocity.y >= 0) rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                Vector3 horisonMove = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                if (CameraCs.createConnect == false) CameraCs.createConnect = true;//カメラ変更用

                if (plate == false)
                {
                    int size = Random.Range(1, 4);
                    if (size == 1)
                    {
                        Vector3 IceHeight = new Vector3(0.0f, (float)size * 0.25f + 0.35999f, 0.0f);
                        Vector3 IcePosition = body.transform.position - bodyHeight - IceHeight + horisonMove.normalized * 2.5f;
                        Instantiate(IceS, IcePosition, Quaternion.identity);  //Sサイズの床を生成
                    }
                    if (size == 2)
                    {
                        Vector3 IceHeight = new Vector3(0.0f, (float)size * 0.25f + 0.25666f, 0.0f);
                        Vector3 IcePosition = body.transform.position - bodyHeight - IceHeight + horisonMove.normalized * 2.5f;
                        Instantiate(IceM, IcePosition, Quaternion.identity);  //Mサイズの床を生成
                    }
                    if (size == 3)
                    {
                        Vector3 IceHeight = new Vector3(0.0f, (float)size * 0.25f + 0.15333f, 0.0f);
                        Vector3 IcePosition = body.transform.position - bodyHeight - IceHeight + horisonMove.normalized * 2.5f;
                        Instantiate(IceL, IcePosition, Quaternion.identity);  //Lサイズの床を生成
                    }
                }
                else
                {
                    Vector3 IcePosition = body.transform.position + 0.184f * bodyHeight + horisonMove.normalized * 2.5f;
                    Instantiate(IcePlate, IcePosition, Quaternion.identity);  //Lサイズの床を生成
                }

                GaugeDecrease(ConnectIceGauge);//設置するとHPが消費される
                Ground = true;
                if (iceconnectSoundCoolTime <= 0 && Time.timeScale != 0)
                {
                    Sound.SetCueName("Running_Tsunagu");
                    Sound.OnSound();
                    tunaguSound = true;
                    Debug.Log("Onsound");
                }
                iceconnectSoundCoolTime = SoundCoolTime;
            }
        }

        connectCoolTime -= Time.deltaTime;
    }
    private void Joushoukiryu()
    {
        if (Grider == true)
        {
            if (tenjou == true)
            {
                Hisorarb.AddForce(0.0f, 9.8f, 0.0f);
            }

            else if (joushou == true)
            {
                Hisorarb.velocity = new Vector3(rb.velocity.x, 9.83f * 3.5f, rb.velocity.z);
            }
        }
        /*if (Grider == true)
        {
            if (tenjou == true)
            {
                rb.AddForce(0.0f, 9.8f, 0.0f);
            }

            else if (joushou == true)
            {
                rb.velocity = new Vector3(rb.velocity.x, 9.83f, rb.velocity.z);
            }
        }*/
    }
    /*if (Grider)
    {
        if (joushou)
        {
            //rb.velocity = new Vector3(rb.velocity.x, 9.83f, rb.velocity.z);
            rb.AddForce(0.0f, 50.0f, 0.0f);
        }
        else if (tenjou)
        {
            rb.AddForce(0.0f, -100.0f, 0.0f);
        }
    }*/
    public void setactivHipdrop(bool TF)
    {
        HipDropCollider.gameObject.SetActive(TF);
    }
}
