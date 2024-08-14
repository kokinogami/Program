using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy3
{
    public class StateDetect3 : Enemy3StateBase
    {
        public override void OnEnter(Enemy3 owner, Enemy3StateBase prevState)
        {

        }
        public override void OnUpdate(Enemy3 owner)
        {
            owner.subscript.Detect();
            if(owner.InArea == true)
            {
                owner.ChangeState(statebound);
            }
            else if (owner.backArea == true)
            {
                owner.backStartDelay();
                if (owner.backbreaktime == false)
                {owner.ChangeState(statebound); }
            }
        }
        public override void OnExit(Enemy3 owner, Enemy3StateBase nextState)
        {

        }
    }
}
