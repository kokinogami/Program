using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuka : MonoBehaviour
{
    private YukinoMain YukinoMain; //���L�m����p�X�N���v�g�B��������Ƃ��͖��O�����������Ă�
    private int iceball;
    private float wait = 1.0f;
    private float oldwait = 0.6f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.YukinoMain = FindObjectOfType<YukinoMain>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iceball == 1 && wait > 0)
        {
            wait -= Time.deltaTime;
        }

        if (wait <= 0 && oldwait > 0)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }

        oldwait = wait;
    }

    private void OnCollisionEnter(Collision collision)
    {
        iceball = YukinoMain.index;
    }
}
