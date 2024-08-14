using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anime: MonoBehaviour
{
    Animator animator;

    private string WalkStr = "isWalk";
    private string JumpStr = "isJump";

    // スタート時に呼ばれる
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // フレーム毎に呼ばれる
    void Update()
    {
        // 前進
        if (Input.GetKey(KeyCode.W))
        {
            this.animator.SetBool(WalkStr, true);
        }
        else
        {
            this.animator.SetBool(WalkStr, false);
        }

        // 左右回転
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