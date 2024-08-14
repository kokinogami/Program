using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shindo_Input : MonoBehaviour
{
    Shindo_Main Main;
    public float rotate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Main = GetComponent<Shindo_Main>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveControroller();
        Main.Move = Main.moveAction.ReadValue<Vector2>();//移動操作操作取得
        Main.Look = Main.lookAction.ReadValue<Vector2>();//カメラ操作取得
    }
    void MoveControroller()//移動操作スクリプト
    {
        Main.rb.constraints = RigidbodyConstraints.FreezeRotationX;
        Main.rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        /*if (Main.tenjou == true)
        {
            Main.rb.AddForce(0.0f, 9.8f, 0.0f);
        }*/

        if (Main.DushCount > 0.0f && Main.Move.magnitude > 0.0f)//前転中
        {
            Main.LimitSpeed = 60.0f;
            Main.DushCount -= 1.0f * Time.deltaTime;
            Main.childObject[Main.index].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Main.rb.velocity = transform.forward * Main.LimitSpeed * Main.Move.normalized.magnitude + new Vector3(0.0f, Main.rb.velocity.y, 0.0f);
        }
        else if (Main.Dush == true && Main.Move.magnitude > 0.0f)//雪玉時
        {
            Main.LimitSpeed = 50.0f;
            Main.childObject[Main.index].SetActive(false);
            Main.index = 1;
            Main.childObject[Main.index].SetActive(true);
            Main.rb.velocity = transform.forward * Main.LimitSpeed * Main.Move.normalized.magnitude + new Vector3(0.0f, Main.rb.velocity.y, 0.0f);
            if (Main.Move.x > 0)
            {
                Quaternion targetRot = Quaternion.AngleAxis(rotate, Vector3.up) * transform.rotation;
                transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRot, rotate);
            }
            else if (Main.Move.x < 0)
            {
                Quaternion targetRot = Quaternion.AngleAxis(rotate, -1 * Vector3.up) * transform.rotation;
                transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRot, rotate);
            }
        }
        else//人型
        {
            Main.LimitSpeed = 25.0f;
            Main.childObject[Main.index].SetActive(false);
            Main.index = 0;
            Main.childObject[Main.index].SetActive(true);
            Main.childObject[Main.index].transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            Main.rb.velocity = transform.forward * Main.LimitSpeed * Main.Move.magnitude + new Vector3(0.0f, Main.rb.velocity.y, 0.0f);
        }
        //Main.rb.AddRelativeForce(Vector3.forward * Main.Move.magnitude * (Main.LimitSpeed - Main.Aspeed.magnitude) * Main.speed, ForceMode.Force);

        if (Main.Ground == true && Main.Move.magnitude > 0)
        {
            //Main.yukinoanime.Walkstr();
            Main.AudioEnd = true;
        }
        if (Main.Ground == true || Main.Grider == true)//移動制限
        {
        }
    }
    public void JumpController(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Main.Ground)
            {
                Main.Ground = false;
                Main.rb.AddForce(0.0f, Main.Jump, 0.0f, ForceMode.Impulse);
                //Main.yukinoanime.Jumpstr();
                if (Main.index == 1)
                {
                    Main.childObject[Main.index].SetActive(false);
                    Main.index = 0;
                    Main.childObject[Main.index].SetActive(true);
                }
            }
            else if (Main.Ground == false)
            {

                if (Main.Grider)
                {
                    Main.rb.drag = 0.5f;
                    Main.Grider = false;
                    Main.griderObj.SetActive(false);
                    Main.childObject[Main.index].SetActive(false);
                    Main.index = 0;
                    Main.childObject[Main.index].SetActive(true);
                }
                else if (Main.mushi == 1)
                {
                    if (Main.mushi == 1)
                    {
                        Main.Ground = false;
                        Main.rb.constraints = RigidbodyConstraints.FreezeRotation;
                        Main.rb.velocity = Vector3.zero;
                        Main.rb.AddRelativeForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
                        Main.mushiObj.SetActive(true);
                        Main.mushiCount = 0.6f;
                        Main.mushi = 0;
                    }
                }
            }

        }
        if (context.performed && Main.Grider == false)
        {
            Main.rb.drag = 8;
            Main.Grider = true;
            Main.griderObj.SetActive(true);
        }
    }
    public void Change(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Main.DushCount = 0.3f;
        }
        if (context.performed)
        {
            Main.Dush = true;
        }
        if (context.canceled)
        {
            Main.DushCount = 0.0f;
            Main.Dush = false;
        }
    }
    public void JumpPadSet(InputAction.CallbackContext context)//ジャンプ台設置
    {
        if (context.started)
        {
            if (Main.Ground && Main.HP >= 10)
            {
                Vector3 PadPosition = transform.position + 2.0f * Vector3.down + Main.rb.velocity.normalized * 3.0f;
                Instantiate(Main.JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
                Main.HP -= 10;
                Main.TimeCount = 3.0f;
            }
        }
    }
    public void HipDrop(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Main.Ground == false)
            {
                Main.rb.AddForce(new Vector3(0.0f, Main.hipdrop, 0.0f), ForceMode.Impulse);
                Debug.Log("hipdrop");
                Main.childObject[Main.index].SetActive(false);
                Main.index = 1;
                Main.childObject[Main.index].SetActive(true);
                Main.Hipdrop = true;
            }
        }
    }
}
