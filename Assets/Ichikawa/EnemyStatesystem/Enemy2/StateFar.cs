using UnityEngine;

public partial class Enemy2
{
    public class StateFar : Enemy2StateBase
    {
        float time;
        float count;
        bool timestop = false;
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {

        }
        public override void OnUpdate(Enemy2 owner)
        {
            owner.Look();
            if (owner.far == false && owner.near == false)
            {
                owner.ChangeState(statewait);
            }
        }
        public override void OnFixedUpdate(Enemy2 owner)
        {
            time += Time.deltaTime;
            if (time <= owner.spherestoptime)
            {
                count += 1;
                if (count % 14 * Time.deltaTime == 0)//14ƒtƒŒ[ƒ€–ˆ‚²‚Æ‚É10ƒ_ƒ[ƒWUŒ‚”­ŽË
                {
                    owner.subscript1.SphereShot();
                    owner.subscript2.SphereShot();
                    owner.subscript3.SphereShot();
                    owner.subscript4.SphereShot();
                }
            }
            else if(time > owner.spherestoptime)
            {
                owner.breaktime = true;
                owner.ChangeState(statewait);
            }
            if (Input.GetKeyDown(KeyCode.Escape))//ŽžŠÔŽ~‚ß‚éŠÖŒW
            {
                if (timestop == false)
                {
                    timestop = true;
                    Time.timeScale = 0;
                }
               else if (timestop == true)
                {
                    timestop = false;
                    Time.timeScale = 1;
                }
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            time = 0;
            owner.elapsedtime = 0f;
        }
    }
}
