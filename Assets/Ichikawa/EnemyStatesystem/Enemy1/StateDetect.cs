using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy1
{
    public class StateDetect : Enemy1StateBase
    {
        public override void OnEnter(Enemy1 owner, Enemy1StateBase prevState)
        {
            owner.elapsedtime = 0;
        }
        public override void OnUpdate(Enemy1 owner)
        {
            owner.Detect();
            if (!owner.look & owner.InArea == true)
            {
                owner.Look();
            }
                if (owner.InArea == true && !owner.knockback)
            {
                owner.Shortcoroutine();
                if (owner.set == true & owner.elapsedtime >= owner.RotateSpeed)
                {
                    owner.ChangeState(statepounce);
                }
            }
        }
        public override void OnExit(Enemy1 owner, Enemy1StateBase nextState)
        {
            owner.rb.isKinematic = false;
            owner.elapsedtime = 0;
        }
    }
}