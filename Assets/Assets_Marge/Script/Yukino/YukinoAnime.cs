using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukinoAnime : MonoBehaviour
{
    Animator animator;

    const string WalkStr = "isWalk";
    const string JumpStr = "isJump";
    const string ForStr = "isForward";
    const string GriderStr = "isGrider";
    const string GroundStr = "isGround";
    const string ChargeStr = "isCharge";
    float jumpcount;

    // スタート時に呼ばれる
    void Start()
    {
        animator = GetComponent<Animator>();
        jumpcount = 10.0f;
    }

    // フレーム毎に呼ばれる
    void Update()
    {
        jumpcount = jumpcount+1.0f * Time.deltaTime;
        if (jumpcount >= 1.0)
        {
            JumpBool(false);
        }
    }
    public void WalkBool(bool Bool)
    {
        this.animator.SetBool(WalkStr,Bool);
    }
    public void JumpBool(bool Bool)
    {
        jumpcount = 0.0f;
        this.animator.SetBool(JumpStr, Bool);
    }
    public void Forbool(bool Bool)
    {
        this.animator.SetBool(ForStr, Bool);
    }
    public void Griderbool(bool Bool)
    {
        this.animator.SetBool(GriderStr, Bool);
    }
    public void Groundbool(bool Bool)
    {
        this.animator.SetBool(GroundStr, Bool);
    }
    public void Chargebool(bool Bool)
    {
        this.animator.SetBool(ChargeStr, Bool);
        if (Bool)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        else
        {
            animator.updateMode = AnimatorUpdateMode.Normal;
        }
    }
}