using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class YukinoMain
{
    /// <summary>
    /// ÉWÉÉÉìÉvèÛë‘
    /// </summary>

    public class StateEnergyJump : PlayerStateBase
    {
        float jumpP = 30;
        bool firing = false;
        Vector3 posY = new Vector3(0, 1.2f, 0);
        GameObject Icefool;
        IceBreak IceBreak;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("EnergyJump");
            if (Main.index == 1)
            {
                ChangeHuman();
                Main.CameraCs.ChaneYukinoCamera();
            }
            owner.rb.velocity = new Vector3(0, jumpP, 0);
            Icefool = Instantiate(owner.IceCatapult, owner.transform.position - posY, Quaternion.identity);
            Icefool.TryGetComponent(out IceBreak);
            firing = false;
            owner.rb.AddForce(0, jumpP, 0);
            GameManager.Sound.SetCueName("Jump_Up");
            GameManager.Sound.OnSound();
            //Main.yukinoanime.JumpBool(true);
            //Sound.SetCueName("Jump");
            //Sound.OnSound();
        }
        public override void OnUpdate(YukinoMain owner)
        {
            owner.enCountJ -= Time.deltaTime;
            if (owner.enCountJ <= 0f)//í‚é~
            {
                owner.ChangeState(stateIdle);
            }
            else if (owner.enCountJ <= 0.1f)
            {
                if (firing == false)
                {
                    owner.rb.velocity = new Vector3(0, 0, 0);
                    owner.rb.AddForce(0, jumpP, 0, ForceMode.Impulse);
                    firing = true;
                }
            }
            else if (owner.enCountJ <= 0.15f)
            {
                owner.rb.velocity = new Vector3(0, jumpP, 0);
            }
            else if (owner.enCountJ <= 0.2f)
            {
                owner.rb.velocity = new Vector3(0, jumpP, 0);
                IceBreak.countUpdate();
                Icefool.transform.position = owner.transform.position - posY;
                //RaycastHit hit;
                //if (Physics.Raycast(owner.transform.position, Vector3.up, out hit, 5f))
                //{
                //    owner.ChangeState(stateIdle);
                //}
            }
        }

        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            owner.enCountJ = 0;
            //owner.rb.velocity = Vector3.zero;
            Icefool = null;
            IceBreak = null;
        }
    }
}
