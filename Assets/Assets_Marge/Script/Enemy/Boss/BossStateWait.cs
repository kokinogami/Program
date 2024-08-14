using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossEnemy
{
    public class BossStateWait : BossEnemyStateBase
    {
        float count = 0;
        public override void OnEnter(BossEnemy owner, BossEnemyStateBase prevState)
        {

        }
        public override void OnUpdate(BossEnemy owner)
        {
            if (owner.nowPhase == phase.pahse1)
            {
                if (owner.nowStep == step.step1) { }
                else if (owner.nowStep == step.step2) { }
                else if (owner.nowStep == step.step3) { }
                else if (owner.nowStep == step.step4) { }
            }
            else if (owner.nowPhase == phase.pahse2)
            {
                if (owner.nowStep == step.step1) { }
                else if (owner.nowStep == step.step2) { }
                else if (owner.nowStep == step.step3) { }
                else if (owner.nowStep == step.step4) { }
            }
            else if (owner.nowPhase == phase.pahseFinal)
            {
                if (owner.nowStep == step.step1) { }
                else if (owner.nowStep == step.step2) { }
                else if (owner.nowStep == step.step3) { }
                else if (owner.nowStep == step.step4) { }
            }
        }
        public override void OnExit(BossEnemy owner, BossEnemyStateBase nextState)
        {

        }
    }
}
