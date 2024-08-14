using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class YukinoAnie : MonoBehaviour
{
    Animator animator;

    private string WalkStr = "isWalk";
    private string JumpStr = "isJump";
    private string ForStr = "isForward";
    public float jumpcount;

    // スタート時に呼ばれる
    /*void Start()
    {
        animator = GetComponent<Animator>();
        jumpcount = 10.0f;
    }*/

    // フレーム毎に呼ばれる
    /*void Update()
    {
        jumpcount = jumpcount+1.0f * Time.deltaTime;
        if (jumpcount >= 1.0)
        {
            Jumpfin();
        }
    }*/
    public void Walkstr()
    {
        this.animator.SetBool(WalkStr, true);
    }
    public void Walkfin()
    {
        this.animator.SetBool(WalkStr, false);
    }
    public void Jumpstr()
    {
        jumpcount = 0.0f;
        this.animator.SetBool(JumpStr, true);
    }
    public void Jumpfin()
    {
        this.animator.SetBool(JumpStr, false);
    }
    public void Forstr()
    {
        jumpcount = 0.0f;
        this.animator.SetBool(ForStr, true);
    }
    public void Forfin()
    {
        this.animator.SetBool(ForStr, false);
    }
}
