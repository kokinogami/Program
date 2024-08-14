using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy3
{
    [SerializeField] public float DetectDist = 50f;
    [SerializeField] public float searchAngle = 45f;
    [SerializeField] float selfGravity = 98f;//UseGravity�̑���̏d��
    [SerializeField] public int ShockDamage = 20;
    [SerializeField] public int UnderDamage = 30;
    [SerializeField] public float LookSpeed = 1.5f;//�Z���قǐU��������x�͑����Ȃ�
    [System.NonSerialized] public bool top;
    [System.NonSerialized] public bool ground;
    [System.NonSerialized] public bool InArea;
    [System.NonSerialized] public bool backArea;
    [System.NonSerialized] public bool Insight;
    [System.NonSerialized] public bool look;
    [System.NonSerialized] public bool breaktime;
    [System.NonSerialized] public bool backbreaktime;
    [System.NonSerialized] public Rigidbody rb;
    [System.NonSerialized] public Transform target;
    public GameObject shockwaves;
    public Enemy3Parent subscript;
    Vector3 raypos;
    private Animator animator;
    public string stampStr = "isstamp";
    public string stopStr = "isstop";
    public string idleStr = "isidle";
    // �X�e�[�g�̃C���X�^���X
    private static readonly Statewait3 statewait = new Statewait3();
    private static readonly StateBound statebound = new StateBound();
    private static readonly StateDetect3 statedetect = new StateDetect3();
    /// <summary>
    /// ���݂̃X�e�[�g
    /// </summary>
    private Enemy3StateBase currentState = statewait;
    // Start()����Ă΂��
    private void OnStart()
    {
        currentState.OnEnter(this, null);
        target = GameManager.Main.transform;
        rb = GetComponent<Rigidbody>();
        backbreaktime = true;
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20.0f))
        {
            raypos = hit.point;
        }
        this.animator = GetComponentInChildren<Animator>();
        this.animator.SetBool(idleStr, true);
        this.animator.SetBool(stopStr, false);
        this.animator.SetBool(stampStr, false);
    }

    private void OnUpdate()
    {
        rb.AddForce(0f, -selfGravity, 0f, ForceMode.Acceleration);//StateBound�̕���isKinematic����������A������
        currentState.OnUpdate(this);
        if (look == true)//look��stateBound�̍Ō�܂ň�ʂ�i�񂾂�A���̌�͂�����true�̏��
        {
            subscript.waitDetect();
        }
        if (transform.position.y < raypos.y)//���܂������p�̑Ώ�
        {
            transform.position = new Vector3(transform.position.x, raypos.y, transform.position.z);
        }
        if (raypos.y <= transform.position.y & transform.position.y <= raypos.y + 0.3f & top == true)//�Ռ��g�����^�C�~���O
        {
            ground = true;
        }
        if(!InArea && !backArea && !Insight)
        {
            this.animator.SetBool(idleStr, true);
            this.animator.SetBool(stopStr, false);
            this.animator.SetBool(stampStr, false);
        }
        if(InArea || backArea || Insight)
        { 
            if (currentState != statebound)
            {
                this.animator.SetBool(stopStr, true);
                this.animator.SetBool(idleStr, false);
            }
        }
    }
    private void ChangeState(Enemy3StateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }
    public void Startcoroutine()//�R���[�`���J�n�֐�
    {
        StartCoroutine(DerayDetect());
    }
    public IEnumerator DerayDetect()//2�b�ԉ������Ȃ��ł��̌㌟�m
    {
        yield return new WaitForSeconds(2);
        breaktime = false;
    }
    public void backStartDelay()//�w��Ō��m�����Ƃ��ɂ��邽�߂̃R���[�`��1�b
    {
        StartCoroutine(backDelay());
    }
    public IEnumerator backDelay()
    {
        yield return new WaitForSeconds(1);
        backbreaktime = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RespawnableStage")
        {
            rb.isKinematic = true;//�ŏ��͏d�̓A���A���̌�transform�ȊO�œ����Ȃ��悤�ɂ���B
        }
        if (collision.gameObject.tag == "Player")//���L�m���G�l�~�[3�̉��ɐ��荞��œ����������̏����i�ǂ��o�����_���[�W�j
        {
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
                Vector3 colPos = new Vector3(target.position.x, 0f, target.position.z);
                Vector3 thisPos = new Vector3(transform.position.x, 0f, transform.position.z);
                Vector3 dir = colPos - thisPos;
                collision.gameObject.GetComponent<Rigidbody>().AddForce(20f * dir, ForceMode.Impulse);
                GameManager.Main.OnDamage(UnderDamage, true);
                if (transform.position.x == collision.gameObject.transform.position.x & transform.position.z == collision.gameObject.transform.position.z)//�Փˈʒu���ǐ^�񒆂��������̏���
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(20f, 0f, 0f, ForceMode.Impulse);
                }
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(collision.gameObject);
        }
    }
    public void Shockwave()//�Ռ��g�̐����֐�
    {
        var shock = Instantiate(shockwaves, transform.position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
        shock.GetComponent<Shockwaves>().damage = ShockDamage;
        shock.SetActive(true);
    }
}
