using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    [SerializeField] float Jumpforce = 20;
    [SerializeField] float JumpForceUp = 2;
    Vector3 jumpVelocity;
    float hipdropcount = 0;
    const float DFtime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        jumpVelocity = new Vector3(0, Jumpforce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        jumpVelocity = new Vector3(0, Jumpforce, 0);
        //Debug.Log(GameManager.Main.rb.velocity);
        switch (GameManager.Main.Hipdrop)
        {
            case true:
                hipdropcount = 1.0f;
                break;
            default:
                if (hipdropcount > 0)
                {
                    hipdropcount -= Time.deltaTime;
                }
                break;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<Rigidbody>().AddForce(0, Jumpforce, 0, ForceMode.VelocityChange);
    //        switch (hipdropcount > 0)
    //        {
    //            case true:
    //                GameManager.Main.rb.velocity = jumpVelocity * JumpForceUp;
    //                break;
    //            default:
    //                GameManager.Main.rb.velocity = jumpVelocity;
    //                break;
    //        }
    //        GameManager.Sound.SetCueName("Trampoline");
    //        GameManager.Sound.OnSound();
    //        GameManager.Main.JumpPadObj = true;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        if (other.gameObject.tag == "Player" && GameManager.Main.JumpPadObj <= 0)
        {
            switch (hipdropcount > 0)
            {
                case true:
                    GameManager.Main.rb.velocity = jumpVelocity * JumpForceUp;
                    break;
                default:
                    GameManager.Main.rb.velocity = jumpVelocity;
                    break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Main.Dush = false;
            GameManager.Main.JumpPadObj = DFtime;
            switch (hipdropcount > 0)
            {
                case true:
                    GameManager.Main.rb.velocity = jumpVelocity * JumpForceUp;
                    break;
                default:
                    GameManager.Main.rb.velocity = jumpVelocity;
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Sound.SetCueName("Trampoline");
            GameManager.Sound.OnSound();
            GameManager.Main.JumpPadObj = 0;
        }
    }
}
