using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// 停止状態
    /// </summary>
    public class StateIdle : PlayerStateBase
    {
        //public YukinoMain Main;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Idle");
            Main.LimitSpeed = Main.HumanoidSpeed;
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
                if (Main.rb.velocity.y > 0.0f)
                {
                    Main.rb.velocity = new Vector3(Main.rb.velocity.x, 0, Main.rb.velocity.z);//坂バグ修正
                }
            }
            Main.BuffEf2.SetActive(false);
            Main.BuffEf3.SetActive(false);
        }
        public override void OnUpdate(YukinoMain owner)
        {
            if (Main.Move.magnitude > 0 && Main.Ground)//歩く
            {
                owner.ChangeState(stateWalking);
            }
            if (Main.Jump)//ジャンプ
            {
                owner.ChangeState(stateJumping);
            }
            if (Main.Hipdrop)//ヒップドロップ
            {
                owner.ChangeState(stateHipdrop);
            }
            RaycastHit hitG;
            if (Main.Grider && Physics.Raycast(owner.transform.position, Vector3.up, out hitG, 2f) == false)//グライダー
            {
                owner.ChangeState(stateGlider);
            }
            if (Main.DoubleJump)//2段ジャンプ
            {
                owner.ChangeState(stateDoubleJumping);
            }
            if (owner.rollingCooltime <= 0 && Main.Dush)//前転
            {
                owner.ChangeState(stateForwardRolling);
            }
            if (Main.inChargeBreak)//チャージブレーキ
            {
                owner.ChangeState(stateChargebrake);
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
        public override void OnFixedUpdate(YukinoMain owner)
        {
            if (owner.Ground && owner.JumpPadObj <= 0)
            {
                owner.rb.velocity = Vector3.zero;
            }
        }
    }
}