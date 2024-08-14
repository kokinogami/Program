using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy2
{
    public class StateRush : Enemy2StateBase
    {
        Vector3 preposition;
        Vector3 Targetposition;
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {
            owner.rush = true;
            owner.emptyscript.attacktilt();
            preposition = new Vector3(owner.transform.position.x, 0, owner.transform.position.z);
            Targetposition = new Vector3(GameManager.Main.transform.position.x, 0,GameManager.Main.transform.position.z);
            Vector3 rushdir = (Targetposition - preposition);
            owner.rb.velocity =rushdir.normalized * owner.MoveSpeed;
            owner.transform.rotation = Quaternion.LookRotation(rushdir);
            owner.RushEffect.SetActive(true);
        }
        public override void OnUpdate(Enemy2 owner)
        {
            Vector3 Nowposition = new Vector3(owner.transform.position.x, 0, owner.transform.position.z);
            float rushdistance = Vector3.Distance(Nowposition,preposition);
            if(rushdistance > owner.Rushdist)
            {
                owner.rush = false;
                owner.rb.velocity =Vector3.zero;
                owner.longbreaktime = true;
                owner.ChangeState(statewait);
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            owner.RushEffect.SetActive(false);
            owner.set = false;
            owner.look = true;
            owner.elapsedtime = 0;
        }
    }
}
