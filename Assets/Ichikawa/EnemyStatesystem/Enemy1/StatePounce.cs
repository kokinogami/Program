using UnityEngine;

public partial class Enemy1
{
    public class StatePounce : Enemy1StateBase
    {
        public Vector3 targetposition;
        public Vector3 Meposition;
        public override void OnEnter(Enemy1 owner, Enemy1StateBase prevState)
        {
            owner.rb.constraints = RigidbodyConstraints.None;
            owner.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;//positionのチェックを外す
            targetposition = new Vector3(owner.target.transform.position.x, 0, owner.target.transform.position.z);
            Meposition = new Vector3(owner.transform.position.x, 0, owner.transform.position.z);
           //飛びつく位置を記憶させる
            Vector3 attackdir = (targetposition - Meposition);
            owner.rb.velocity = attackdir.normalized * owner.PouncePower + new Vector3(0, owner.PounceUpPower, 0);
            owner.preAttack = 1.1f;
        }
        public override void OnUpdate(Enemy1 owner)
        {
            owner.preAttack -= Time.deltaTime;
            if (owner.preAttack > 0.9f && owner.preAttack < 1.0f)
            {
                Instantiate(owner.PreAttackEffect, owner.transform.position + 1.5f * owner.transform.forward, Quaternion.identity);
                owner.preAttack = 0.9f;
            }
            if (owner.preAttack <= 0)
            {
                owner.preAttack = 5f;
            }
            if(owner.transform.position.y > owner.raypos.y + 0.5f)
            { owner.attack = true; }
            if (owner.attack == true)
            {
                if (owner.isGround == true && owner.preAttack >= 1.1f)
                {
                    owner.rb.velocity = Vector3.zero;//止まる
                    owner.attack = false;
                }
            }
            if (owner.attack == false)
            {
                owner.breaktime = true;
                owner.ChangeState(statewait);
            } 
        }
        public override void OnExit(Enemy1 owner, Enemy1StateBase nextState)
        {
            owner.look = true;
            owner.set = false;
        }
    }
}
