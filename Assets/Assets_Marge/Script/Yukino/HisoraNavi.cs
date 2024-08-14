using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HisoraNavi : MonoBehaviour
{
    public GameObject Yukino;//Player
    public GameObject Target;//GiapiiPos
    private float speed;//移動速度
    private Vector3 YukinoPos;//Playerの位置
    private Vector3 NavPositon;//GiapiiPosの位置
    Rigidbody rb;
    [SerializeField] Animator animator;
    public static HisoraNavi hisoraNavi;

    public Vector3 HeadMove;//ユキノの上に移動するための関数

    [SerializeField] CapsuleCollider griderColloder;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out hisoraNavi);
        speed = 3.0f;
        rb = this.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (YukinoMain.Yukinocurrentstate == YukinoMain.YukinoState.Glider)
        {
            Griider();
            if (griderColloder.enabled == false)
            {
                griderColloder.enabled = true;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            NonGriider();
            if (griderColloder.enabled == true)
            {
                griderColloder.enabled = false;
            }
        }
    }
    void NonGriider()
    {
        NavPositon = Target.transform.position;
        YukinoPos = Yukino.transform.position;
        if (Vector3.Distance(this.gameObject.transform.position, NavPositon) >= 0.5f)//GiapiiPosとGiapiiの距離
        {
            transform.LookAt(NavPositon);
            rb.velocity = transform.forward * speed;// * Time.deltaTime;
                                                    //transform.position += transform.forward * speed * Time.deltaTime;
            speed = 3.0f + Vector3.Distance(this.gameObject.transform.position, NavPositon) * 3;
            /*if (Vector3.Distance(this.gameObject.transform.position, NavPositon) >= 6)
            {
                //rb.velocity = new Vector3(0, 0, 0);
                speed = 3.0f + Vector3.Distance(this.gameObject.transform.position, NavPositon) * 2;
            }
            else
            {
                speed = 3.0f;
            }*/
        }
        else
        {
            transform.rotation = Yukino.transform.rotation;
            speed = 3.0f;
            rb.velocity = Vector3.zero;
            transform.LookAt(new Vector3(YukinoPos.x, NavPositon.y, YukinoPos.z));
        }
    }
    void Griider()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    public void SetAnime(bool setbool)
    {
        animator.SetBool("Grider", setbool);
    }
}
