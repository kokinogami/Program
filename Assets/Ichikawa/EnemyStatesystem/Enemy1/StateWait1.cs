using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy1
{
    public class StateWait1 : Enemy1StateBase
    {
        public override void OnEnter(Enemy1 owner, Enemy1StateBase prevState)
        {
            owner.Waitfreeze();
            if (owner.look)
            {
                owner.rb.isKinematic = true;
            }
            if (owner.breaktime == true)
            {
                owner.Startcoroutine();
            }
        }
    
        public override void OnUpdate(Enemy1 owner)
        {
            if (owner.breaktime == false && owner.look)
            {
                owner.ChangeState(statedetect);
            }
            if(owner.isGround && !owner.look)
            {
                owner.ChangeState(statedetect);
            }
        }
        public override void OnExit(Enemy1 owner, Enemy1StateBase nextState)
        {
            owner.look = false;
        }

    }
}
