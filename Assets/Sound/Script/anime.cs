using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anime: MonoBehaviour
{
    Animator animator;

    private string WalkStr = "isWalk";
    private string JumpStr = "isJump";

    // �X�^�[�g���ɌĂ΂��
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // �t���[�����ɌĂ΂��
    void Update()
    {
        // �O�i
        if (Input.GetKey(KeyCode.W))
        {
            this.animator.SetBool(WalkStr, true);
        }
        else
        {
            this.animator.SetBool(WalkStr, false);
        }

        // ���E��]
        if (Input.GetKey(KeyCode.A))
        {
        }
        else if (Input.GetKey(KeyCode.D))
        {
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.animator.SetBool(JumpStr, true);
        }
        else
        {
            this.animator.SetBool(JumpStr, false);
        }
    }
}