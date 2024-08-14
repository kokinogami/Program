using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// �������
    /// </summary>
    public class StateWalking : PlayerStateBase
    {
        //YukinoMain Main;

        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Walking");
            Main.LimitSpeed = Main.HumanoidSpeed;
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
            }
            Main.yukinoanime.WalkBool(true);
            Main.BuffEf2.SetActive(false);
            Main.BuffEf3.SetActive(false);
        }
        public override void OnUpdate(YukinoMain owner)
        {
            /*if (owner.Ground && GameManager.gameState == GameState.Nomal && Main.JumpPadObj == false)
            {
                //Move(owner);
                GameManager.Sound.SetCueName("Walk_Nomal_R");
                GameManager.Sound.OnSound();
            }*/
            //Freez();
            if (Main.Move.magnitude <= 0)//��~
            {
                owner.ChangeState(stateIdle);
            }
            if (Main.Jump)//�W�����v
            {
                owner.ChangeState(stateJumping);
            }
            if (owner.rollingCooltime <= 0 && owner.Dush)//�O�]
            {
                owner.ChangeState(stateForwardRolling);
            }
            if (Main.Hipdrop)//�q�b�v�h���b�v
            {
                owner.ChangeState(stateHipdrop);
            }
            RaycastHit hitG;
            if (Main.Grider && Physics.Raycast(owner.transform.position, Vector3.up, out hitG, 2f) == false)//�O���C�_�[
            {
                owner.ChangeState(stateGlider);
            }
            if (Main.DoubleJump)//2�i�W�����v
            {
                owner.ChangeState(stateDoubleJumping);
            }
            if (Main.inChargeBreak)//�`���[�W�u���[�L
            {
                owner.ChangeState(stateChargebrake);
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
        public override void OnFixedUpdate(YukinoMain owner)
        {
            if (Main.JumpPadObj <= 0 && Main.Ground) Move(owner);
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            if (nextState != stateForwardRolling) Main.yukinoanime.WalkBool(false);
        }
        void Move(YukinoMain owner)
        {
            //���x�ƕ����̌v�Z���ύX
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up); //�����v�Z
            var move_Nomal = owner.Move.normalized; //������͐��K��
            var velocityVector = horizontalRotation.normalized * new Vector3(move_Nomal.x, 0, move_Nomal.y) * Main.LimitSpeed; //�@���x�N�g���v�Z//+ new Vector3(0.0f, Main.rb.velocity.y, 0.0f);//owner.transform.forward * owner.LimitSpeed * owner.Move.normalized.magnitude;
            velocityVector = Vector3.ProjectOnPlane(velocityVector, owner.planeVector); //�@���x�N�g�����p�x�v�Z
            Main.rb.velocity = velocityVector;

            //���x�̕�������������v�Z���ύX
            if (Main.Ground)
            {
                var Rospeed = 600 * Time.deltaTime;
                var velocity = new Vector3(velocityVector.x, 0, velocityVector.z);
                Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
                owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, targetRotation, Rospeed);
            }
        }
        void Freez()//�o�O�C���p
        {
            RaycastHit hitA;
            if (Physics.Raycast(Main.transform.position, Vector3.down * 1.02f, out hitA, 5.1f))
            {
                Main.transform.position = new Vector3(Main.transform.position.x, hitA.point.y + 1.02f, Main.transform.position.z);
                if (Main.rb.velocity.y > 0.0f)
                {
                    {
                        Main.rb.velocity += Main.transform.forward * Main.rb.velocity.y;
                        Main.rb.velocity = new Vector3(Main.rb.velocity.x, -10.0f, Main.rb.velocity.z);
                    }
                }
            }
        }
    }
}