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
                    if (count % 14 * Time.deltaTime == 0)//14�t���[�������Ƃ�10�_���[�W�U������
                    {
                        if (owner.subscript1 != null) owner.subscript1.SphereShot();
                        if (owner.subscript2 != null) owner.subscript2.SphereShot();
                        if (owner.subscript3 != null) owner.subscript3.SphereShot();
                        if (owner.subscript4 != null) owner.subscript4.SphereShot();
                    }
                }
                else if (time > owner.spherestoptime)//�U���I���܂őҋ@�X�e�[�g�Ɉڂ�Ȃ������ǂ��ƍl����
                {
                    owner.breaktime = true;
                    owner.ChangeState(statewait);
                }
            if (Input.GetKeyDown(KeyCode.Escape))//���������Ԏ~�߂�֌W�̕ύX�_
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
