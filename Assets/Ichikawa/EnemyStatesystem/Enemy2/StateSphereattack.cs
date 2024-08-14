using UnityEngine;

public partial class Enemy2
{
    public class StateSphereattack : Enemy2StateBase
    {
        float time;
        float count;
        bool timestop = false;
        public override void OnEnter(Enemy2 owner, Enemy2StateBase prevState)
        {

        }
        public override void OnUpdate(Enemy2 owner)
        {
           
        }
        public override void OnFixedUpdate(Enemy2 owner)
        {
                time += Time.deltaTime;
                if (time <= owner.spherestoptime)
                {
                    count += 1;
                    if (count % 14 * Time.deltaTime == 0)//14フレーム毎ごとに10ダメージ攻撃発射
                    {
                        if (owner.subscript1 != null) owner.subscript1.SphereShot();
                        if (owner.subscript2 != null) owner.subscript2.SphereShot();
                        if (owner.subscript3 != null) owner.subscript3.SphereShot();
                        if (owner.subscript4 != null) owner.subscript4.SphereShot();
                    }
                }
                else if (time > owner.spherestoptime)//攻撃終わるまで待機ステートに移らない方が良いと考えた
                {
                    owner.breaktime = true;
                    owner.ChangeState(statewait);
                }
            if (Input.GetKeyDown(KeyCode.Escape))//ここが時間止める関係の変更点
            {
                if (timestop == false)
                {
                    timestop = true;
                    Time.timeScale = 0;
                }
              else  if (timestop == true)
                {
                    timestop = false;
                    Time.timeScale = 1;
                }
            }
        }
        public override void OnExit(Enemy2 owner, Enemy2StateBase nextState)
        {
            owner.set = false;
            time = 0;
            owner.look = true;
            owner.breaktime = true;
        }
    }

}
