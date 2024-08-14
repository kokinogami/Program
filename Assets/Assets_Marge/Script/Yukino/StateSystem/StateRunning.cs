using UnityEngine;
using static CriWare.CriAtomExMic;

public partial class YukinoMain
{
    /// <summary>
    /// 走り状態
    /// </summary>
    public class StateRunning : PlayerStateBase
    {
        //YukinoMain Main;
        GameObject Effect;

        Vector3 step1Buffsize = new Vector3(1.25f, 0.6f, 0.7f);
        Vector3 step2Buffsize = new Vector3(1.8f, 0.6f, 0.7f);
        Vector3 step3Buffsize = new Vector3(2.35f, 0.6f, 0.7f);

        bool OnRunSound = false;

        [System.NonSerialized] public float buffGrantCount = 0;



        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Running");
            if (Main.index == 0)
            {
                ChangeIceBall();
            }
            Main.LimitSpeed = Main.IceBallSpeed;

            Sound.SetCueName("Running_Ball");
            Sound.OnSound();
            OnRunSound = true;

            if (prevState == stateEnergyDush) return;
            Main.YukidamaAnimation.MoveAnimaStr();
            Effect = (GameObject)Instantiate(Main.RunStartEf, Main.gameObject.transform.position + Vector3.up * 0.3f, Main.transform.rotation);
            Effect.transform.parent = Main.gameObject.transform;
            Main.CameraCs.ShakeCamera(YukinoMain.ShakeCameraState.startRun);
            Main.rolleffect.SetActive(true);
            owner.iceconnectSoundCoolTime = 0;
            owner.rollingCooltime = 0f;
            if (prevState != stateChargebrake) owner.StartCoroutineFunction("ChangeDushBuff"); //ダッシュ継続バフコルーチンを起動　コルーチンはYukinoCoroutine内に格納
        }
        public override void OnUpdate(YukinoMain owner)
        {
            if (OnRunSound && Time.timeScale == 0)
            {
                RunSoundStop();
            }
            else if(OnRunSound==false && Time.timeScale != 0)
            {
                Sound.SetCueName("Running_Ball");
                Sound.OnSound();
                OnRunSound = true;
            }

            RaycastHit hit;
            if (Physics.SphereCast(owner.transform.position, 0.9f, owner.transform.forward, out hit, 0.3f, 8192))
            {
                IceBreak iceBreak;
                hit.collider.TryGetComponent(out iceBreak);
                iceBreak.BrokenBridge();
            }

            if (owner.JumpPadObj > 0) return;
            if (Main.Dush == false) owner.ChangeState(stateWalking);//歩き
            if (Main.Hipdrop) owner.ChangeState(stateHipdrop);//ヒップドロップ
            //if (Main.Move.sqrMagnitude <= 0) owner.ChangeState(stateIdle);//停止
            if (Main.inChargeBreak) owner.ChangeState(stateChargebrake);//チャージブレーキ
            if (Main.Jump) owner.ChangeState(stateJumping);//ジャンプ
            if (owner.enCount > 0) owner.ChangeState(stateEnergyDush);//ダッシュ
        }
        public override void OnFixedUpdate(YukinoMain owner)
        {
            if (owner.Ground && owner.JumpPadObj <= 0) Move(owner);
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            RunSoundStop();
            if (nextState == stateEnergyDush) return;
            Main.YukidamaAnimation.MoveAnimafin();
            Main.rolleffect.SetActive(false);
            Main.StopCoroutineFunction("ChangeDushBuff");
            Main.roll2Ef.SetActive(false);
            Main.roll3Ef.SetActive(false);
            Main.BuffEf2.SetActive(false);
            Main.BuffEf3.SetActive(false);
            owner.nowDushBuff = dushBuffState.step1;
        }
        void Move(YukinoMain owner)
        {
            //速度と方向の計算＆変更
            var velocityVector = owner.transform.forward * owner.LimitSpeed;// * owner.Move.normalized.magnitude;
            velocityVector = Vector3.ProjectOnPlane(velocityVector, owner.planeVector);
            owner.rb.velocity = velocityVector;// + new Vector3(0.0f, owner.rb.velocity.y, 0.0f);

            //入力から回転方向取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            // 方向キーの入力値とカメラの向きから、移動方向を決定
            Vector2 move = owner.Move;
            if (move == Vector2.zero)
            {
                move = Vector2.up;
            }
            Vector3 moveForward = cameraForward * move.y + Camera.main.transform.right * move.x;
            var A = Quaternion.LookRotation(moveForward);
            if (owner.Ground)
            {
                var Rospeed = 10 * Time.deltaTime;
                owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, A, Rospeed);
            }
        }
        void Freez()//バグ修正用
        {
            RaycastHit hitA;
            if (Physics.Raycast(Main.transform.position, Vector3.down, out hitA, 5.1f))
            {
                Main.transform.position = new Vector3(Main.transform.position.x, hitA.point.y + 0.9526401f, Main.transform.position.z);
            }
            if (Main.rb.velocity.y > 0.0f)
            {
                {
                    Main.rb.velocity = new Vector3(Main.rb.velocity.x, 0.0f, Main.rb.velocity.z);
                }
            }
        }
        public void RunSoundStop()
        {
            Sound.SetCueName("Running_Ball");
            Sound.StopSound();
            OnRunSound = false;
        }

        public void ChangeBuff(dushBuffState changeBuff)//継続時間によって当たり判定の大きさを変更
        {
            switch (changeBuff)
            {
                case (dushBuffState.step1)://0～1s
                    Main.yukidamaBoxCollider.size = step1Buffsize;
                    Main.roll2Ef.SetActive(false);
                    Main.roll3Ef.SetActive(false);
                    Main.BuffEf2.SetActive(false);
                    Main.BuffEf3.SetActive(false);
                    break;
                case (dushBuffState.step2)://1～2s
                    Sound.SetCueName("Fase_Up_1");
                    Sound.OnSound();
                    Sound.SetCueName("Running_Fase2");
                    Sound.OnSound();

                    Main.yukidamaBoxCollider.size = step2Buffsize;

                    Main.roll2Ef.SetActive(true);
                    Main.BuffEf2.SetActive(true);
                    Main.BuffEf3.SetActive(false);
                    break;
                case (dushBuffState.step3)://2～3s
                    Sound.SetCueName("Fase_Up_2");
                    Sound.OnSound();
                    Sound.SetCueName("Running_Fase3");
                    Sound.OnSound();

                    Main.yukidamaBoxCollider.size = step3Buffsize;

                    Main.roll2Ef.SetActive(false);
                    Main.roll3Ef.SetActive(true);
                    Main.BuffEf2.SetActive(false);
                    Main.BuffEf3.SetActive(true);
                    break;
            }
            GameManager.Main.nowDushBuff = changeBuff;
        }
    }
}
