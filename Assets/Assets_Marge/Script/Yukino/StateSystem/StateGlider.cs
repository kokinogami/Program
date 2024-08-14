using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// グライダー状態
    /// </summary>
    public class StateGlider : PlayerStateBase
    {
        //YukinoMain Main;
        public Vector2 currentVelocity = Vector2.zero;
        Vector2 YukinoPos;
        Vector2 GriderPos;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Glider");
            changeGliderParameter();
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
            }
            Sound.SetCueName("Glider_Start");
            Sound.OnSound();
            Sound.SetCueName("Glider_Now");
            Sound.OnSound();
            HisoraNavi.hisoraNavi.SetAnime(true);
            owner.yukinoanime.Griderbool(true);
        }

        public override void OnUpdate(YukinoMain owner)
        {
            GriderMarkActive(owner);
            if (Main.Grider == false)//停止
            {
                owner.ChangeState(stateIdle);
            }
        }
        public override void OnFixedUpdate(YukinoMain owner)
        {
            HisoraMove(owner);
            YukinoMove(owner);
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            owner.GriderMark.SetActive(false);
            Yukinocurrentstate = YukinoState.Idle;
            //Main.rb.useGravity = true;
            Main.Hisorarb.useGravity = false;
            Main.rb.velocity = new Vector3(0, Main.Hisorarb.velocity.y, 0);
            Sound.SetCueName("Glider_Now");
            Sound.StopSound();
            Sound.SetCueName("Glider_End");
            Sound.OnSound();
            HisoraNavi.hisoraNavi.SetAnime(false);
            owner.yukinoanime.Griderbool(false);
        }
        public override void OnLateUpdate(YukinoMain owner)
        {

        }
        void HisoraMove(YukinoMain owner)
        {
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            owner.Hisorarb.velocity = horizontalRotation.normalized * new Vector3(owner.Move.x, 0, owner.Move.y) * owner.LimitSpeed + new Vector3(0.0f, owner.Hisorarb.velocity.y, 0.0f);
        }
        void YukinoMove(YukinoMain owner)
        {
            if (Main.Move != Vector2.zero)
            {
                var Rospeed = 500 * Time.deltaTime;
                var velocity = new Vector3(Main.Hisorarb.velocity.x, 0, Main.Hisorarb.velocity.z);
                Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
                Main.Hisorarb.rotation = Quaternion.RotateTowards(Main.Hisora.transform.rotation, targetRotation/* * GriderRotate*/, Rospeed);
            }
            Main.transform.eulerAngles = Main.Hisora.transform.eulerAngles;
            float yukino_pos_x = owner.rb.position.x;
            float yukino_pos_z = owner.rb.position.z;
            YukinoPos = new Vector2(yukino_pos_x, yukino_pos_z);
            GriderPos = new Vector2(Main.Hisorarb.position.x, Main.Hisorarb.position.z);
            YukinoPos = Vector2.SmoothDamp(YukinoPos, GriderPos, ref currentVelocity, 0.1f);
            //Main.rb.position = new Vector3(A.x, Main.Gliderrb.position.y - 2.5f, A.y);
            Main.rb.velocity = new Vector3(currentVelocity.x, 0, currentVelocity.y);
            Main.rb.position = new Vector3(yukino_pos_x, Main.Hisorarb.position.y - 2.5f, yukino_pos_z);
        }
        void changeGliderParameter()
        {
            Yukinocurrentstate = YukinoState.Glider;
            Main.rb.velocity = Vector3.zero;
            var StartPos = Main.rb.position + new Vector3(0, 2.5f, 0);
            Main.Hisorarb.position = StartPos;
            Main.Hisora.transform.eulerAngles = Main.transform.eulerAngles;
            //Main.rb.useGravity = false;
            Main.Hisorarb.useGravity = true;
            Main.Hisorarb.drag = 4.0f;
        }
        void GriderMarkActive(YukinoMain owner)
        {
            if (Physics.Raycast(Main.transform.position, Vector3.down, out RaycastHit hit, 100))
            {
                Vector3 up = Vector3.up * 0.1f;
                owner.GriderMark.transform.position = hit.point + up;
                //Vector3.Angle(hit.transform.forward, hit.normal);
                Vector3 forward = owner.transform.forward;
                Vector3 rotation = Vector3.ProjectOnPlane(forward, hit.normal);
                rotation = Quaternion.FromToRotation(forward, rotation).eulerAngles;
                owner.GriderMark.transform.rotation = Quaternion.Euler(rotation);
                owner.GriderMark.SetActive(true);
            }
            else
            {
                owner.GriderMark.transform.rotation = Quaternion.Euler(Vector3.zero);
                owner.GriderMark.SetActive(false);
            }
        }
    }
}