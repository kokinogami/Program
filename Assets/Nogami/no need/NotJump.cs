using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotJump : MonoBehaviour
{
    YukinoMain Main;
    private float Count;
    float VeloY = 0;
    bool Velo = false;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out Main);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Main.Ground)
        {
            Count = 1.0f;
        }
        else if (Count > 0.0f)
        {
            Count -= Time.deltaTime;
        }
        if (Count > 0.0f && Main.index == 1 && Main.rb.velocity.y >= 0.0f)
        {
            if (Physics.Raycast(this.transform.position, transform.forward, out hit, 2.0f) == false)
            {
                Main.rb.AddForce(0.0f, -1 * Main.rb.velocity.y, 0.0f, ForceMode.Impulse);
            }
            //if (Main.rb.velocity.y < 0.0f)
            //{
            //    VeloY = Main.rb.velocity.y;
            //    Velo = true;
            //}
            //Main.rb.velocity = new Vector3(Main.rb.velocity.x, 0.0f, Main.rb.velocity.z);
            //if (Velo)
            //{
            //    Main.rb.velocity = new Vector3(Main.rb.velocity.x, VeloY, Main.rb.velocity.z);
            //    Velo = false;
            //}
            //Main.rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            //Main.rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        //Debug.Log(Main.rb.velocity.magnitude);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpPad")//ジャンプ台
        {
            Count = 0.0f;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "JumpPad")//ジャンプ台
        {
            Count = 0.0f;
        }
    }
}
