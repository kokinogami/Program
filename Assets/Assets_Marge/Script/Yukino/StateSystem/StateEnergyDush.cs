using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoMain : MonoBehaviour
{
    public class StateEnergyDush : PlayerStateBase
    {
        bool OnEnergyBoostSound = false;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("EnergyDush");
            owner.EnergyDushEf.SetActive(true);
            owner.LimitSpeed = owner.EnergyDushSpeed;
            Sound.SetCueName("Energy_Boost");
            Sound.OnSound();
            OnEnergyBoostSound = true;
        }
        public override void OnUpdate(YukinoMain owner)
        {
            owner.enCount -= Time.deltaTime;

            if (OnEnergyBoostSound && Time.timeScale == 0)
            {
                Sound.SetCueName("Energy_Boost");
                Sound.StopSound();
                OnEnergyBoostSound = false;
            }
            else if (OnEnergyBoostSound == false && Time.timeScale != 0)
            {
                Sound.SetCueName("Energy_Boost");
                Sound.OnSound();
                OnEnergyBoostSound = true;
            }

            RaycastHit hit;
            if (Physics.SphereCast(owner.transform.position, 0.8f, owner.transform.forward, out hit, 0.1f, 8192))
            {
                IceBreak iceBreak;
                hit.collider.TryGetComponent(out iceBreak);
                iceBreak.BrokenBridge();
            }

            if (owner.enCount <= 0 || owner.Dush == false) owner.ChangeState(stateRunning);//�_�b�V��
            if (Main.inChargeBreak) owner.ChangeState(stateRunning);//�`���[�W�u���[�L
        }
        public override void OnFixedUpdate(YukinoMain owner)
        {
            if (owner.Ground && owner.JumpPadObj <= 0) Move(owner);
        }
        public override void OnLateUpdate(YukinoMain owner)
        {

        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            owner.enCount = 0;
            owner.EnergyDushEf.SetActive(false);
            Sound.SetCueName("Energy_Boost");
            Sound.StopSound();
            OnEnergyBoostSound = false;
        }
        void Move(YukinoMain owner)
        {
            //���x�ƕ����̌v�Z���ύX
            var velocityVector = owner.transform.forward * owner.LimitSpeed;// * owner.Move.normalized.magnitude;
            velocityVector = Vector3.ProjectOnPlane(velocityVector, owner.planeVector);
            owner.rb.velocity = velocityVector;// + new Vector3(0.0f, owner.rb.velocity.y, 0.0f);

            //���͂����]�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
            Vector3 moveForward = cameraForward * owner.Move.y + Camera.main.transform.right * owner.Move.x;
            var A = Quaternion.LookRotation(moveForward);
            if (owner.Ground)
            {
                var Rospeed = 10 * Time.deltaTime;
                owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, A, Rospeed);
            }
        }
    }
}
