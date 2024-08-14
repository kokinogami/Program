using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// �W�����v���
    /// </summary>
    public class StateJumping : PlayerStateBase
    {
        //YukinoMain Main;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Jumping");
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
            }

            Main.yukinoanime.JumpBool(true);
            Sound.SetCueName("Jump");
            Sound.OnSound();
        }
        public override void OnUpdate(YukinoMain owner)
        {
            RaycastHit hitG;
            if (Main.Grider && Physics.Raycast(owner.transform.position, Vector3.up, out hitG, 2f) == false)//�O���C�_�[
            {
                owner.ChangeState(stateGlider);
            }
            if (Main.DoubleJump == true)//��i�W�����v
            {
                owner.ChangeState(stateDoubleJumping);
            }
            if (Main.Ground)//��~
            {
                owner.ChangeState(stateIdle);
            }
            if (Main.Hipdrop)//�q�b�v�h���b�v
            {
                owner.ChangeState(stateHipdrop);
            }
            if (owner.rollingCooltime <= 0 && Main.Dush)//�O�]
            {
                owner.ChangeState(stateForwardRolling);
            }
            if (owner.enCountJ > 0)//��W�����v
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
            Main.yukinoanime.JumpBool(false);
            Main.Jump = false;
        }
    }
}