using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// ëOì]èÛë‘
    /// </summary>
    public class StateForwardRolling : PlayerStateBase
    {
        bool Homing = false;
        //float rollingCount;
        EnemyDestroyEvent targetEnemy;
        //YukinoMain Main;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("ForwardRolling");
            owner.LimitSpeed = owner.ForwardSpeed;
            owner.yukinoanime.Forbool(true);
            GameObject Effect = (GameObject)Instantiate(owner.SnowballStartEf, owner.gameObject.transform.position + Vector3.down * 0.4f, Quaternion.identity);
            Effect.transform.parent = owner.gameObject.transform;
            if (owner.index == 1)
            {
                ChangeHuman();
                owner.CameraCs.ChaneYukinoCamera();
            }
            if (owner.invinciblePlayer) owner.StopCoroutine("invincibleTime");
            owner.StartCoroutine("invincibleTime", 1);
            HomingCheck(owner);
            owner.DushCount = owner.DushCountBackUp;
            //rollingCount = owner.DushCount;// owner.DushCountBackUp;
        }
        public override void OnUpdate(YukinoMain owner)
        {
            if (owner.Dush && owner.DushCount <= 0.0f)//ëñÇÈ
            {
                owner.ChangeState(stateRunning);
            }
            else if (owner.Dush == false && owner.DushCount <= 0.0f && owner.Move.magnitude > 0)//ï‡Ç≠
            {
                owner.ChangeState(stateWalking);
            }
            else if (owner.Dush == false && owner.DushCount <= 0.0f)//í‚é~
            {
                owner.ChangeState(stateIdle);
            }
            else if (owner.Dush == false && owner.DushCount <= 0.0f)//í‚é~
            {
                owner.ChangeState(stateIdle);
            }
        }
        public override void OnFixedUpdate(YukinoMain owner)
        {
            DushMove(owner);
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            Main.yukinoanime.Forbool(false);
            targetEnemy = null;
            Homing = false;
            owner.rollingCooltime = 0.4f;
            owner.DushCount = 0;
        }

        private void DushMove(YukinoMain owner)
        {
            //rollingCount -= 0.02f;
            owner.DushCount -= Time.fixedDeltaTime;
            if (Homing)
            {
                Vector3 yukiknoPos = Main.transform.position;
                Vector3 enemypos = targetEnemy.transform.position;
                Vector3 enemyDirection = (enemypos - yukiknoPos).normalized;

                var Rospeed = 600 * Time.deltaTime;
                var velocity = new Vector3(enemyDirection.x, 0, enemyDirection.z);
                Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);
                owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, targetRotation, Rospeed);
                var velocityVector = owner.transform.forward * owner.LimitSpeed;
                velocityVector = Vector3.ProjectOnPlane(velocityVector, owner.planeVector);
                owner.rb.velocity = velocityVector;
            }
            else
            {
                owner.rb.velocity = new Vector3(owner.rb.velocity.x, 0.0f, owner.rb.velocity.z);
                var velocityVector = owner.transform.forward * owner.LimitSpeed;
                velocityVector = Vector3.ProjectOnPlane(velocityVector, owner.planeVector);
                owner.rb.velocity = velocityVector;
            }
            //owner.rb.AddForce(0, 0, Main.LimitSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        void HomingCheck(YukinoMain owner)
        {

            Vector3 yukiknoPos = Main.transform.position;
            Vector3 yukiknoForward = Main.transform.position;
            float checkDistans = Mathf.Pow(owner.homingDestans, 2);

            for (int i = 0; i < GameManager.AllEnemy.Count; i++)
            {
                if (GameManager.AllEnemy[i] != null)
                {
                    Vector3 enemypos = GameManager.AllEnemy[i].transform.position;
                    Vector3 enemyVector = enemypos - yukiknoPos;
                    float targetDistans = (enemyVector).sqrMagnitude;//ãóó£åvéZ
                    float targetAngle = Vector3.Angle(enemyVector, owner.transform.forward);//2ì_Ç©ÇÁäpìxåvéZ
                    if (checkDistans >= targetDistans && owner.homingAngle >= targetAngle)
                    {
                        checkDistans = targetDistans;
                        targetEnemy = GameManager.AllEnemy[i];
                        Homing = true;
                        owner.homingdis.Add(targetDistans);
                        owner.homingangle.Add(targetAngle);
                    }
                }
            }
        }
    }
}