using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// ロード状態
    /// </summary>
    public class StateMovie : PlayerStateBase
    {
        //YukinoMain Main;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Movie");
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
            }
            //if (prevState == null) return;
            //Main.rb.useGravity = true;
        }
    }
}
