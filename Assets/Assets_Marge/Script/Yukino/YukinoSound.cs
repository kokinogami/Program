using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CriWare;

public class YukinoSound : MonoBehaviour
{
    YukinoMain Main;


    // Start is called before the first frame update
    IEnumerator Start()
    {

        while (!CriWareInitializer.IsInitialized())
        {
            yield return null;

        }

        /* プレーヤーの作成 */
        Yukino = new CriAtomExPlayer();
        bgm = new CriAtomExPlayer();
        sendbgm = new CriAtomExPlayer();



    }

    /* プレーヤー */
    private CriAtomExPlayer Yukino;
    private CriAtomExPlayer bgm;
    private CriAtomExPlayer sendbgm;

    private CriAtomExPlayback wind;
    private CriAtomExPlayback bgm1;
    private CriAtomExPlayback Running_Ball;
    private CriAtomExPlayback Running_Tsunagu;
    private CriAtomExPlayback Charge_Brake_Now;
    private CriAtomExPlayback Glider_Now;
    private CriAtomExPlayback Charge_Breake_Now;
    private CriAtomExPlayback Energy_Boost;
    private CriAtomExPlayback Hisora_Voice;
    private CriAtomExPlayback Beam;

    /*  ACB 情報 */
    private CriAtomExAcb acb;

    /* CriAtomExAcb からキュー情報を取得 */
    CriAtomEx.CueInfo cueInfo;

    /* キュー名 */
    private string cueName;

    // Update is called once per frame
    void Update() { }

    /*音の再生*/
    public void OnSound()
    {
        if (GameManager.GM.UnLoadStage) { return; }

        Yukino.SetCue(acb, cueName);

        if (cueName == "Running_Ball")
        {
            Running_Ball = Yukino.Start();
        }
        else if (cueName == "Running_Tsunagu")
        {
            Running_Tsunagu = Yukino.Start();
        }
        else if (cueName == "Charge_Brake_Now")
        {
            Charge_Brake_Now = Yukino.Start();
        }
        else if (cueName == "Glider_Now")
        {
            Glider_Now = Yukino.Start();
        }
        else if (cueName == "Charge_Breake_Now")
        {
            Charge_Breake_Now = Yukino.Start();
        }
        else if (cueName == "Energy_Boost")
        {
            Energy_Boost = Yukino.Start();
        }
        else if (cueName == "Hisora_Voice")
        {
            Hisora_Voice = Yukino.Start();
        }
        else if (cueName == "Beam")
        {
            Beam = Yukino.Start();
        }
        else
        {
            Yukino.Start();
        }

    }

    /*風の音再生*/
    public void WindSound()
    {
        if (GameManager.GM.UnLoadStage) { return; }

        Yukino.SetCue(acb, cueName);
        wind = Yukino.Start();
    }

    /*BGMの再生*/
    public void OnBGM()
    {
        if (GameManager.GM.UnLoadStage) { return; }
        bgm.SetCue(acb, cueName);
        bgm1 = bgm.Start();
    }

    /*音止める*/
    public void StopSound()
    {
        Yukino.SetCue(acb, cueName);
        if (cueName == "Running_Ball")
        {
            Running_Ball.Stop();
        }
        else if (cueName == "Running_Tsunagu")
        {
            Running_Tsunagu.Stop();
        }
        else if (cueName == "Charge_Brake_Now")
        {
            Charge_Brake_Now.Stop();
        }
        else if (cueName == "Glider_Now")
        {
            Glider_Now.Stop();
        }
        else if (cueName == "Charge_Breake_Now")
        {
            Charge_Breake_Now.Stop();
        }
        else if (cueName == "Energy_Boost")
        {
            Energy_Boost.Stop();
        }
        else if (cueName == "Hisora_Voice")
        {
            Hisora_Voice.Stop();
        }
        else
        {
            Yukino.Stop();
        }

    }

    /*BGM止める*/
    public void StopBGM()
    {
        bgm.Stop();
    }

    /*BGM遷移*/
    public void BGM_Block_Next()
    {
        //int block = bgm1.playback.GetCurrentBlockIndex();
        int block = bgm1.GetCurrentBlockIndex();

        if (GameManager.stageState == Stage.Stage1)
        {
            if (GameManager.EnemyCount <= 3)//残り敵4体
            {
                if (block == 1)
                {
                    bgm1.SetNextBlockIndex(2);
                }
                else if (block == 3)//もしint blockが4の時
                {
                    bgm1.SetNextBlockIndex(4);
                }
                else if (block == 5)
                {
                    bgm1.SetNextBlockIndex(6);
                }
                else if (block == 6)
                {
                    bgm1.SetNextBlockIndex(7);
                }
            }
            else if (GameManager.EnemyCount <= 5)//残りの敵が7体以下
            {
                if (block == 1)/*もしint blockが1の時*/
                {
                    bgm1.SetNextBlockIndex(1);
                    /*cueName = "Stage1_BGM_2";
                    sendbgm.SetCue(acb, cueName);
                    sendbgm.Start();*/
                }
                else if (block == 3)//もしint blockが3の時
                {
                    bgm1.SetNextBlockIndex(4);
                    /*cueName = "Stage1_BGM_3";
                    sendbgm.SetCue(acb, cueName);
                    sendbgm.Start();*/
                }
                else if (block == 5)
                {
                    bgm1.SetNextBlockIndex(6);
                }
            }
            else if (GameManager.EnemyCount <= 6)//残り敵8体
            {
                if (block == 1)
                {
                    bgm1.SetNextBlockIndex(2);
                }
                else if (block == 3)
                {
                    bgm1.SetNextBlockIndex(4);
                }
            }
            else if (GameManager.EnemyCount <= 8)//残りの敵が10体以下
            {
                if (block == 1)//もしint blockが1の時
                {
                    bgm1.SetNextBlockIndex(2);
                    /*cueName = "Stage1_BGM_2";
                    sendbgm.SetCue(acb, cueName);
                    sendbgm.Start();*/
                }
            }
        }
        //以下ステージ2
        else if (GameManager.stageState == Stage.Stage2)
        {
            if (GameManager.EnemyCount <= 2)//残り敵2体
            {
                if (block == 1)
                {
                    bgm1.SetNextBlockIndex(2);
                }
                else if (block == 3)//もしint blockが3の時
                {
                    bgm1.SetNextBlockIndex(4);
                }
                else if (block == 4)
                {
                    bgm1.SetNextBlockIndex(5);
                }
                else if (block == 6)
                {
                    bgm1.SetNextBlockIndex(7);
                }
            }
            else if (GameManager.EnemyCount <= 4)//残りの敵が4体以下
            {
                if (block == 1)/*もしint blockが1の時*/
                {
                    bgm1.SetNextBlockIndex(2);
                }
                else if (block == 3)//もしint blockが4の時
                {
                    bgm1.SetNextBlockIndex(4);
                }
                else if (block == 4)
                {
                    bgm1.SetNextBlockIndex(5);
                }
            }
            else if (GameManager.EnemyCount <= 6)//残り敵6体
            {
                if (block == 1)
                {
                    bgm1.SetNextBlockIndex(2);
                }
                else if (block == 3)
                {
                    bgm1.SetNextBlockIndex(4);
                }
            }
            else if (GameManager.EnemyCount <= 9)//残りの敵が9体以下
            {
                if (block == 1)//もしint blockが1の時
                {
                    bgm1.SetNextBlockIndex(2);
                }
            }
        }

        /*ボス戦遷移*/
        else if (GameManager.stageState == Stage.MainStage && GameManager.GM.BossBattle == true)
        {
            if (GameManager.GM.boss.CheckPhaseNumber == 2)/*フェーズが2の時*/
            {
                if (block == 7)//7→9
                {
                    bgm1.SetNextBlockIndex(9);
                }
                else if (block == 1)//1→22
                {
                    bgm1.SetNextBlockIndex(22);
                }
                else if (block == 3)//3→23
                {
                    bgm1.SetNextBlockIndex(23);
                }
                else if (block == 5)//5→24
                {
                    bgm1.SetNextBlockIndex(24);
                }
            }
            else if (GameManager.GM.boss.CheckPhaseNumber == 3)/*フェーズが3の時*/
            {
                if (block == 12)//12→16
                {
                    bgm1.SetNextBlockIndex(16);
                }
                else if (block == 15)//15→19
                {
                    bgm1.SetNextBlockIndex(19);
                }
                else if (block == 11)//11→20
                {
                    bgm1.SetNextBlockIndex(20);
                }
                else if (block == 14)//14→21
                {
                    bgm1.SetNextBlockIndex(21);
                }
            }
        }
    }


    /*キューにセットする*/
    public void SetCueName(string name)
    {
        cueName = name;
    }
    public void SetAcb(CriAtomExAcb acb)
    {
        /* (14) ACB の保存 */
        this.acb = acb;
    }

    /*風の音のブロックを遷移させる*/
    public void WindNextBlock()
    {
        Yukino.SetCue(acb, cueName);
        wind.SetNextBlockIndex(2);
    }

    /*ボスのビームのブロックを遷移させる*/
    public void BeamNextBlock()
    {
        Yukino.SetCue(acb, cueName);
        Beam.SetNextBlockIndex(2);
    }

    public void Next()
    {
        int cur = this.wind.GetCurrentBlockIndex();
        wind.SetNextBlockIndex(cur % this.cueInfo.numBlocks);
    }

    //チャージブレーキ時とジャンプ台でジャンプ時
    public void Bass_Cut()
    {
        bgm.SetCue(acb, cueName);
        bgm.Start();
    }

    /*音全部止める*/
    public void AllSoundStop()
    {
        Yukino.Stop();
        bgm.Stop();
        sendbgm.Stop();
    }
}
