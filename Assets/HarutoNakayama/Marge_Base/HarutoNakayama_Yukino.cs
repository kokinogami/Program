using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// ���̂Q�s�̃R�[�h�ŃR���|�[�l���g�������I�ɒǉ�����܂��B
[RequireComponent(typeof(CharacterController))]
public class HarutoNakayama_Yukino : MonoBehaviour
{
    int b;
    public AudioSource audioSource;
    public List<AudioClip> audioClipList = new List<AudioClip>();
    CapsuleCollider CapCol;
    private bool AudioEnd;

    [SerializeField] private AudioSource a;//AudioSource�^�̕ϐ�a��錾 �g�p����AudioSource�R���|�[�l���g���A�^�b�`�K�v

    [SerializeField] private AudioClip walk;//AudioClip�^�̕ϐ�b1��錾 �g�p����AudioClip���A�^�b�`�K�v
    [SerializeField] private AudioClip ball;



    public void OnSound(InputAction.CallbackContext context)
    {
        if (index == 0)  //�l�^��Ԏ�
        {
            a.PlayOneShot(walk);
        }
        if (index == 1)  //��ʏ�Ԏ�
        {
            a.PlayOneShot(ball);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)//�W�����v��
        {

        }
        if (Grider)//�O���C�_�[�g�p��
        {

        }
    }
    // Start is called before the first frame update
    Rigidbody rb;
    public static float speed = 50.0f;//�����x
    public static float LimitSpeed = 50.0f;//���x���
    public static float Jump = 10.0f;//�W�����v��
    public static float TimeCount;//�����񕜃C���^�[�o��
    private static float mushiCount;
    public static int HP;//?���l
    public static int HPmax;
    public static int GaugeQuantity;
    public static int mushi;//��峂̉񐔐���
    public static int index = 0;//�l�Ɛ�ʂ̐؂�ւ�
    private static int o_max = 0;

    public static Vector3 Rotate;

    public static Slider GaugeMain;//�e�Q�[�W
    public static Slider Gauge2;
    public static Slider Gauge3;
    public static GameObject UI3;

    public static bool Ground;//�n�ʂƂ̐ڐG����
    public static bool Grider;//�O���C�_�[�g�p�����ǂ����̔���
    public static bool Count;//�񕜃C���^�[�o���Ǘ�
    public static bool joushou;
    public static bool tenjou;

    public static GameObject body;
    public static GameObject Ice;
    public static GameObject JumpPadPrefab;
    public static GameObject explosionPrefab;
    //public GameObject griderObj;
    //public GameObject mushiObj;
    static GameObject[] childObject;

    private static Vector3 bodyHeight = new Vector3(0.0f, -1.000001f, 0.0f);  //�X�𐶐�����ʒu�Ɋ֌W�B�̂̑傫���ƕX�̌����Ɉˑ�

    RaycastHit hit;

    //InputSystem
    private static InputAction moveAction;
    private static InputAction lookAction;
    private static Vector2 Move;
    public static Vector2 Look;

    static Vector2 Aspeed = Vector2.zero;

    public static YukinoAnime yukinoanime;

    void Start()
    {
        b = TitleSystem.getA();
        print(b);
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

        AudioEnd = false;
        CapCol = GetComponent<CapsuleCollider>();

       
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
        if (Move.magnitude == 0 && index == 0 && Ground)
        {
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            //yukinoanime.Walkfin();
        }

        if ((Mathf.Abs(Aspeed.magnitude) >= LimitSpeed))//�ő呬�x
        {
            Vector3 Vero = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.velocity = Vero.normalized * LimitSpeed;
        }
        if (mushiCount <= 0)
        {
            //mushiObj.SetActive(false);
        }
        else
            mushiCount -= Time.deltaTime;
        if (Grider)
        {
            speed = 50.0f;
        }
        else
        {
            speed = 10.0f;
        }
        /*if (Input.GetKey("left shift"))
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

        if (Input.GetKey("1") && mushi == 1)
        {
            Ground = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.AddForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
            mushi = 0; ;
            Ground = false;
        }
        if (Input.GetMouseButtonDown(1) && Ground == false)
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
        }*/

        
        if(Aspeed.magnitude <= 0 && AudioEnd)
        {           
            Debug.Log("AudioEnd");
            AudioEnd = false;
        }

        if( Keyboard.current.hKey.wasPressedThisFrame)
        {
            CapCol.enabled = false ;

          Invoke("modoru", 3);

        }


    }

    void modoru()
    {
        CapCol.enabled = true;
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
            rb.drag = 0.5f;
            mushi = 1;
            //griderObj.SetActive(false);
        }
        if (collision.gameObject.tag == "enemy")
        {
            if (index == 0)
            {
                HP -= 10;
                TimeCount = 3.0f;
            }
        }
        if (collision.gameObject.tag == "JumpPad")//�W�����v��
        {
            Vector3 JumpDir = collision.transform.rotation * new Vector3(0.0f, 8.0f, -24.0f);
            if (Physics.Raycast(body.transform.position + 0.6f * bodyHeight, new Vector3(rb.velocity.x, 0.0f, rb.velocity.z), out hit, 4.0f) && hit.normal.y >= 0.8f)
            {
                Ground = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(JumpDir, ForceMode.Impulse);
            }
        }
        if (collision.gameObject.tag == "BrokenObject")
        {
            if (index == 1)
            {
                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);//�G�t�F�N�g�̑傫���ݒ�

            }
        }
        if (collision.gameObject.name == "Button")
        {
            // Wall �Ƃ������O�̃I�u�W�F�N�g���擾
            GameObject Wall = GameObject.Find("Wall");
            // �w�肵���I�u�W�F�N�g���폜
            Destroy(Wall);
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
    }
    private static void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("joushou"))
        {
            joushou = false;
        }
    }

    void MoveControroller()//�ړ�����X�N���v�g
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        //if ((Mathf.Abs(Aspeed.magnitude) < LimitSpeed)){
        rb.AddRelativeForce(0.0f, 0.0f, Move.magnitude * speed, ForceMode.Force);
        if (Ground && Move.magnitude > 0)
        {
            //yukinoanime.Walkstr();
            AudioEnd = true;
        }
    }

    
    public void JumpController(InputAction.CallbackContext context)
    {
        if (context.started)
        {
           
                if (Ground)
                {
                childObject[index].SetActive(false);
                index = 0;
                childObject[index].SetActive(true);
                rb.AddForce(0.0f, Jump, 0.0f, ForceMode.Impulse);
                    Ground = false;
                    //yukinoanime.Jumpstr();
                }
                else if (Ground == false)
                {
               
                    if (Grider)
                    {
                        rb.drag = 0.5f;
                        Grider = false;
                        //griderObj.SetActive(false);
                    childObject[index].SetActive(false);
                    index = 0;
                    childObject[index].SetActive(true);
                }
                    else if (mushi == 1)
                    {
                        Mushi();
                    }
                }
            
        }
        if (context.performed && Grider == false)
        {
            rb.drag = 8;
            Grider = true;
            //griderObj.SetActive(true);
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


    void ConnectIce()//�_�b�V�����A�󒆂ɂ���ΕX�̏��𐶐�
    {
        if (index == 1)
        {
            if (Physics.Raycast(body.transform.position, Vector3.down, out hit, 1.1f) == false && Ground && HP > 1)
            {
                //rb.AddForce(0.0f, -1 * rb.velocity.y, 0.0f, ForceMode.VelocityChange);
                rb.useGravity = false;
                Vector3 horisonMove = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                Vector3 verticalMove = new Vector3(0.0f, rb.velocity.y, 0.0f);

                Vector3 IcePosition = body.transform.position + bodyHeight + horisonMove.normalized * 2.5f + new Vector3(0.0f, 0.25f, 0.0f);
                Instantiate(Ice, IcePosition, Quaternion.identity);  //�X�̏��𐶐�
                HP -= 1;//�ݒu�����HP��-1�����
                TimeCount = 3.0f;
            }
            else { rb.useGravity = true; }
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
            //rb.mass = 10;
        }
        if (context.canceled)
        {
            LimitSpeed = 25;
            index = 0;
            //rb.mass = 50;
        }
        //���̃I�u�W�F�N�g���A�N�e�B�u��
        childObject[index].SetActive(true);
    }
    public void Mushi()
    {
        if (mushi == 1)
        {
            Ground = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(0.0f, 8.0f, 5.0f, ForceMode.Impulse);
            //mushiObj.SetActive(true);
            mushiCount = 0.6f;
            mushi = 0;
            Ground = false;
        }
    }
    public void JumpPadSet(InputAction.CallbackContext context)//�W�����v��ݒu
    {
        if (context.started)
        {
            if (Ground && HP >= 10)
            {
                Vector3 PadPosition = transform.position + 3.0f * Vector3.down + rb.velocity.normalized * 3.0f;
                Instantiate(JumpPadPrefab, PadPosition, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
                HP -= 10;
                TimeCount = 3.0f;
            }
        }
    }
    private void Joushoukiryu()
    {
        if (joushou == true && Grider == true)
        {
            rb.velocity = new Vector3(0.0f, 9.83f, 0.0f);
        }
    }

    
     
             

    void debugcom()
    {
        //Debug.Log(index);
        //Debug.Log(rb.velocity.magnitude);
        //Debug.Log(Move);
        //Debug.Log(Move.magnitude);
    }
}
