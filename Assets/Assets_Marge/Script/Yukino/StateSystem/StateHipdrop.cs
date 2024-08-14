using UnityEngine;

public partial class YukinoMain
{
    /// <summary>
    /// ヒップドロップ状態
    /// </summary>
    public class StateHipdrop : PlayerStateBase
    {
        //YukinoMain Main;
        GameObject Effect;
        RaycastHit hit;
        float dropStartPos;
        float dropEndPos;
        float CTime;
        public override void OnEnter(YukinoMain owner, PlayerStateBase prevState)
        {
            Debug.Log("Hipdrop");
            if (Main.index == 0)
            {
                ChangeIceBall();
            }
            Physics.Raycast(Main.transform.position, Vector3.down, out hit, 20.0f);
            dropStartPos = Main.transform.position.y;
            owner.rb.velocity = Vector3.zero;
            CTime = 0;
            owner.StartCoroutineFunction("ChangeDushBuff");
            Instantiate(owner.ChaergeBreakEf, owner.gameObject.transform.position, Quaternion.identity);
        }
        public override void OnUpdate(YukinoMain owner)
        {
            if (Main.cancelHDorpCTime > 0) Main.cancelHDorpCTime -= Time.deltaTime;
            if (Main.Hipdrop == false) owner.ChangeState(stateIdle);//停止
            if (Main.Ground)
            {
                Physics.SphereCast(owner.transform.position, owner.groundRadius, Vector3.down, out RaycastHit hit, owner.groundDistance);

                if (hit.collider.tag != "Ground" && hit.collider.tag != "JumpPad")
                {
                    owner.ChangeState(stateIdle);
                }

                else if (hit.collider.tag == "Ground")
                {
                    IceBreak iceBreak;
                    hit.collider.TryGetComponent(out iceBreak);
                    iceBreak.BrokenBridge();
                    GameManager.Sound.SetCueName("Break_Tsunagu");
                    GameManager.Sound.OnSound();
                }
            }
        }
        public override void OnFixedUpdate(YukinoMain owner)
        {
            CTime += Time.fixedDeltaTime;//Time.deltaTime;
            if (CTime > 0.5f)
            {
                Main.rb.velocity = new Vector3(0, Main.hipdrop, 0);
                //CTime = 0;
            }
            else
            {
                owner.rb.velocity = Vector3.zero;
            }

            //Main.rb.velocity = new Vector3(0, Main.hipdrop, 0);
        }
        public override void OnExit(YukinoMain owner, PlayerStateBase nextState)
        {
            owner.StopCoroutineFunction("ChangeDushBuff");
            if (Main.Ground)
            {
                Sound.SetCueName("Hip_Drop");
                Sound.OnSound();

                dropEndPos = Main.transform.position.y;
                float dropDistance = dropStartPos - dropEndPos;
                EffectSet(dropDistance);
                //Physics.Raycast(Main.transform.position, Vector3.down, out hit, 1.2f);
                Main.setactivHipdrop(true);
                Main.Hipdrop = false;
                /*Main.childObject[Main.index].SetActive(false);
                Main.index = 0;
                Main.childObject[Main.index].SetActive(true);*/
            }
        }

        private void EffectSet(float effSize)
        {
            GameObject child = Main.HipDropEf.transform.GetChild(0).gameObject;//ヒップドロップエフェクトの子（root）
            Transform children = child.GetComponentInChildren<Transform>();//rootの子をすべて取得
            foreach (Transform ob in children)
            {
                var part = ob.gameObject.GetComponent<ParticleSystem>();
                var main = part.main;
                if (effSize > 20)
                {
                    main.startSizeX = 0.11f;
                    main.startSizeY = 0.11f;
                }
                else if (effSize > 0)
                {
                    main.startSizeX = 0.01f + 0.005f * effSize;
                    main.startSizeY = 0.01f + 0.005f * effSize;
                }
                else
                {
                    main.startSizeX = 0.01f;
                    main.startSizeY = 0.01f;
                }
            }
            Instantiate(Main.HipDropEf, Main.body.transform.position, Quaternion.identity);
        }
    }
}