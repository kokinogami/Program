using UnityEngine;
public partial class Enemy2
{ 
public class StateWait : Enemy2StateBase
    {
        float waittime;
        private bool one;
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {
            one = true;
        }
        public override void OnUpdate(Enemy2 owner)
        {
            if (owner.breaktime == false && owner.longbreaktime == false && !owner.end && owner.waitset )
            {
                owner.ChangeState(statedetect);
            }
            waittime += Time.deltaTime;
            if(waittime >= 3.0)
            {
                owner.breaktime = false;
            }
            if(waittime >= 5.0)
            {
                owner.longbreaktime = false;
            }
            if (!owner.breaktime && !owner.longbreaktime)
            {
                if (one)
                {
                    owner.emptyscript.waittilt();
                    one = false;
                }
                owner.matchwaitheight();
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            owner.look = false;
            waittime = 0f;
            one = true;
        }
    }

}
