using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class Shindo_Main : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClipList = new List<AudioClip>();

    [SerializeField] public AudioSource a;//AudioSource�^�̕ϐ�a��錾 �g�p����AudioSource�R���|�[�l���g���A�^�b�`�K�v

    [SerializeField] public AudioClip walk;//AudioClip�^�̕ϐ�b1��錾 �g�p����AudioClip���A�^�b�`�K�v
    [SerializeField] public AudioClip ball;
    public Rigidbody rb;
    public float speed;//�����x
    public float LimitSpeed;//���x���
    public float Jump;//�W�����v��
    public float TimeCount;//�����񕜃C���^�[�o��
    public float mushiCount;
    public float DushCount;
    public float hipdrop;
    public float connectCoolTime;//�W�����v��A���ꐶ���J�n�܂ł̃N�[���^�C��
    public int HP;//?���l
    public int HPmax;
    public int GaugeQuantity;
    public int mushi;//��峂̉񐔐���
    public int index = 0;//�l�Ɛ�ʂ̐؂�ւ�
    public int o_max = 0;
    public int enemyNum;//�G�̐�

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
    public bool Dush;
    public bool Hipdrop;
    public bool AudioEnd;
    public bool Result = false;//���U���g�\�������ۂ�

    public GameObject body;
    public GameObject Ice;
    public GameObject JumpPadPrefab;
    public GameObject explosionPrefab;
    public GameObject griderObj;
    public GameObject mushiObj;
    public GameObject resultObj;
    public GameObject[] childObject;
    public GameObject[] enemies;
    [SerializeField] GameObject rolleffect;

    public Vector3 bodyHeight = new Vector3(0.0f, -1.000001f, 0.0f);  //�X�𐶐�����ʒu�Ɋ֌W�B�̂̑傫���ƕX�̌����Ɉˑ�

    public RaycastHit hit;

    //InputSystem
    public InputAction moveAction;
    public InputAction lookAction;
    public Vector2 Move;
    public Vector2 Look;

    public Vector2 Aspeed = Vector2.zero;

    public YukinoAnime yukinoanime;
    // Start is called before the first frame update
    void Start()
    {
        speed = 20.0f;
        LimitSpeed = 25.0f;
        Jump = 10.0f;
        hipdrop = -30.0f;

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
        connectCoolTime = 0.0f;
        Count = true;
        AudioEnd = false;
        Hipdrop = false;

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

        Startpos = this.transform.position + new Vector3(0.0f, 3.0f, 0.0f);

        enemies = GameObject.FindGameObjectsWithTag("zako");
        enemyNum = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Freez();
        debugcom();
        effect();
        Aspeed = new Vector2(rb.velocity.x, rb.velocity.z);//���x�x�N�g��

        if (enemyNum == 0 && Result == false)
        {
            resultObj.SetActive(true);
            Result = true;
        }
    }
    private void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * Move.y + Camera.main.transform.right * Move.x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        if (moveForward != Vector3.zero)
        {
            if (index == 0)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
    }
    void debugcom()
    {
        //Debug.Log(Ground);
        //Debug.Log(rb.velocity.magnitude);
        //Debug.Log(Move);
        //Debug.Log(Move.magnitude);
        //Debug.Log(LimitSpeed);
        Debug.Log(Move);
    }
    private void Freez()//WASD���͂��Ȃ��������Ȃ��悤�ɂ���i���������j
    {
        if (Move.magnitude == 0 || Ground == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
        }
    }
    private void effect()
    {
        if (index == 1)
        {
            rolleffect.SetActive(true);
        }
        else
        {
            rolleffect.SetActive(false);
        }
    }
}