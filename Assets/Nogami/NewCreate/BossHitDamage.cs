using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitDamage : MonoBehaviour
{
    public static BossHPController BossHPController;
    SphereCollider thisCcollider;
    bool onsound;
    [SerializeField] Boss boss;
    float count;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out thisCcollider);
        if (BossHPController == null)
        {
            transform.root.TryGetComponent(out BossHPController);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.StateProcessor.State == boss.StateAttack4 && onsound == false)
        {
            if (count < 4)
            {
                count += Time.deltaTime;
            }
            else if (count >= 4)
            {
                Debug.Log("�r�[����");
                GameManager.Sound.SetCueName("Beam"); //�㏸�C���ɓ������Ƃ�
                GameManager.Sound.OnSound();
                onsound = true;
            }
        }
        else if (boss.StateProcessor.State != boss.StateAttack4 && onsound)
        {
            Debug.Log("�r�[����off");
            GameManager.Sound.SetCueName("Beam"); //�㏸�C���ɓ������Ƃ�
            GameManager.Sound.BeamNextBlock();
            onsound = false;
            count = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.Main.index == 1)
            {
                BossHPController.OnDamage(thisCcollider.ClosestPoint(collision.transform.position));
            }
        }
    }
}
