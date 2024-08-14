using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// ロード状態
    /// </summary>
    public class StateLoad : PlayerStateBase
    {
        //YukinoMain Main;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Loading");
            //if (prevState == null) return;
            //Main.rb.useGravity = true;
        }
        public override void OnUpdate(YukinoMain owner)
        {
            if (owner.OnWhiteFade == false && GameManager.startBGM ==false)
            {
                owner.ChangeState(stateIdle);
            }
            /*if (Main.fadeIn.alfa <= 0.0f&&)//歩き
            {
                owner.ChangeState(stateIdle);
            }*/
        }
        public override void OnLateUpdate(YukinoMain owner)
        {
            if (Main.Ground && GameManager.startBGM)
            {
                owner.ChangeState(stateIdle);
            }
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            //Main.rb.useGravity = false;
        }
    }
}
