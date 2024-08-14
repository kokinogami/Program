using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy3
{
    public class StateBound : Enemy3StateBase
    {
        public override void OnEnter(Enemy3 owner, Enemy3StateBase prevState)
        {
            
        }
        public override void OnUpdate(Enemy3 owner)
        {
            if (owner.transform.position.y <= owner.raypos.y + 6.0f & !owner.top)//����6.0�܂ŏ㏸
            {
                owner.transform.position += new Vector3(0f, 9.0f * Time.deltaTime, 0f);
            }
            if (owner.transform.position.y > owner.raypos.y + 6.0f)//top��true�ɂȂ��āArbvelocity�ŗ���������
            {
                owner.top = true;
                owner.rb.isKinematic = false;
                owner.animator.SetBool(owner.stampStr, true);
                owner.animator.SetBool(owner.stopStr, false);
            }
            if(owner.ground == true)//�{�̑��iEnemy3.cs)�Œn�ʂɓ������ground��true�ɂȂ�
            {
                owner.Shockwave();//�n�ʒ��n���ɏՌ��g����
                owner.ChangeState(statewait);
                owner.animator.SetBool(owner.stopStr, true);
                owner.animator.SetBool(owner.stampStr, false);
            }
        }
        public override void OnExit(Enemy3 owner, Enemy3StateBase nextState)
        {
            owner.top = false;
            owner.ground = false;
            owner.breaktime = true;
            owner.backbreaktime = true;
            owner.look = true;
            owner.subscript.elapsedtime = 0;
        }
    }
}

