using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy3
{
    public class Statewait3 : Enemy3StateBase
    {
        public override void OnEnter(Enemy3 owner, Enemy3StateBase prevState)
        {
            if (owner.breaktime == true)
            {
                owner.Startcoroutine();
            }
        }
        public override void OnUpdate(Enemy3 owner)
        {
            if (owner.breaktime == false)
            {
                owner.ChangeState(statedetect);
            }
        }
        public override void OnExit(Enemy3 owner, Enemy3StateBase nextState)
        {

        }
    }
}
