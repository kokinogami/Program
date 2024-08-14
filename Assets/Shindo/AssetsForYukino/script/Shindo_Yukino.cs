using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// ���̂Q�s�̃R�[�h�ŃR���|�[�l���g�������I�ɒǉ�����܂��B
[RequireComponent(typeof(CharacterController))]
public class Shindo_Yukino : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed = 10.0f;//�����x
    public float LimitSpeed = 50.0f;//���x���
    public float Jump = 10.0f;//�W�����v��
    public float TimeCount;//�����񕜃C���^�[�o��
    public int HP;//?���l
    public int HPmax;
    public int GaugeQuantity;
    public int mushi;//��峂̉񐔐���
    public int index = 0;//�l�Ɛ�ʂ̐؂�ւ�
    private int o_max = 0;
    private float mushiCount;

    public Vector3 Rotate;
    public Vector3 Startpos;//���X�|�[���n�_

    public Slider GaugeMain;//�e�Q�[�W
    public Slider Gauge2;
    public Slider Gauge3;
    public GameObject UI3;

    public bool Ground;//�n�ʂƂ̐ڐG����
    public bool Grider;//�O���C�_�[�g�p�����ǂ����̔���
    public bool Count;//�񕜃C���^�[�o���Ǘ�
    public bool joushou;
    public bool tenjou;

    public bool IceBall { get; set; }

    public GameObject body;
    public GameObject Ice;
    public GameObject JumpPadPrefab;
    GameObject[] childObject;
    public GameObject griderObj;
    public GameObject mushiObj;

    private Vector3 bodyHeight = new Vector3(0.0f, -1.000001f, 0.0f);  //�X�𐶐�����ʒu�Ɋ֌W�B�̂̑傫���ƕX�̌����Ɉˑ�

    RaycastHit hit;

    //InputSystem
    private InputAction moveAction;
    private InputAction lookAction;
    private Vector2 Move;
    public Vector2 Look;

    Vector2 Aspeed = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Ground = true;

        GaugeQuantity = 2;
        GaugeMain.value = 100;
        GaugeMain.maxValue = 100;
        Gauge2.value = 0;
        Gauge2.maxValue = 100;
        Gauge3.value = 0;
        Gauge3.maxValue = 100;
        HP = 100;
        HPmax = 100 * GaugeQuantity;
        mushi = 1;
        TimeCount = 3.0f;
        Count = true;

        Rotate = Vector3.zero;
        Startpos = this.transform.position + new Vector3(0.0f, 3.0f, 0.0f);

        o_max = this.transform.childCount;//�q�I�u�W�F�N�g�̌��擾
        childObject = new GameObject[o_max];//�C���X�^���X�쐬

        for (int i = 0; i < o_max; i++)
        {
            childObject[i] = transform.GetChild(i).gameObject;//���ׂĂ̎q�I�u�W�F�N�g�擾
        }
        //���ׂĂ̎q�I�u�W�F�N�g���A�N�e�B�u
        foreach (GameObject gamObj in childObject)
        {
            gamObj.SetActive(false);
        }
        //�ŏ��͂ЂƂ����A�N�e�B�u�����Ă���
        childObject[index].SetActive(true);
        //InputSystem
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        moveAction = actionMap["Move"];
        lookAction = actionMap["Look"];
    }

    // Update is called once per frame
    void Update()
    {
        Move = moveAction.ReadValue<Vector2>();//�ړ����쑀��擾
        Look = lookAction.ReadValue<Vector2>();//�J��������擾

        debugcom();
        ConnectIce();
        //Freez();
        HPGaugeControl();
        Joushoukiryu();

        Aspeed = new Vector2(rb.velocity.x, rb.velocity.z);//���x�x�N�g��

        if (Ground == true || Grider == true)//�ړ�����
        {
            MoveControroller();
        }

        if ((Mathf.Abs(Aspeed.magnitude) >= LimitSpeed))//�ő呬�x
        {
            Vector3 Vero = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.velocity = Vero.normalized * LimitSpeed;
        }

        if (Input.GetKey("left shift"))
        {
            speed = 200.0f;
            //if (HP >= 1)
            //    rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
        }
        else
        {
            speed = 100.0f;
        }

        if (Input.GetKey("space") && Ground == true)
        {
            rb.AddForce(0.0f, Jump, 0.0f, ForceMode.Impulse);
            Ground = false;
        }

        if (Input.GetKeyDown("1") && mushi == 1)
        {
            Ground = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.AddForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
            mushi = 0;
            Ground = false;
        }
        /*if (Input.GetMouseButtonDown(1) && Ground == false)
        {
            if (Grider == false)
            {
                rb.drag = 8;
                Grider = true;
                griderObj.SetActive(true);
            }
            else
            {
                rb.drag = 0.5f;
                Grider = false;
                griderObj.SetActive(false);
            }
        }*/

        if (mushiCount <= 0)
        {
            mushiObj.SetActive(false);
        }
        else
            mushiCount -= Time.deltaTime; 
    }
    private void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * Move.y + Camera.main.transform.right * Move.x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        //rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//�n�ʂƂ̐ڐG����
        {
            Ground = true;
            Grider = false;
            griderObj.SetActive(false);
            rb.drag = 0.5f;
            mushi = 1;
        }
        if (collision.gameObject.tag == "JumpPad")//�W�����v��
        {
            rb.AddForce(0, Jump, 0, ForceMode.Impulse);
            Ground = false;
            griderObj.SetActive(false);
        }
        if (collision.gameObject.tag == "BrokenObject")
        {
            Destroy(collision.gameObject);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("heal") == true)//�񕜃A�C�e��
        {
            HP += 100;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("joushou"))
        {
            joushou = true;
        }
        if (other.CompareTag("tenjou"))
        {
            tenjou = true;
        }
        if (other.CompareTag("checkPoint"))//���X�|�[���n�_�̕ύX
        {
            Startpos = other.transform.position;
        }

        if (other.CompareTag("DeathZone"))//��������ƃ��X�|�[������
        {
            rb.velocity = Vector3.zero;
            this.transform.position = Startpos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("joushou"))
        {
            joushou = false;
        }

        if (other.CompareTag("tenjou"))
        {
            tenjou = false;
        }
    }

    private void Joushoukiryu()
    {
        if (tenjou == true && Grider == true)
        {
            
        }

        else if (joushou == true && Grider == true)
        {
            rb.velocity = new Vector3(0.0f, 9.83f, 0.0f);
        }
    }

    void MoveControroller()//�ړ�����X�N���v�g
    {

        if (tenjou == true && Grider == true)
        {
            rb.AddForce(0.0f, 9.8f, 0.0f);
        }
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        //if ((Mathf.Abs(Aspeed.magnitude) < LimitSpeed)){
        rb.AddRelativeForce(0.0f, 0.0f, Move.magnitude * speed, ForceMode.Force);
    }
    public void JumpController(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (index == 0)
            {
                if (Ground)
                {
                    rb.AddForce(0.0f, Jump, 0.0f, ForceMode.Impulse);
                    Ground = false;
                }
                else if (Ground == false)
                {
                    if (Grider == false)
                    {
                        rb.drag = 8;
                        Grider = true;
                    }
                    else
                    {
                        rb.drag = 0.5f;
                        Grider = false;
                    }
                }
            }
        }
    }
    private void Freez()//WASD���͂��Ȃ��������Ȃ��悤�ɂ���i���������j
    {
        if (Move.magnitude == 0 || Ground == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
    }


    void ConnectIce()
    {
        if (Input.GetKey(KeyCode.LeftShift)) //�_�b�V�����A�󒆂ɂ���ΕX�̏��𐶐�
        {
            if (Physics.Raycast(body.transform.position, Vector3.down, out hit, 5.1f) == false && Ground && HP > 1)
            {
                rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                Vector3 horisonMove = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                Vector3 verticalMove = new Vector3(0.0f, rb.velocity.y, 0.0f);

                Vector3 IcePosition = body.transform.position + bodyHeight + horisonMove.normalized * 2.5f;
                Instantiate(Ice, IcePosition, Quaternion.identity);  //�X�̏��𐶐�
                HP -= 1;//�ݒu�����HP��-1�����
                TimeCount = 3.0f;
            }
        }
    }

    void HPGaugeControl()
    {
        if (Input.GetKey(KeyCode.Q) & HP >= 10)
        {
            HP -= 10;
            TimeCount = 3.0f;
        }

        if (Input.GetKey(KeyCode.O) & GaugeQuantity < 3)//HP�o�[����
        {
            UI3.SetActive(true);
            GaugeQuantity++;
            HPmax = 100 * GaugeQuantity;
        }
        if (HP > HPmax)//�ő�HP����
        {
            HP = HPmax;
        }
        if (HP < 100 & Count == false)//�����񕜂Ɛ���
        {
            HP = HP + 1;
        }
        GaugeMain.value = HP;//UI�̕ύX
        Gauge2.value = HP - 100;
        Gauge3.value = HP - 200;
        if (TimeCount > 0.0f)//�����񕜂̃C���^�[�o������
        {
            TimeCount -= Time.deltaTime;
            Count = true;
        }
        else
        {
            Count = false;
        }
    }

    public void Change(InputAction.CallbackContext context)
    {
        //���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g���A�N�e�B�u
        childObject[index].SetActive(false);
        if (context.started)
        {
            LimitSpeed = 50;
            index = 1;
            rb.mass = 10;
            IceBall = true;
        }
        if (context.canceled)
        {
            LimitSpeed = 25;
            index = 0;
            rb.mass = 50;
            IceBall = false;
        }
        //���̃I�u�W�F�N�g���A�N�e�B�u��
        childObject[index].SetActive(true);
    }
    public void Mushi(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (mushi == 1)
            {
                Ground = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = Vector3.zero;
                rb.AddForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
                mushiObj.SetActive(true);
                mushiCount = 0.6f;
                mushi = 0; ;
                Ground = false;
            }
        }
    }
    public void JumpPadSet(InputAction.CallbackContext context)//�W�����v��ݒu
    {
        if (context.started)
        {
            if (Ground && HP >= 10)
            {
                if (Physics.Raycast(this.transform.position + rb.velocity, Vector3.down, out hit, 3.0f))
                {
                    Vector3 PadPosition = transform.position + 3.0f * Vector3.down + rb.velocity.normalized * 3.0f;
                    Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
                    HP -= 10;
                    TimeCount = 3.0f;
                }
                else
                {
                    Vector3 IcePosition = body.transform.position + bodyHeight + rb.velocity.normalized * 3.0f;
                    Instantiate(Ice, IcePosition, Quaternion.identity);  //�X�̏��𐶐�
                    Vector3 PadPosition = transform.position + 3.0f * Vector3.down + rb.velocity.normalized * 3.0f;
                    Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));//�W�����v��𐶐�
                    HP -= 11;
                    TimeCount = 3.0f;
                }
            }
        }
    }

    void debugcom()
    {
        //Debug.Log(index);
        //Debug.Log(rb.velocity.magnitude);
        //Debug.Log(Move);
        Debug.Log(Move.magnitude);
    }
}
