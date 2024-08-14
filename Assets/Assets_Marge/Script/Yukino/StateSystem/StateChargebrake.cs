using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// チャージブレーキ状態
    /// </summary>
    public class StateChargebrake : PlayerStateBase
    {
        //YukinoMain Main;
        GameObject Effect;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Main.ChargebreakOffscreen.SetActive(true);
            Debug.Log("Chargebrake");
            owner.rb.velocity = new Vector3(0.0f, owner.rb.velocity.y, 0.0f);
            Instantiate(owner.ChaergeBreakEf, owner.gameObject.transform.position, Quaternion.identity);
            Time.timeScale = owner.bulletTimeSpeed;
            ChangeChargebrake();
            Main.CameraCs.ChangeChargeBreakCamera();
            ChargeBreakStart();
            Main.chargeBreakCs.gameObject.SetActive(true);
            Main.cameraData.SetRenderer(1);
            Main.Monochrome.SetActive(true);
            Effect = (GameObject)Instantiate(Main.SnowballLoopEf, Main.gameObject.transform.position + Vector3.down * 0.4f, Quaternion.identity);
            Effect.transform.parent = Main.gameObject.transform;
            Sound.SetCueName("Running_Tsunagu");
            Sound.StopSound();
            Main.tunaguSound = false;
            Sound.SetCueName("Charge_Brake_Start");
            Sound.OnSound();
            Sound.SetCueName("Carge_Brake_Cat_Start");
            Sound.Bass_Cut();
            Sound.SetCueName("Charge_Brake_Now");
            Sound.OnSound();
            owner.StartCoroutineFunction("ChangeDushBuff");
            owner.DushCharge = false;
            owner.yukinoanime.Chargebool(true);
            owner.rollingCooltime = 0f;
        }
        public override void OnUpdate(YukinoMain owner)
        {
            //Sound.SetCueName("Carge_Brake_Cat_Start");
            //Sound.OnSound();
            if (Main.inChargeBreak == false && Main.Dush)//走る
            {
                Main.transform.rotation = Main.chargeBreakCs.transform.rotation;
                owner.ChangeState(stateRunning);
            }
            else if(owner.DushCharge){
                Main.transform.rotation = Main.chargeBreakCs.transform.rotation;
                owner.inChargeBreak = false;
                owner.ChangeState(stateRunning);
            }
            else if (Main.inChargeBreak == false)
            {
                if (Main.Dush == false && Main.Move.magnitude > 0)
                {
                    owner.ChangeState(stateWalking);
                    Main.CameraCs.ChaneYukinoCamera();
                }
                else
                {
                    owner.ChangeState(stateIdle);
                    Main.CameraCs.ChaneYukinoCamera();
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
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            owner.DushCharge = false;
            Main.childObject[6].SetActive(false);
            Time.timeScale = 1;
            Main.DushCount = 0;
            Main.cameraData.SetRenderer(default);
            Main.Monochrome.SetActive(false);
            Main.ChargebreakOffscreen.SetActive(false);
            Destroy(Effect);
            Sound.SetCueName("Charge_Brake_Now");
            Sound.StopSound();
            Sound.SetCueName("Charge_Brake_End");
            Sound.OnSound();
            Sound.SetCueName("Carge_Brake_Cat_End");
            Sound.Bass_Cut();
            if (nextState != stateRunning)
            {
                Main.StopCoroutineFunction("ChangeDushBuff");
                Main.roll2Ef.SetActive(false);
                Main.roll3Ef.SetActive(false);
            }
            owner.yukinoanime.Chargebool(false);
        }
        void ChargeBreakStart()
        {
            float Value = Main.CameraCs.cameraValue;
            Main.CameraCs.cameraValue = 0.0f;
            Main.chargeBreakCs.gameObject.transform.eulerAngles = new Vector3(0.0f, Value, 0.0f);
        }
    }
}