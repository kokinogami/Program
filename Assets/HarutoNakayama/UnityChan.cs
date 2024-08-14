using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan : MonoBehaviour
{
    Animator animator;

    private string WalkStr = "isWalk";
    private string JumpStr = "isJump";
    private string ForStr = "isForward";

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
            transform.position += transform.forward * 0.01f;
            this.animator.SetBool(WalkStr, true);
        }
        else
        {
            this.animator.SetBool(WalkStr, false);
        }

        // 左右回転
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.animator.SetBool(JumpStr, true);
        }
        else
        {
            this.animator.SetBool(JumpStr, false);
        }

        //前転
        if (Input.GetKey(KeyCode.U))
        {
            transform.position += transform.forward * 0.01f;
            this.animator.SetBool(ForStr, true);
        }
        else
        {
            this.animator.SetBool(ForStr, false);
        }
    }
}