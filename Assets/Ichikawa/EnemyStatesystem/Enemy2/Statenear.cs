using UnityEngine;
public partial class Enemy2
{
    public class Statenear : Enemy2StateBase
    {
        private int randomAt;
        Vector3 prepos;
        Vector3 backpos;
        float passtime = 0f;
        float Rate;
        float movetime = 1.5f;
        private bool one;
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {
            randomAt = Random.Range(1, 11);
            owner.emptyscript.horizon();
            one = true;
        }
        public override void OnUpdate(Enemy2 owner)
        {
            if(owner.backArea )
            { owner.Look(); }
            if(randomAt <= 3)
            { owner.matchheight(); }
            if (randomAt > 3)
            { owner.matchheight(); }
            if (owner.set)
            {
                if (one)//å„ÇÎÇ…â∫Ç™ÇÈèÄîı
                {
                    prepos = owner.transform.position;
                    backpos = owner.transform.position - owner.transform.forward * 5f;
                    one = false;
                }
            }
            if (randomAt <= 3 & owner.set == true )
            {
                owner.ChangeState(statesphereattack);
            }
            if(randomAt > 3 & owner.set == true )
            {
                passtime += Time.deltaTime;
                Rate = Mathf.Clamp01(passtime / movetime);
                owner.transform.position = Vector3.Lerp(prepos, backpos, Rate);//ó\íõÇ≈àÍìxè≠Çµå„ÇÎÇ…â∫Ç™Ç¡ÇƒÇ©ÇÁìÀêi
                if (passtime >= movetime)
                {
                    owner.ChangeState(staterush);
                }
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            passtime = 0f;
            one = true;
        }
    }

}
