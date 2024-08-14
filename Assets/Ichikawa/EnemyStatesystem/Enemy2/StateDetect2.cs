using UnityEngine;
public partial class Enemy2
{
    public class StateDetect2 : Enemy2StateBase
    {
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {
            owner.backtime = 0f;
            owner.waitset = false;
        }
        public override void OnUpdate(Enemy2 owner)
        {
            if(owner.elapsedtime >= owner.RotateSpeed) { owner.holdview = true; }
            owner.Detect();
            if (owner.far == true)
            {
                if (!owner.holdview)//二回目検知で検知外に行くことなくfarのとき、目では追ってるから必要ない
                { owner.Look(); }
                owner.emptyscript.horizon();
                owner.matchheight();
                if (owner.set == true & owner.holdview == true)
                {
                    owner.ChangeState(statesphereattack);
                }
            }
            if (owner.near == true)
            {
                owner.ChangeState(statenear);
            }
            if(owner.backArea == true)
            {
                owner.Startshortcoroutine();
                owner.ChangeState(statenear);
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            owner.set = false;//trueになる瞬間はmatchheightがおわってから
            owner.elapsedTime = 0f;
        }
    }
}
