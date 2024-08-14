using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Enemy2
{
    public class Statebodyattack : Enemy2StateBase
    {
        CharacterController Controller;
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {
            Controller = owner.GetComponent<CharacterController>();
            owner.emptyscript.attacktilt();
        }
        public override void OnUpdate(Enemy2 owner)
        {
            owner.Detect(); //常に検知モード入ってないと検知外に出た時に攻撃を止めてくれない
            if (Movestop(owner))
            {
                //ユキノの方へ向かせ、移動する（襲う）
                Vector3 direction = GameManager.Main.transform.position - owner.transform.position;
                direction = direction.normalized;
                owner.transform.LookAt(GameManager.Main.transform.position);
                Vector3 velocity = direction * owner.MoveSpeed;
                Controller.Move(velocity * 3.0f * Time.deltaTime);
            }
            if(owner.near == false)
            {
                owner.longbreaktime = true;
                owner.ChangeState(statewait);
            }
            if(GameManager.Main.transform.position.y > owner.transform.position.y)
            {
                owner.breaktime = true;
                owner.ChangeState(statewait);
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            owner.look = true;
        }
       public bool Movestop(Enemy2 owner)
        {
            if (owner.spherescript1.movestop == true) return false;
            if (owner.spherescript2.movestop == true) return false;
            if (owner.spherescript3.movestop == true) return false;
            if (owner.spherescript4.movestop == true) return false;
            if (owner.spherescript5.movestop == true) return false;
            if (owner.spherescript6.movestop == true) return false;
            if (owner.spherescript7.movestop == true) return false;
            if (owner.spherescript8.movestop == true) return false;
            if (owner.subscript1.movestop == true) return false;
            if (owner.subscript2.movestop == true) return false;
            if (owner.subscript3.movestop == true) return false;
            if (owner.subscript4.movestop == true) return false;
            return true;
        }

    }

}