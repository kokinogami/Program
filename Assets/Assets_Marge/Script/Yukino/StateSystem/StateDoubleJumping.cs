using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// 二段ジャンプ状態
    /// </summary>
    public class StateDoubleJumping : PlayerStateBase
    {
        //YukinoMain Main;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("DoubleJumping");
            //Main.mushiObj.SetActive(true);
            Main.mushiCount = 0.6f;
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
            }
            //Sound.SetCueName("Jump_Jump");
            //Sound.OnSound();
        }
        public override void OnUpdate(YukinoMain owner)
        {
            RaycastHit hitG;
            if (Main.Grider && Physics.Raycast(owner.transform.position, Vector3.up, out hitG, 2f) == false)//グライダー
            {
                owner.ChangeState(stateGlider);
            }

            if (Main.Ground)//停止
            {
                owner.ChangeState(stateIdle);
            }

            if (Main.Hipdrop)//ヒップドロップ
            {
                owner.ChangeState(stateHipdrop);
            }
            if (owner.rollingCooltime <= 0 && Main.Dush)//前転
            {
                owner.ChangeState(stateForwardRolling);
            }
            if (owner.enCountJ > 0)//大ジャンプ
            {
                RaycastHit hit;
                if (Physics.Raycast(owner.transform.position, Vector3.up, out hit, 5f))
                {
                    owner.ChangeState(stateEnergyJump);
                }
                else
                {
                    owner.ChangeState(stateEnergyJump);
                }
            }
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            //Main.mushiObj.SetActive(false);
            owner.DoubleJump = false;
        }
    }
}