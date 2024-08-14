using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy2
{
    [SerializeField] float DetectDist = 20;//���ߌ��m����
    [SerializeField] float farDist = 40;//�ő匟�m����
    [SerializeField] public int RoundSphereDamage;
    [SerializeField] public int AttackSphereDamage;
    [SerializeField] public int RushDamage;
    [SerializeField] public float RushknockPower = 3f;//�ːi�U�����̃m�b�N�o�b�N�������
    [SerializeField] public float partKnockPower = 51f;//���ʔj�󎞂̃m�b�N�o�b�N�������
    [SerializeField] public float partKnockUpPower = 15f;//���ʔj�󎞂̃m�b�N�o�b�N�ŏ�ɉ������
    [SerializeField] public float RushUpPower = 5f;//�ːi�U�����̃m�b�N�o�b�N�ŏ�ɉ������
    [SerializeField] public float Rushdist;//�ːi�U���Ői�ދ���
    [SerializeField] public float spherestoptime;//�e�U���p������
    [SerializeField] public float MoveSpeed;//�ːi���̑���
    [SerializeField] public float RotateSpeed = 1.0f;//�������قǐU��������x�������Ȃ�
    [SerializeField] public float movetime;//���L�m�̖ڐ��ɍ��킹��̂ɂ����鎞��
    [SerializeField] public float Roundspheremove;
    [SerializeField] private Collider player;
    [SerializeField] private Collider body;
    [SerializeField] GameObject RushEffect;
    [SerializeField] GameObject HitEffect;
    [SerializeField] public float searchAngle = 45f;
    public Dou_ene_Empty emptyscript;
    public Enemy2sphere spherescript1;
    public Enemy2sphere spherescript2;
    public Enemy2sphere spherescript3;
    public Enemy2sphere spherescript4;
    public Enemy2sphere spherescript5;
    public Enemy2sphere spherescript6;
    public Enemy2sphere spherescript7;
    public Enemy2sphere spherescript8;
    public Gurun subscript1;
    public Gurun subscript2;
    public Gurun subscript3;
    public Gurun subscript4;
    // private GameObject Yukino;
    //YukinoMain YukinoScript;
    [System.NonSerialized] public bool far;//���������Ō��m�����Ƃ��p
    [System.NonSerialized] public bool near;//���ߋ����Ō��m�����Ƃ��p
    [System.NonSerialized] public bool match;//���L�m�̍����ɍ��킹�I��������p
    [System.NonSerialized] public bool breaktime;//�e���U����̖��h�����ԗp
    [System.NonSerialized] public bool longbreaktime;//�ːi�U����̖��h�����ԗp
    [System.NonSerialized] public bool rush;// ��ɓːi����Gurun��Roundsphere��������������RushDamage��^���邽��
    [System.NonSerialized] public bool set;//���������킹�Ă���U�������邽��
    [System.NonSerialized] public bool waitset;//���������킹�Ă���U�������邽��
    [System.NonSerialized] public bool nodamage;
    [System.NonSerialized] public bool Insight; //�����݂̂Ō��m����Ƃ��p
    [System.NonSerialized] public bool look;//�ːi�U�����I����true��
    [System.NonSerialized] public bool backArea;
    [System.NonSerialized] public bool end;//����̋����S�ł����Ƃ��p
    [System.NonSerialized] public bool holdview;//Look()���I���������p
    bool blind;
    [System.NonSerialized] Vector3 postpositon;
    [System.NonSerialized] Vector3 YukinoEyepos;
    //Vector3 StartEyepos; //�������U�����p���L�m�̗��n���̖ڂ̈ʒu�ۊǗp
    public float nodamageTime = 0.5f;
    float elapsedTime = 0f;//�o�ߎ���
    public float elapsedtime;
    public float backtime;
    float rate;
    Vector3 raypos;
    Transform target;
    [System.NonSerialized] public Rigidbody rb;
    public List<GameObject> Others;
    SphereCollider Scol;
    // �X�e�[�g�̃C���X�^���X
    private static readonly StateWait statewait = new StateWait();
    private static readonly Statenear statenear = new Statenear();
    private static readonly StateSphereattack statesphereattack = new StateSphereattack();
    private static readonly StateDetect2 statedetect = new StateDetect2();
    private static readonly StateRush staterush = new StateRush();


    /// <summary>
    /// ���݂̃X�e�[�g
    /// </summary>
    private Enemy2StateBase currentState = statewait;

    // Start()����Ă΂��
    private void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        Scol = GetComponent<SphereCollider>();
        currentState.OnEnter(this, null);
        target = GameManager.Main.transform;
        elapsedtime = 0f;
        rush = false;
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20.0f))
        {
            raypos = hit.point;
        }
    }

    // Update()����Ă΂��
    private void OnUpdate()
    {
        currentState.OnUpdate(this);
        Vector3 Pos = transform.position;
        if (!end)
        { transform.position = new Vector3(Pos.x, Mathf.Clamp(Pos.y, raypos.y + 2.0f, raypos.y + 4.0f), Pos.z); }//�ړ�����
        if (transform.position.y < raypos.y - 50f)//�������Ă������p�̏���
        { Destroy(gameObject); }
        if (GameManager.Main.index == 1)
        { YukinoEyepos = GameManager.Main.transform.position + new Vector3(0f, 0.8789997f, 0f); }
        if (GameManager.Main.index == 0)
        { YukinoEyepos = GameManager.Main.transform.position; }
        if (rush || end)//�ːi����rb.velocity�Ǝ��肪�����Ȃ������ɗ���������p
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
        if (nodamage == true)
        {
            nodamageTime -= Time.deltaTime;
            if (nodamageTime <= 0.0f)
            {
                nodamage = false;
            }
        }
        if (look == true & currentState == statewait)
        {
            waitDetect();//���������̌��m
        }
        Scol.isTrigger = false;
        rb.useGravity = true;//����̋��������Ȃ�����������悤��
        end = true;
        for (int i = 0; i < Others.Count; i++)
        {
            if (Others[i] != null)
            {
                Scol.isTrigger = true;
                rb.useGravity = false;
                end = false;
            }
        }
        if (end == true)
        {
            ChangeState(statewait);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        if (elapsedtime >= RotateSpeed & currentState != staterush)
        {
            if (blind == false)
            {
                holdview = true;
                HoldLook();
            }
        }
        if (blind & !near)//���m�O�ɍs�����Ƃ�
        {
            elapsedtime = 0f;
        }
        if (!far & !Insight)//���m�O����n�܂邩��Ablind��true�ŃX�^�[�g����
        { blind = true; }
        else
        { blind = false; }
    }

    private void OnFixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }

    private void ChangeState(Enemy2StateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && nodamage == false)
        {
            if (GameManager.Main.index == 1 || GameManager.Main.Dush)
            {
                GameObject hitef = Instantiate(HitEffect, gameObject.transform.position, Quaternion.identity);
                hitef.transform.localScale = Vector3.one * 1.5f;
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && nodamage == false && end && GameManager.Main.index == 1)
        {
            Instantiate(HitEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void Detect()//���m�֐��ifar,near,backArea�̏ꍇ��bool)
    {
        float detectDistance = Mathf.Pow(DetectDist, 2);//�������גጸ�isqrMagnitude)�K�p�ς�
        float FarDistance = Mathf.Pow(farDist, 2);
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = GameManager.Main.transform.position;
        Vector3 Direction = Bpos - Apos;
        float distance = Direction.sqrMagnitude;
        float targetAngle = Vector3.Angle(Direction, transform.forward);
        if (detectDistance < distance & distance <= FarDistance & searchAngle > targetAngle)
        {
            far = true;
        }
        else if (distance <= detectDistance & searchAngle > targetAngle)
        {
            near = true;
        }
        else if (distance < detectDistance & targetAngle >= searchAngle)//�w��̌��m
        {
            backArea = true;
        }
        else
        {
            far = false;
            near = false;
            backArea = false;
        }
    }
    public void waitDetect()//�����������ȊO�ڂł͒ǂ�������A�����̏����̂�
    {
        float dis = Mathf.Pow(farDist, 2);
        Vector3 apos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 bpos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 measure = bpos - apos;
        float measuredist = measure.sqrMagnitude;
        if (measuredist <= dis)
        {
            Insight = true;
        }
        else
        {
            Insight = false;
        }
    }
    public void Look()//�U����������������i���m�ŋC�Â������j
    {
        Vector3 sightPos = new Vector3(target.position.x, transform.position.y, target.position.z);//�������Ȃ��悤��y�̂݃G�l�~�[1�̍���
        Quaternion targetRotation = Quaternion.LookRotation(sightPos - transform.position);
        elapsedtime += Time.deltaTime;
        rate = Mathf.Clamp01(elapsedtime / RotateSpeed);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rate);
    }
    public void HoldLook()//���Ԃ����Ȃ��Ń��L�m�Ɍ����i��{�I��Look()���I�������Ɏg���j
    {
        if (holdview)
        {
            Vector3 sightPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(sightPos);
        }
    }
    public void Startshortcoroutine()//StateWait()�ŌĂׂ�悤�ɂ��邽�߂̃R���[�`���J�n�֐�
    {
        StartCoroutine(DerayDetect());
    }
    public IEnumerator DerayDetect()//�U��������O�b�ԉ����������̌㌟�m
    {
        yield return new WaitForSeconds(3);
        breaktime = false;
    }
    public void matchheight()//Enemy2�̍��������L�m�̎��_�ɍ��킹��
    {
        if (set == false)
        {
            Vector3 pos = transform.position;
            elapsedTime += Time.deltaTime;
            rate = Mathf.Clamp01(elapsedTime / movetime);
            postpositon = new Vector3(pos.x, YukinoEyepos.y + 1.0f, pos.z);
            transform.position = Vector3.Lerp(pos, postpositon, rate);
        }
        if (elapsedTime >= movetime)
        {
            set = true;//�ړ����������Ă���ߐڍU���Ɉڂ��悤�ɂ���
        }
    }
    public void matchwaitheight()//�U����̉������Ȃ����Ԃɑ؋󂳂��鏈��
    {
        Vector3 pos = transform.position;
        backtime += Time.deltaTime;
        rate = Mathf.Clamp01(backtime / movetime);
        postpositon = new Vector3(pos.x, raypos.y + 4.0f, pos.z);
        transform.position = Vector3.Lerp(pos, postpositon, rate);
        if (backtime >= movetime)
        {
            waitset = true;
        }
    }
}
