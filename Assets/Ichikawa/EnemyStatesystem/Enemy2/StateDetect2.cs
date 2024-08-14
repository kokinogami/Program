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
                if (!owner.holdview)//���ڌ��m�Ō��m�O�ɍs�����ƂȂ�far�̂Ƃ��A�ڂł͒ǂ��Ă邩��K�v�Ȃ�
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
            owner.set = false;//true�ɂȂ�u�Ԃ�matchheight��������Ă���
            owner.elapsedTime = 0f;
        }
    }
}
