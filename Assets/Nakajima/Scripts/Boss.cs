using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossState;

public class Boss : MonoBehaviour
{
    public BossStateProcessor StateProcessor { get; set; } = new BossStateProcessor();
    public BossStateIdle StateIdle { get; set; } = new BossStateIdle();
    public BossStateMove StateMove { get; set; } = new BossStateMove();
    public BossStateAttack1 StateAttack1 { get; set; } = new BossStateAttack1();
    public BossStateAttack2 StateAttack2 { get; set; } = new BossStateAttack2();
    public BossStateAttack3 StateAttack3 { get; set; } = new BossStateAttack3();
    public BossStateAttack4 StateAttack4 { get; set; } = new BossStateAttack4();
    public BossStateAttack1Idle StateAttack1Idle { get; set; } = new BossStateAttack1Idle();
    public BossStateAttack3Idle StateAttack3Idle { get; set; } = new BossStateAttack3Idle();
    public BossStateAttack4Idle StateAttack4Idle { get; set; } = new BossStateAttack4Idle();
    public BossStateAttack5 StateAttack5 { get; set; } = new BossStateAttack5();
    public BossStateAttack5Idle StateAttack5Idle { get; set; } = new BossStateAttack5Idle();
    public BossStateChangeFloor StateChangeFloor { get; set; } = new BossStateChangeFloor();

    private string _preStateName;
    
    [SerializeField]
    public GameObject PlayerObject;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject Attack3_1;
    [SerializeField]
    private GameObject Attack3_2;
    [SerializeField]
    private GameObject Attack3_3;
    [SerializeField]
    private GameObject Attack3_4;
    [SerializeField]
    private GameObject WingParent;
    [SerializeField]
    private GameObject Floor;
    [SerializeField]
    private GameObject ShockWave;
    [SerializeField]
    private ParticleSystem Attack4BeamParticle;
    
    private GameObject Floor_1;
    private GameObject Floor_2;
    private GameObject Floor_3;
    private GameObject Floor_4;
    private GameObject Floor_5;
    private GameObject Floor_6;

    private int FloorFlagNumber = -1;
    public int CheckPhaseNumber;
    private int PhaseNumber;
    private int FloorNumber;
    public float WingTimer = 0.0f;

    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private float ChangeStateDistance;

    [SerializeField]
    private float StateChangeTime;
    private float InitStateChangeTime;

    [SerializeField]
    private float Attack1Time; //Attack1の前隙
    
    [SerializeField]
    private int Magazine;

    private int InitMagazine;
    
    [SerializeField]
    private float ReloadTime; //

    private float InitReloadTime;

    [SerializeField]
    private float Attack3Time; //Attack3の前隙

    [SerializeField]
    private int Attack3Count; //飛ばす回数

    private int CurrentAttack3Count;

    [SerializeField]
    private float Attack3DelayTime;

    private float InitAttack3DelayTime;

    [SerializeField]
    private float Attack3Speed;
    
    private float Attack3Distance = 10.0f;

    [SerializeField]
    private float Attack3RiseMagnification;

    

    private Vector3 Attack3Position;

    private Vector3 InitAttack3Pos1;
    private Vector3 InitAttack3Pos2;
    private Vector3 InitAttack3Pos3;
    private Vector3 InitAttack3Pos4;

    private Vector3 InitLocalAttack3Pos1;
    private Vector3 InitLocalAttack3Pos2;
    private Vector3 InitLocalAttack3Pos3;
    private Vector3 InitLocalAttack3Pos4;

    private Quaternion InitAttack3Ang1;
    private Quaternion InitAttack3Ang2;
    private Quaternion InitAttack3Ang3;
    private Quaternion InitAttack3Ang4;

    [SerializeField]
    private float InitAttack3PosY;

    private bool isInit = false;
    private bool isAttack3_1;
    private bool isAttack3_2;
    private bool isAttack3_3;
    private bool isAttack3_4;

    private BossWing BW_1;
    private BossWing BW_2;
    private BossWing BW_3;
    private BossWing BW_4;

    private bool isNear = false;
    private bool isNear2 = false;

    [SerializeField]
    private float Attack3RunUp;

    public float Attack3RunUpCountTime;

    private Vector3 Attack3SavePos;


    [SerializeField]
    private float Attack3RisePos;

    private float Attack3RiseTime;


    [SerializeField]
    private float Attack4Time; //判定持続

    [SerializeField]
    private float Attack4DurationTime; //判定持続

    private float InitAttack4DurationTime;

    [SerializeField]
    private float Attack4CoolTime; //クールタイム

    private float InitAttack4CoolTime;
    
    [SerializeField]
    private float Lookspeed = 0.1f;

    [SerializeField]
    private float Attack4Lookspeed = 0.1f;

    private float CountTime;
    private bool isCenter = false;
    private float DistancePosition = 0.5f;
    private BossHPController BHC;
    private Vector3 InitFloorPos;
    private bool isNearPlayer = false;
    private bool isShockWaveGenerate = false;

    //市川が書きました↓
    [SerializeField] private float Attack2stoptime;
    private float Attack2time;
    private float At2count;
    [SerializeField] private GameObject attacksphere;
    [SerializeField] private float SphereSpeed;
    [SerializeField] private float At2RollSpeed;

    [SerializeField] private float TimeAdd;

    private bool isPhase2Beam;
    private bool isPhase3Beam;



    // Start is called before the first frame update
    void Start()
    {
        StateIdle.ExecAction = Idle;
        StateMove.ExecAction = Move;
        StateIdle.ExecAction = Idle;
        StateAttack1.ExecAction = Attack1;
        StateAttack2.ExecAction = Attack2;
        StateAttack3.ExecAction = Attack3;
        StateAttack4.ExecAction = Attack4;
        StateAttack5.ExecAction = Attack5;
        StateAttack1Idle.ExecAction = Attack1Idle;
        StateAttack3Idle.ExecAction = Attack3Idle;
        StateAttack4Idle.ExecAction = Attack4Idle;
        StateAttack5Idle.ExecAction = Attack5Idle;
        
        StateChangeFloor.ExecAction = ChangeFloor;
        
        StateProcessor.State = StateMove;

        InitReloadTime = ReloadTime;
        InitMagazine = Magazine;
        InitAttack3DelayTime = Attack3DelayTime;
        InitAttack4DurationTime = Attack4DurationTime;
        InitAttack4CoolTime = Attack4CoolTime;

        Floor_1 = Floor.transform.Find("BossPosition_1").gameObject;
        Floor_2 = Floor.transform.Find("BossPosition_2").gameObject;
        Floor_3 = Floor.transform.Find("BossPosition_3").gameObject;
        Floor_4 = Floor.transform.Find("BossPosition_4").gameObject;
        Floor_5 = Floor.transform.Find("BossPosition_5").gameObject;
        Floor_6 = Floor.transform.Find("BossPosition_6").gameObject;

        InitAttack3Ang1 = Quaternion.Euler(Attack3_1.transform.localEulerAngles);
        InitAttack3Ang2 = Quaternion.Euler(Attack3_2.transform.localEulerAngles);
        InitAttack3Ang3 = Quaternion.Euler(Attack3_3.transform.localEulerAngles);
        InitAttack3Ang4 = Quaternion.Euler(Attack3_4.transform.localEulerAngles);

        BW_1 = Attack3_1.GetComponent<BossWing>();
        BW_2 = Attack3_2.GetComponent<BossWing>();
        BW_3 = Attack3_3.GetComponent<BossWing>();
        BW_4 = Attack3_4.GetComponent<BossWing>();

        InitLocalAttack3Pos1 = Attack3_1.transform.localPosition;
        InitLocalAttack3Pos2 = Attack3_2.transform.localPosition;
        InitLocalAttack3Pos3 = Attack3_3.transform.localPosition;
        InitLocalAttack3Pos4 = Attack3_4.transform.localPosition;

        BHC = GetComponent<BossHPController>();
        InitFloorPos = Floor.transform.position;
        InitStateChangeTime = StateChangeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.timeScale != 0)
        {
        CheckPhaseNumber = PhaseNumber;
        /*// Debug
        if(Input.GetKey(KeyCode.H))
        {
            StateProcessor.State = StateAttack1;
            Debug.Log(StateProcessor.State.GetStateName());
        }

        if(Input.GetKey(KeyCode.J))
        {
            StateProcessor.State = StateAttack2;
            Debug.Log(StateProcessor.State.GetStateName());
        }

        if(Input.GetKey(KeyCode.K))
        {
            StateProcessor.State = StateAttack3;
            Debug.Log(StateProcessor.State.GetStateName());
        }

        if(Input.GetKey(KeyCode.L))
        {
            StateProcessor.State = StateAttack4;
            Debug.Log(StateProcessor.State.GetStateName());
        }*/
        


        // State Change Timer
        if(StateProcessor.State.GetStateName() == "State:Move")
        {
            StateChangeTime -= Time.deltaTime*TimeAdd;
            Vector3 relativePos = PlayerObject.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation (relativePos);
            transform.rotation  = Quaternion.Slerp (this.transform.rotation, rotation, Time.deltaTime*TimeAdd*Lookspeed);
        }
        else
        {
            StateChangeTime = InitStateChangeTime;
        }

        //Boss Down
        if((160 < BHC.HP && BHC.HP <= 180) || (80 <= BHC.HP && BHC.HP <= 100) || (0 <= BHC.HP && BHC.HP <= 20))
        {
            StateProcessor.State = StateIdle;
            ResetBossValue();
        }
        else if(StateProcessor.State.GetStateName() == "State:Idle")
        {
            StateProcessor.State = StateMove;
        }

        //Boss Down Move
        if(StateProcessor.State.GetStateName() == "State:Idle") 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 0f, 0f), MoveSpeed*Time.deltaTime*TimeAdd*1.5f);
            FloorNumber = 4;
        }

        //HP & Floor
        if(StateProcessor.State.GetStateName() == "State:Move" || StateProcessor.State.GetStateName() == "State:Attack1" || StateProcessor.State.GetStateName() == "State:Attack2") //移動
        {
            //Move Animation(Idle)
            Debug.Log("now state : " + StateProcessor.State.GetStateName());
            if((220 < BHC.HP && BHC.HP <= 240) || (140 < BHC.HP && BHC.HP <= 160) || (60 < BHC.HP && BHC.HP <= 80)) //1F
            {
                Floor.transform.position = InitFloorPos;
                if((220 < BHC.HP && BHC.HP <= 240))
                {
                    PhaseNumber = 1;
                }
                else if((140 < BHC.HP && BHC.HP <= 160))
                {
                    PhaseNumber = 2;
                }
                else if((60 < BHC.HP && BHC.HP <= 80))
                {
                    PhaseNumber = 3;
                }
                FloorNumber = 1;
            }
            else if((200 < BHC.HP && BHC.HP <= 220) || (120 < BHC.HP && BHC.HP <= 140)|| (40 < BHC.HP && BHC.HP <= 60)) //2F
            {
                Floor.transform.position = InitFloorPos + new Vector3(0f, 40f, 0f);
                if((200 < BHC.HP && BHC.HP <= 220))
                {
                    PhaseNumber = 1;
                }
                else if((120 < BHC.HP && BHC.HP <= 140))
                {
                    PhaseNumber = 2;
                }
                else if((40 < BHC.HP && BHC.HP <= 60))
                {
                    PhaseNumber = 3;
                }
                FloorNumber = 2;
            }
            else if((180 < BHC.HP && BHC.HP <= 200) || (100 < BHC.HP && BHC.HP <= 120)|| (20 < BHC.HP && BHC.HP <= 40)) //3F
            {
                Floor.transform.position = InitFloorPos + new Vector3(0f, 80f, 0f);
                if((180 < BHC.HP && BHC.HP <= 200))
                {
                    PhaseNumber = 1;
                }
                else if((100 < BHC.HP && BHC.HP <= 120))
                {
                    PhaseNumber = 2;
                }
                else if((20 < BHC.HP && BHC.HP <= 40))
                {
                    PhaseNumber = 3;
                }
                FloorNumber = 3;
            }

            if(FloorFlagNumber < 0 || FloorFlagNumber > 4)
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor_1.transform.position, MoveSpeed*Time.deltaTime*TimeAdd*0.6f);
            }
            if(Vector3.Distance(transform.position, Floor_1.transform.position) <= DistancePosition && (FloorFlagNumber < 0 || FloorFlagNumber > 4))
            {
                FloorFlagNumber = 0;
            }

            if(FloorFlagNumber == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor_2.transform.position, MoveSpeed*Time.deltaTime*TimeAdd*0.6f);
            }
            if(Vector3.Distance(transform.position, Floor_2.transform.position) <= DistancePosition && FloorFlagNumber == 0)
            {
                FloorFlagNumber = 1;
            }

            if(FloorFlagNumber == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor_3.transform.position, MoveSpeed*Time.deltaTime*TimeAdd*0.6f);
            }
            if(Vector3.Distance(transform.position, Floor_3.transform.position) <= DistancePosition && FloorFlagNumber == 1)
            {
                FloorFlagNumber = 2;
            }

            if(FloorFlagNumber == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor_4.transform.position, MoveSpeed*Time.deltaTime*TimeAdd*0.6f);
            }
            if(Vector3.Distance(transform.position, Floor_4.transform.position) <= DistancePosition && FloorFlagNumber == 2)
            {
                FloorFlagNumber = 3;
            }

            if(FloorFlagNumber == 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor_5.transform.position, MoveSpeed*Time.deltaTime*TimeAdd*0.6f);
            }
            if(Vector3.Distance(transform.position, Floor_5.transform.position) <= DistancePosition && FloorFlagNumber == 3)
            {
                FloorFlagNumber = 4;
            }

            if(FloorFlagNumber == 4)
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor_6.transform.position, MoveSpeed*Time.deltaTime*TimeAdd*0.6f);
            }
            if(Vector3.Distance(transform.position, Floor_6.transform.position) <= DistancePosition && FloorFlagNumber == 4)
            {
                FloorFlagNumber = 5;
            }
        }

        //State Change
        if(isNearPlayer) 
        {
            if(StateProcessor.State.GetStateName() == "State:Move" && StateChangeTime <= 0.0f)
            {
                Debug.Log("Boss State Change");
                RandomStateChanege(PhaseNumber); //random attack
                isNearPlayer = false;
            }
        }

        //State Name Save
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }

        //Attack1
        if(StateProcessor.State.GetStateName() == "State:Attack1")
        {
            CountTime += Time.deltaTime;
            if(CountTime <= Attack1Time)
            {
                if(Vector3.Distance(PlayerObject.transform.position, transform.position) <= 50.0f)
                {
                    
                }
                else
                {
                    Vector3 relativePos = PlayerObject.transform.position - this.transform.position;
                    Quaternion rotation = Quaternion.LookRotation (relativePos);
                    transform.rotation  = Quaternion.Slerp (this.transform.rotation, rotation, Lookspeed);
                }
            }
            else
            {
                if(Vector3.Distance(PlayerObject.transform.position, transform.position) <= 50.0f)
                {
                    
                }
                else
                {
                    Vector3 relativePos = PlayerObject.transform.position - this.transform.position;
                    Quaternion rotation = Quaternion.LookRotation (relativePos);
                    transform.rotation  = Quaternion.Slerp (this.transform.rotation, rotation, Lookspeed);
                }

                BossAttack1();
            }
        }

        //Attack2
        if(StateProcessor.State.GetStateName() == "State:Attack2") //回転
        {
            BossAttack2();
            if (Attack2time > Attack2stoptime)
            {
                Attack2time = 0.0f;
                StateProcessor.State = StateMove;
            }      
        }

        //Attack3
        if(StateProcessor.State.GetStateName() == "State:Attack3") //突き刺し
        {
            if(CurrentAttack3Count != 4)
            {
                if(PlayerObject.transform.position.y >= transform.position.y)
                {
                    transform.LookAt(PlayerObject.transform.position);
                    transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
                }
                else
                {
                    transform.LookAt(PlayerObject.transform.position);
                }
            }
            if(isCenter)
            {    
                if(!isInit)
                {
                    Debug.Log("Boss Posi " + Attack3_1.transform.position);
                    InitAttack3Pos1 = Attack3_1.transform.position;
                    InitAttack3Pos2 = Attack3_2.transform.position;
                    InitAttack3Pos3 = Attack3_3.transform.position;
                    InitAttack3Pos4 = Attack3_4.transform.position;
                    isInit = true;
                }
                CountTime += Time.deltaTime;
                
                if(CountTime >= Attack3Time && CurrentAttack3Count != 4)
                {
                    BossAttack3();
                    float PosAddX = Attack3RiseTime;
                    float PosAddY = -12f + Attack3RiseTime / 1.5f * 3.0f;
                    float PosAddZ = -11f + Attack3RiseTime / 1.5f * 3.0f;

                    float PosAddX2 = -9f + Attack3RiseTime;
                    float PosAddY2 = -4f + Attack3RiseTime / 1.5f * 3.0f;
                    float PosAddZ2 = Attack3RiseTime / 1.5f * 3.0f;
                    if(!isAttack3_1 && StateProcessor.State.GetStateName() == "State:Attack3" && CurrentAttack3Count == 0 && Attack3RunUpCountTime >= Attack3RunUp && !isNear2)
                    {
                        Attack3_1.transform.LookAt(Attack3Position);
                        Attack3_1.transform.localEulerAngles = new Vector3(Attack3_1.transform.localEulerAngles.x - PosAddX2, Attack3_1.transform.localEulerAngles.y + PosAddY2, Attack3_1.transform.localEulerAngles.z + PosAddZ2);
                    }
                    else if(!isAttack3_1 && StateProcessor.State.GetStateName() == "State:Attack3" && !isNear2)
                    {
                        Attack3_1.transform.LookAt(PlayerObject.transform.position);
                        Attack3_1.transform.localEulerAngles = new Vector3(Attack3_1.transform.localEulerAngles.x - PosAddX2, Attack3_1.transform.localEulerAngles.y + PosAddY2, Attack3_1.transform.localEulerAngles.z + PosAddZ2);
                    }

                    if(!isAttack3_2 && StateProcessor.State.GetStateName() == "State:Attack3" && CurrentAttack3Count == 1 && Attack3RunUpCountTime >= Attack3RunUp && !isNear2)
                    {
                        Attack3_2.transform.LookAt(Attack3Position);
                        Attack3_2.transform.localEulerAngles = new Vector3(Attack3_2.transform.localEulerAngles.x - PosAddX, Attack3_2.transform.localEulerAngles.y + PosAddY, Attack3_2.transform.localEulerAngles.z + PosAddZ);
                    }
                    else if(Attack3Count >= 1 && !isAttack3_2 && StateProcessor.State.GetStateName() == "State:Attack3" && !isNear2)
                    {
                        Attack3_2.transform.LookAt(PlayerObject.transform.position);
                        Attack3_2.transform.localEulerAngles = new Vector3(Attack3_2.transform.localEulerAngles.x - PosAddX, Attack3_2.transform.localEulerAngles.y + PosAddY, Attack3_2.transform.localEulerAngles.z + PosAddZ);
                    }

                    if(!isAttack3_3 && StateProcessor.State.GetStateName() == "State:Attack3" && CurrentAttack3Count == 2 && Attack3RunUpCountTime >= Attack3RunUp && !isNear2)
                    {
                        Attack3_3.transform.LookAt(Attack3Position);
                        Attack3_3.transform.localEulerAngles = new Vector3(Attack3_3.transform.localEulerAngles.x - PosAddX, Attack3_3.transform.localEulerAngles.y + PosAddY, Attack3_3.transform.localEulerAngles.z + PosAddZ);
                    }
                    else if(Attack3Count >= 2 && !isAttack3_3 && StateProcessor.State.GetStateName() == "State:Attack3" && !isNear2)
                    {
                        Attack3_3.transform.LookAt(PlayerObject.transform.position);
                        Attack3_3.transform.localEulerAngles = new Vector3(Attack3_3.transform.localEulerAngles.x - PosAddX, Attack3_3.transform.localEulerAngles.y + PosAddY, Attack3_3.transform.localEulerAngles.z + PosAddZ);
                    }

                    if(!isAttack3_4 && StateProcessor.State.GetStateName() == "State:Attack3" && CurrentAttack3Count == 3 && Attack3RunUpCountTime >= Attack3RunUp && !isNear2)
                    {
                        Attack3_4.transform.LookAt(Attack3Position);
                        Attack3_4.transform.localEulerAngles = new Vector3(Attack3_4.transform.localEulerAngles.x - PosAddX2, Attack3_4.transform.localEulerAngles.y + PosAddY2, Attack3_4.transform.localEulerAngles.z + PosAddZ2);
                    }
                    else if(Attack3Count >= 3 && !isAttack3_4 && StateProcessor.State.GetStateName() == "State:Attack3" && !isNear2)
                    {
                        Attack3_4.transform.LookAt(PlayerObject.transform.position);
                        Attack3_4.transform.localEulerAngles = new Vector3(Attack3_4.transform.localEulerAngles.x - PosAddX2, Attack3_4.transform.localEulerAngles.y + PosAddY2, Attack3_4.transform.localEulerAngles.z + PosAddZ2);
                    }
                }
                else
                {
                    
                    if(CurrentAttack3Count != 4)
                    {
                        if(Attack3RiseTime <= Attack3RisePos)
                        {
                            Attack3RiseTime += Time.deltaTime * 1.5f;
                            //Debug.Log("時間計測中:" + Attack3RiseTime);
                        }
                        if(Attack3RiseTime <= Attack3RisePos)
                        {
                            float PosAddX = Attack3RiseTime;
                            float PosAddY = -12f + Attack3RiseTime / 1.5f * 3.0f;
                            float PosAddZ = -11f + Attack3RiseTime / 1.5f * 3.0f;

                            float PosAddX2 = -9f + Attack3RiseTime;
                            float PosAddY2 = -4f + Attack3RiseTime / 1.5f * 3.0f;
                            float PosAddZ2 = Attack3RiseTime / 1.5f * 3.0f;

                            float RotateTime = Attack3RiseTime;
                            if(RotateTime >= 1.0f){RotateTime = 1.0f;}

                            if(!isAttack3_1 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_1.transform.position = new Vector3(Attack3_1.transform.position.x, InitAttack3Pos1.y + Attack3RiseTime*1.5f, Attack3_1.transform.position.z);
                                //Attack3_1.transform.localPosition = new Vector3(Attack3_1.transform.localPosition.x + RotateTime/30f, Attack3_1.transform.localPosition.y, Attack3_1.transform.localPosition.z);
                                Attack3_1.transform.LookAt(Vector3.Lerp(transform.forward + transform.position, PlayerObject.transform.position, RotateTime));
                                Attack3_1.transform.localEulerAngles = new Vector3(Attack3_1.transform.localEulerAngles.x - PosAddX2*RotateTime, Attack3_1.transform.localEulerAngles.y + PosAddY2, Attack3_1.transform.localEulerAngles.z + PosAddZ2);
                            }

                            if(Attack3Count >= 1 && !isAttack3_2 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_2.transform.position = new Vector3(Attack3_2.transform.position.x, InitAttack3Pos2.y + Attack3RiseTime*1.5f, Attack3_2.transform.position.z);
                                //Attack3_2.transform.localPosition = new Vector3(Attack3_2.transform.localPosition.x + RotateTime/30f, Attack3_2.transform.localPosition.y, Attack3_2.transform.localPosition.z);
                                Attack3_2.transform.LookAt(Vector3.Lerp(transform.forward + transform.position, PlayerObject.transform.position, RotateTime));
                                Attack3_2.transform.localEulerAngles = new Vector3(Attack3_2.transform.localEulerAngles.x - PosAddX*RotateTime, Attack3_2.transform.localEulerAngles.y + PosAddY, Attack3_2.transform.localEulerAngles.z + PosAddZ);
                            }

                            if(Attack3Count >= 2 && !isAttack3_3 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_3.transform.position = new Vector3(Attack3_3.transform.position.x, InitAttack3Pos3.y + Attack3RiseTime*7.0f, Attack3_3.transform.position.z);
                                Attack3_3.transform.LookAt(Vector3.Lerp(transform.forward + transform.position, PlayerObject.transform.position, RotateTime));
                                Attack3_3.transform.localEulerAngles = new Vector3(Attack3_3.transform.localEulerAngles.x - PosAddX*RotateTime, Attack3_3.transform.localEulerAngles.y + PosAddY, Attack3_3.transform.localEulerAngles.z + PosAddZ);
                            }
                            
                            if(Attack3Count >= 3 && !isAttack3_4 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_4.transform.position = new Vector3(Attack3_4.transform.position.x, InitAttack3Pos4.y + Attack3RiseTime*7.0f, Attack3_4.transform.position.z);
                                Attack3_4.transform.LookAt(Vector3.Lerp(transform.forward + transform.position, PlayerObject.transform.position, RotateTime));
                                Attack3_4.transform.localEulerAngles = new Vector3(Attack3_4.transform.localEulerAngles.x - PosAddX2*RotateTime, Attack3_4.transform.localEulerAngles.y + PosAddY2, Attack3_4.transform.localEulerAngles.z + PosAddZ2);
                            }
                        }
                        else
                        {
                            float PosAddX = Attack3RiseTime;
                            float PosAddY = -12f + Attack3RiseTime / 1.5f * 3.0f;
                            float PosAddZ = -11f + Attack3RiseTime / 1.5f * 3.0f;

                            float PosAddX2 = -9f + Attack3RiseTime;
                            float PosAddY2 = -4f + Attack3RiseTime / 1.5f * 3.0f;
                            float PosAddZ2 = Attack3RiseTime / 1.5f * 3.0f;

                            if(!isAttack3_1 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_1.transform.position = new Vector3(Attack3_1.transform.position.x, InitAttack3Pos1.y + Attack3RiseTime*1.5f, Attack3_1.transform.position.z);
                                Attack3_1.transform.LookAt(PlayerObject.transform.position);
                                Attack3_1.transform.localEulerAngles = new Vector3(Attack3_1.transform.localEulerAngles.x - PosAddX2, Attack3_1.transform.localEulerAngles.y + PosAddY2, Attack3_1.transform.localEulerAngles.z + PosAddZ2);
                            }

                            if(Attack3Count >= 1 && !isAttack3_2 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_2.transform.position = new Vector3(Attack3_2.transform.position.x, InitAttack3Pos2.y + Attack3RiseTime*1.5f, Attack3_2.transform.position.z);
                                Attack3_2.transform.LookAt(PlayerObject.transform.position);
                                Attack3_2.transform.localEulerAngles = new Vector3(Attack3_2.transform.localEulerAngles.x - PosAddX, Attack3_2.transform.localEulerAngles.y + PosAddY, Attack3_2.transform.localEulerAngles.z + PosAddZ);
                            }

                            if(Attack3Count >= 2 && !isAttack3_3 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_3.transform.position = new Vector3(Attack3_3.transform.position.x, InitAttack3Pos3.y + Attack3RiseTime*7.0f, Attack3_3.transform.position.z);
                                Attack3_3.transform.LookAt(PlayerObject.transform.position);
                                Attack3_3.transform.localEulerAngles = new Vector3(Attack3_3.transform.localEulerAngles.x - PosAddX, Attack3_3.transform.localEulerAngles.y + PosAddY, Attack3_3.transform.localEulerAngles.z + PosAddZ);
                            }

                            if(Attack3Count >= 3 && !isAttack3_4 && StateProcessor.State.GetStateName() == "State:Attack3")
                            {
                                Attack3_4.transform.position = new Vector3(Attack3_4.transform.position.x, InitAttack3Pos4.y + Attack3RiseTime*7.0f, Attack3_4.transform.position.z);
                                Attack3_4.transform.LookAt(PlayerObject.transform.position);
                                Attack3_4.transform.localEulerAngles = new Vector3(Attack3_4.transform.localEulerAngles.x - PosAddX2, Attack3_4.transform.localEulerAngles.y + PosAddY2, Attack3_4.transform.localEulerAngles.z + PosAddZ2);
                            }    
                        }
                    }
                        
                }
            }
            else
            {
                Vector3 TopPos = new Vector3(Floor.transform.position.x, Floor.transform.position.y + 40f, Floor.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, TopPos, MoveSpeed*Time.deltaTime*TimeAdd);
                if(Vector3.Distance(transform.position, TopPos) <= DistancePosition)
                {
                    isCenter = true;
                }
            }
        }

        
        //羽を初期位置に
        //Vector3 velocity = (InitAttack3Pos1 - Attack3_1.transform.position) * 0.5f;
        //Attack3_1.transform.position += velocity *= Time.deltaTime;

        if(StateProcessor.State.GetStateName() == "State:Attack3" && CurrentAttack3Count == 4)
        {
            
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            float dis = 0.01f;
            float spead  = 4.0f;

            if(Vector3.Distance(Attack3_1.transform.localPosition, InitLocalAttack3Pos1) >= dis)
            {
               Vector3 velocity = (InitLocalAttack3Pos1 - Attack3_1.transform.localPosition) * spead;
               Attack3_1.transform.localPosition += velocity *= Time.deltaTime;
            }
            else
            {
                Attack3_1.transform.localPosition = InitLocalAttack3Pos1;
                flag1 = true;
            }
            if(Vector3.Distance(Attack3_1.transform.localEulerAngles, InitAttack3Ang1.eulerAngles) >= dis)
            {
                Attack3_1.transform.localEulerAngles = InitAttack3Ang1.eulerAngles;
            }
            else
            {
                Attack3_1.transform.localEulerAngles = InitAttack3Ang1.eulerAngles;
            }


            if(Vector3.Distance(Attack3_2.transform.localPosition, InitLocalAttack3Pos2) >= dis)
            {
               Vector3 velocity = (InitLocalAttack3Pos2 - Attack3_2.transform.localPosition) * spead;
               Attack3_2.transform.localPosition += velocity *= Time.deltaTime;
            }
            else
            {
                Attack3_2.transform.localPosition = InitLocalAttack3Pos2;
                flag2 = true;
            }
            if(Vector3.Distance(Attack3_2.transform.localEulerAngles, InitAttack3Ang2.eulerAngles) >= dis)
            {
                //Attack3_2.transform.localEulerAngles = (Quaternion.RotateTowards(Attack3_2.transform.rotation, InitAttack3Ang2, 2f*Time.deltaTime)).eulerAngles;
                Attack3_2.transform.localEulerAngles = InitAttack3Ang2.eulerAngles;
            }
            else
            {
                Attack3_2.transform.localEulerAngles = InitAttack3Ang2.eulerAngles;
            }


            if(Vector3.Distance(Attack3_3.transform.localPosition, InitLocalAttack3Pos3) >= dis)
            {
               Vector3 velocity = (InitLocalAttack3Pos3 - Attack3_3.transform.localPosition) * spead;
               Attack3_3.transform.localPosition += velocity *= Time.deltaTime;
            }
            else
            {
                Attack3_3.transform.localPosition = InitLocalAttack3Pos3;
                flag3 = true;
            }
            if(Vector3.Distance(Attack3_3.transform.localEulerAngles, InitAttack3Ang3.eulerAngles) >= dis)
            {
               //Attack3_3.transform.localEulerAngles = (Quaternion.RotateTowards(Attack3_3.transform.rotation, InitAttack3Ang3, 2f*Time.deltaTime)).eulerAngles;
                Attack3_3.transform.localEulerAngles = InitAttack3Ang3.eulerAngles;
            }
            else
            {
                Attack3_3.transform.localEulerAngles = InitAttack3Ang3.eulerAngles;
            }


            if(Vector3.Distance(Attack3_4.transform.localPosition, InitLocalAttack3Pos4) >= dis)
            {
               Vector3 velocity = (InitLocalAttack3Pos4 - Attack3_4.transform.localPosition) * spead;
               Attack3_4.transform.localPosition += velocity *= Time.deltaTime;
            }
            else
            {

                Attack3_4.transform.localPosition = InitLocalAttack3Pos4;
                flag4 = true;
            }
            if(Vector3.Distance(Attack3_4.transform.localEulerAngles, InitAttack3Ang4.eulerAngles) >= dis)
            {
               //Attack3_4.transform.localEulerAngles = (Quaternion.RotateTowards(Attack3_4.transform.rotation, InitAttack3Ang4, 2f*Time.deltaTime)).eulerAngles;
                Attack3_4.transform.localEulerAngles = InitAttack3Ang4.eulerAngles;
            }
            else
            {
                Attack3_4.transform.localEulerAngles = InitAttack3Ang4.eulerAngles;
            }


            if(flag1 && flag2 && flag3 && flag4)
            {
                Debug.Log("StateChange Move");
                StateProcessor.State = StateMove;
                CurrentAttack3Count = 0;
                isCenter = false;
                isInit = false;
                BW_1.ResetWing();
                BW_2.ResetWing();
                BW_3.ResetWing();
                BW_4.ResetWing();
            }
        }

        //Attack4
        if(StateProcessor.State.GetStateName() == "State:Attack4") //ビーム
        {
            if(isCenter)
            {
                Vector3 relativePos = PlayerObject.transform.position - this.transform.position;
                Vector3 relativePos2 = new Vector3(relativePos.x, relativePos.y - 12.0f, relativePos.z);
                    // 方向を、回転情報に変換
                Quaternion rotation = Quaternion.LookRotation (relativePos2);
                    // 現在の回転情報と、ターゲット方向の回転情報を補完する
                transform.rotation  = Quaternion.RotateTowards (this.transform.rotation, rotation, Attack4Lookspeed*Time.deltaTime*TimeAdd);

                CountTime += Time.deltaTime;
                if(CountTime >= Attack4Time)
                {
                    BossAttack4();
                }
                else
                {
                    //Beam Animation(Charge)
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Floor.transform.position, MoveSpeed*Time.deltaTime*TimeAdd);
                if(Vector3.Distance(transform.position, Floor.transform.position) <= DistancePosition)
                {
                    isCenter = true;
                }
            }
        }
        }
       

        //近接攻撃用のオンオフ
        /*
        if(isAttack3)
        {
            Attack3DurationTime -= Time.deltaTime;
            if(Attack3DurationTime <= 0)
            {
                isAttack3 = false;
            }
        }
        else
        {
            Attack3DurationTime = InitAttack3DurationTime;
            SwingField.SetActive(false);
        }
        */

        void ResetBossValue()
        {
            //Attack1 Value
            CountTime = 0;
            Magazine = InitMagazine;
            ReloadTime = InitReloadTime;

            //Attack2 Value
            Attack2time = 0.0f;

            //Attack3 Value
            CurrentAttack3Count = 0;
            isCenter = false;
            BW_1.ResetWing();
            BW_2.ResetWing();
            BW_3.ResetWing();
            BW_4.ResetWing();

            Attack3_1.transform.parent = WingParent.transform;
            Attack3_2.transform.parent = WingParent.transform;
            Attack3_3.transform.parent = WingParent.transform;
            Attack3_4.transform.parent = WingParent.transform;

            isAttack3_1 = false; //
            isAttack3_2 = false;
            isAttack3_3 = false;
            isAttack3_4 = false;
            BW_1.ResetWing();
            BW_2.ResetWing();
            BW_3.ResetWing();
            BW_4.ResetWing();
            CurrentAttack3Count = 4;
            CountTime = 0.0f;
            WingTimer = 0.0f;
            isInit = false;
            isCenter = false;
            isShockWaveGenerate = false;
            Attack3RiseTime = 0.0f;
            BW_1.isWingAttack = false;
            BW_2.isWingAttack = false;
            BW_3.isWingAttack = false;
            BW_4.isWingAttack = false;

            //Attack4
            CountTime = 0.0f;
            Attack4DurationTime = InitAttack4DurationTime;
            Attack4CoolTime = InitAttack4CoolTime;
            isCenter = false;
            Attack4BeamParticle.Stop();            
            
            Attack3_1.transform.localPosition = InitLocalAttack3Pos1;
            Attack3_2.transform.localPosition = InitLocalAttack3Pos2;
            Attack3_3.transform.localPosition = InitLocalAttack3Pos3;
            Attack3_4.transform.localPosition = InitLocalAttack3Pos4;
            Attack3_1.transform.localEulerAngles = InitAttack3Ang1.eulerAngles;
            Attack3_2.transform.localEulerAngles = InitAttack3Ang2.eulerAngles;
            Attack3_3.transform.localEulerAngles = InitAttack3Ang3.eulerAngles;
            Attack3_4.transform.localEulerAngles = InitAttack3Ang4.eulerAngles;
            
        }
    }

    private void BossAttack1() //n ten baasuto
    {
        if(Time.timeScale != 0)
        {
        ReloadTime += Time.deltaTime;
        if(ReloadTime >= InitReloadTime)
        {
            Vector3 vec = transform.position;
            vec = new Vector3(vec.x, vec.y+19.0f, vec.z);
            GameObject fBullet = Instantiate(Bullet, vec, transform.rotation);
            fBullet.SetActive(true);
            fBullet.transform.localScale = new Vector3(10, 10, 10);
            fBullet.transform.parent = null;
            fBullet.transform.position = vec;
            //fBullet.transform.rotation = this.gameObject.transform.rotation;
            ReloadTime = 0f;
            if(Magazine == 0)
            {
                StateProcessor.State = StateMove;
                CountTime = 0;
                Magazine = InitMagazine;
                ReloadTime = InitReloadTime;
            }
            else
            {
                Magazine -= 1;
            }
        }
        }
    }

    private void BossAttack2() //Rotation Attack2
    {
        if(Time.timeScale != 0)
        {
        Attack2time += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y, 0.0f);//Suihei ni suru
        if (Attack2time <= Attack2stoptime)
        {
            transform.Rotate(new Vector3(0, At2RollSpeed, 0));
            At2count += 1;
            Vector3 vec1 = new Vector3(Attack3_1.transform.position.x, Attack3_1.transform.position.y - 3.5f, Attack3_1.transform.position.z);
            Vector3 vec2 = new Vector3(Attack3_2.transform.position.x, Attack3_2.transform.position.y - 3.5f, Attack3_2.transform.position.z);
            Vector3 vec3 = new Vector3(Attack3_3.transform.position.x, Attack3_3.transform.position.y - 3.5f, Attack3_3.transform.position.z);
            Vector3 vec4 = new Vector3(Attack3_4.transform.position.x, Attack3_4.transform.position.y - 3.5f, Attack3_4.transform.position.z);
            if (At2count % 10 == 0)//10フレームごとに球発射
            {
                var Shot1 = Instantiate(attacksphere, vec1 + transform.forward * 15f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));
                Shot1.GetComponent<Rigidbody>().velocity = transform.forward.normalized * SphereSpeed;
                var Shot2 = Instantiate(attacksphere, vec2 + transform.forward * 15f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));
                Shot2.GetComponent<Rigidbody>().velocity = transform.forward.normalized * SphereSpeed;
                var Shot3 = Instantiate(attacksphere, vec3 - transform.forward * 15f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));
                Shot3.GetComponent<Rigidbody>().velocity = -transform.forward.normalized * SphereSpeed;
                var Shot4 = Instantiate(attacksphere, vec4 - transform.forward * 15f, Quaternion.LookRotation(Vector3.up, -1 * transform.forward));
                Shot4.GetComponent<Rigidbody>().velocity = -transform.forward.normalized * SphereSpeed;
            }
        }
        }
    }

    private void BossAttack3() //hane sasi
    {
        if(Time.timeScale != 0)
        {
        if(Time.timeScale != 0)
        {
            if(CurrentAttack3Count == 0)
            {
                Attack3_1.transform.parent = null;
                if(Attack3RunUpCountTime+0.3f <= Attack3RunUp)
                {
                    Debug.Log("Boss Wing idle");
                    Attack3RunUpCountTime += Time.deltaTime;
                    Attack3_1.transform.position += -Attack3_1.transform.forward * Attack3RiseMagnification;
                }
                else if(Attack3RunUpCountTime <= Attack3RunUp)
                {
                    Debug.Log("Boss Wing Shot");
                    Attack3RunUpCountTime += Time.deltaTime;
                    
                }
                else
                {
                    if(!isNear)
                    {
                        Attack3Position = PositionCheck();
                        Attack3Position = new Vector3(Attack3Position.x, PlayerObject.transform.position.y, Attack3Position.z);
                        Vector3 velocity = (Attack3Position - Attack3_1.transform.position) * Attack3Speed * 0.55f;
                        Attack3_1.transform.position += velocity *= Time.deltaTime;
                        
                        if(Vector3.Distance(Attack3Position, Attack3_1.transform.position) <= Attack3Distance)
                        {
                            isNear = true;
                            BW_1.WingGroundTimer = 0.0f;
                            Attack3SavePos = (Attack3Position - Attack3_1.transform.position) * Attack3Speed * 0.7f;
                            BW_1.isWingAttack = true;
                        }
                    }
                    else
                    {
                        if(!isNear2 && WingTimer <= 5.0f)
                        {
                            WingTimer += Time.deltaTime;
                            Vector3 velocity = Attack3SavePos;
                            Attack3_1.transform.position += velocity *= Time.deltaTime;
                            if(BW_1.isWing && BW_1.WingGroundTimer >= 0.05f)
                            {
                                isNear2 = true;
                                BW_1.isWingAttack = false;
                            }
                        }
                        else
                        {
                            if(Attack3DelayTime >= 0)
                            {
                                isAttack3_1 = true; 
                                Attack3DelayTime -= Time.deltaTime;
                                //ShockWave
                                if(!isShockWaveGenerate)
                                {
                                    isShockWaveGenerate = true;
                                    GameObject cloneWave1 = Instantiate(ShockWave, Attack3SavePos, Quaternion.identity);
                                    cloneWave1.SetActive(true);
                                    cloneWave1.transform.parent = null;
                                    cloneWave1.transform.position = Attack3_1.transform.position;
                                }
                            }
                            else
                            {
                                Attack3RunUpCountTime = 0;
                                Attack3DelayTime = InitAttack3DelayTime;
                                isNear = isNear2 = false;
                                if(CurrentAttack3Count == Attack3Count)
                                {
                                    Attack3_1.transform.parent = WingParent.transform;
                                    isAttack3_1 = false; //
                                    isAttack3_2 = false;
                                    isAttack3_3 = false;
                                    isAttack3_4 = false;
                                    isShockWaveGenerate = false;
                                    BW_1.ResetWing();
                                    BW_2.ResetWing();
                                    BW_3.ResetWing();
                                    BW_4.ResetWing();
                                    CurrentAttack3Count = 4;
                                    CountTime = 0.0f;
                                    WingTimer = 0.0f;
                                    Attack3RiseTime = 0.0f;
                                }
                                else
                                {
                                    isShockWaveGenerate = false;
                                    CurrentAttack3Count += 1;
                                }
                                
                            }
                        }   
                    }
                }
            }

            if(CurrentAttack3Count == 1)
            {
                Attack3_2.transform.parent = null;
                if(Attack3RunUpCountTime+0.3f <= Attack3RunUp)
                {
                    Attack3RunUpCountTime += Time.deltaTime;
                    Attack3_2.transform.position += -Attack3_2.transform.forward * Attack3RiseMagnification;
                }
                else if(Attack3RunUpCountTime <= Attack3RunUp)
                {
                    Attack3RunUpCountTime += Time.deltaTime;
                }
                else
                {
                    if(!isNear)
                    {
                        Attack3Position = PositionCheck();
                        Attack3Position = new Vector3(Attack3Position.x, PlayerObject.transform.position.y, Attack3Position.z);
                        Vector3 velocity = (Attack3Position - Attack3_2.transform.position) * Attack3Speed * 0.5f;
                        Attack3_2.transform.position += velocity *= Time.deltaTime;
                        
                        if(Vector3.Distance(Attack3Position, Attack3_2.transform.position) <= Attack3Distance)
                        {
                            isNear = true;
                            BW_2.WingGroundTimer = 0.0f;
                            Attack3SavePos = (Attack3Position - Attack3_2.transform.position) * Attack3Speed * 0.7f;
                            BW_2.isWingAttack = true;
                        }
                    }
                    else
                    {
                        if(!isNear2 && WingTimer <= 5.0f)
                        {
                            WingTimer += Time.deltaTime;
                            Vector3 velocity = Attack3SavePos;
                            Attack3_2.transform.position += velocity *= Time.deltaTime;
                            if(BW_2.isWing && BW_2.WingGroundTimer >= 0.05f)
                            {
                                isNear2 = true;
                                BW_2.isWingAttack = false;
                            }
                        }
                        else
                        {
                            if(Attack3DelayTime >= 0)
                            {
                                isAttack3_2 = true; 
                                Attack3DelayTime -= Time.deltaTime;
                                //
                                if(!isShockWaveGenerate)
                                {
                                    isShockWaveGenerate = true;
                                    GameObject cloneWave2 = Instantiate(ShockWave, Attack3SavePos, Quaternion.identity);
                                    cloneWave2.SetActive(true);
                                    cloneWave2.transform.parent = null;
                                    cloneWave2.transform.position = Attack3_2.transform.position;
                                }
                            }
                            else
                            {
                                Attack3RunUpCountTime = 0;
                                Attack3DelayTime = InitAttack3DelayTime;
                                isNear = isNear2 = false;
                                if(CurrentAttack3Count == Attack3Count)
                                {
                                    Attack3_1.transform.parent = WingParent.transform;
                                    Attack3_2.transform.parent = WingParent.transform;

                                    isAttack3_1 = false; //
                                    isAttack3_2 = false;
                                    isAttack3_3 = false;
                                    isAttack3_4 = false;
                                    isShockWaveGenerate = false;
                                    BW_1.ResetWing();
                                    BW_2.ResetWing();
                                    BW_3.ResetWing();
                                    BW_4.ResetWing();
                                    CurrentAttack3Count = 4;
                                    CountTime = 0.0f;
                                    WingTimer = 0.0f;
                                    Attack3RiseTime = 0.0f;
                                }
                                else
                                {
                                    isShockWaveGenerate = false;
                                    CurrentAttack3Count += 1;
                                }
                            }
                        }   
                    }
                }
            }

            if(CurrentAttack3Count == 2)
            {
                Attack3_3.transform.parent = null;
                if(Attack3RunUpCountTime+0.3f <= Attack3RunUp)
                {
                    Attack3RunUpCountTime += Time.deltaTime;
                    Attack3_3.transform.position += -Attack3_3.transform.forward * Attack3RiseMagnification;
                }
                else if(Attack3RunUpCountTime <= Attack3RunUp)
                {
                    Attack3RunUpCountTime += Time.deltaTime;
                }
                else
                {
                    if(!isNear)
                    {
                        Attack3Position = PositionCheck();
                        Attack3Position = new Vector3(Attack3Position.x, PlayerObject.transform.position.y, Attack3Position.z);

                        Vector3 velocity = (Attack3Position - Attack3_3.transform.position) * Attack3Speed * 0.5f;
                        Attack3_3.transform.position += velocity *= Time.deltaTime;

                        if(Vector3.Distance(Attack3Position, Attack3_3.transform.position) <= Attack3Distance)
                        {
                            isNear = true;
                            BW_3.WingGroundTimer = 0.0f;
                            Attack3SavePos = (Attack3Position - Attack3_3.transform.position) * Attack3Speed * 0.7f;
                            BW_3.isWingAttack = true;
                        }
                    }
                    else
                    {
                        if(!isNear2 && WingTimer <= 5.0f)
                        {
                            WingTimer += Time.deltaTime;
                            Vector3 velocity = Attack3SavePos;
                            Attack3_3.transform.position += velocity *= Time.deltaTime;
                            if(BW_3.isWing && BW_3.WingGroundTimer >= 0.05f)
                            {
                                isNear2 = true;
                                BW_3.isWingAttack = false;
                            }
                        }
                        else
                        {
                            if(Attack3DelayTime >= 0)
                            {
                                isAttack3_3 = true; //
                                Attack3DelayTime -= Time.deltaTime;
                                //
                                if(!isShockWaveGenerate)
                                {
                                    isShockWaveGenerate = true;
                                    GameObject cloneWave3 = Instantiate(ShockWave, Attack3SavePos, Quaternion.identity);
                                    cloneWave3.SetActive(true);
                                    cloneWave3.transform.parent = null;
                                    cloneWave3.transform.position = Attack3_3.transform.position;
                                }
                            }
                            else
                            {
                                Attack3RunUpCountTime = 0;
                                Attack3DelayTime = InitAttack3DelayTime;
                                isNear = isNear2 = false;
                                if(CurrentAttack3Count == Attack3Count)
                                {
                                    Attack3_1.transform.parent = WingParent.transform;
                                    Attack3_2.transform.parent = WingParent.transform;
                                    Attack3_3.transform.parent = WingParent.transform;
                                    isAttack3_1 = false; //
                                    isAttack3_2 = false;
                                    isAttack3_3 = false;
                                    isAttack3_4 = false;
                                    isShockWaveGenerate = false;
                                    BW_1.ResetWing();
                                    BW_2.ResetWing();
                                    BW_3.ResetWing();
                                    BW_4.ResetWing();
                                    CurrentAttack3Count = 4;
                                    CountTime = 0.0f;
                                    WingTimer = 0.0f;
                                    Attack3RiseTime = 0.0f;

                                }
                                else
                                {
                                    isShockWaveGenerate = false;
                                    CurrentAttack3Count += 1;
                                }
                            }
                        }   
                    }
                }
            }

            if(CurrentAttack3Count == 3)
            {
                Attack3_4.transform.parent = null;
                if(Attack3RunUpCountTime+0.3f <= Attack3RunUp)
                {
                    Attack3RunUpCountTime += Time.deltaTime;
                    Attack3_4.transform.position += -Attack3_4.transform.forward * Attack3RiseMagnification;
                }
                else if(Attack3RunUpCountTime <= Attack3RunUp)
                {
                    Attack3RunUpCountTime += Time.deltaTime;
                }
                else
                {
                    if(!isNear)
                    {
                        Attack3Position = PositionCheck();
                        Attack3Position = new Vector3(Attack3Position.x, PlayerObject.transform.position.y, Attack3Position.z);
                        
                        Vector3 velocity = (Attack3Position - Attack3_4.transform.position) * Attack3Speed * 0.5f;
                        Attack3_4.transform.position += velocity *= Time.deltaTime;
                        
                        if(Vector3.Distance(Attack3Position, Attack3_4.transform.position) <= Attack3Distance)
                        {
                            isNear = true;
                            BW_4.WingGroundTimer = 0.0f;
                            Attack3SavePos = (Attack3Position - Attack3_4.transform.position) * Attack3Speed * 0.7f;
                            BW_4.isWingAttack = true;
                        }
                    }
                    else
                    {
                        if(!isNear2 && WingTimer <= 5.0f)
                        {
                            WingTimer += Time.deltaTime;
                            Vector3 velocity = Attack3SavePos;
                            Attack3_4.transform.position += velocity *= Time.deltaTime;
                            if(BW_4.isWing && BW_4.WingGroundTimer >= 0.05f)
                            {
                                isNear2 = true;
                                BW_4.isWingAttack = false;
                            }
                        }
                        else
                        {
                            if(Attack3DelayTime >= 0)
                            {
                                isAttack3_4 = true; //
                                Attack3DelayTime -= Time.deltaTime;
                                //
                                if(!isShockWaveGenerate)
                                {
                                    isShockWaveGenerate = true;
                                    GameObject cloneWave4 = Instantiate(ShockWave, Attack3SavePos, Quaternion.identity);
                                    cloneWave4.SetActive(true);
                                    cloneWave4.transform.parent = null;
                                    cloneWave4.transform.position = Attack3_4.transform.position;
                                }
                            }
                            else
                            {
                                Attack3RunUpCountTime = 0;
                                Attack3DelayTime = InitAttack3DelayTime;
                                isNear = isNear2 = false;
                                if(CurrentAttack3Count == Attack3Count)
                                {
                                    Attack3_1.transform.parent = WingParent.transform;
                                    Attack3_2.transform.parent = WingParent.transform;
                                    Attack3_3.transform.parent = WingParent.transform;
                                    Attack3_4.transform.parent = WingParent.transform;

                                    isAttack3_1 = false; //
                                    isAttack3_2 = false;
                                    isAttack3_3 = false;
                                    isAttack3_4 = false;
                                    isShockWaveGenerate = false;
                                    BW_1.ResetWing();
                                    BW_2.ResetWing();
                                    BW_3.ResetWing();
                                    BW_4.ResetWing();
                                    CurrentAttack3Count = 4;
                                    CountTime = 0.0f;
                                    WingTimer = 0.0f;
                                    Attack3RiseTime = 0.0f;
                                }
                                else
                                {
                                    isShockWaveGenerate = false;
                                    CurrentAttack3Count += 1;
                                }
                            }
                        }   
                    }
                }
            }
        }
        }
    }

    private void BossAttack4()
    {
        if(Time.timeScale != 0)
        {
        //Beam Animation(Active)
        Attack4DurationTime -= Time.deltaTime;
        if(Attack4DurationTime <= 0)
        {
            Attack4CoolTime -= Time.deltaTime;
            if(Attack4CoolTime <= 0)
            {
                StateProcessor.State = StateMove;
                CountTime = 0.0f;
                Attack4DurationTime = InitAttack4DurationTime;
                Attack4CoolTime = InitAttack4CoolTime;
                isCenter = false;
            }
        }
        else
        {
            Attack4BeamParticle.Play();
        }
        }
    }

    //YukinoState Check
    private Vector3 PositionCheck()
    {
        if(YukinoMain.currentState == YukinoMain.stateWalking)
        {
            Debug.Log("YukinoState:Walk");
            return PlayerObject.transform.position + PlayerObject.transform.forward * 20.0f;
        }else if(YukinoMain.currentState == YukinoMain.stateRunning)
        {
            return PlayerObject.transform.position + PlayerObject.transform.forward * 40.0f;
        }else if(YukinoMain.currentState == YukinoMain.stateForwardRolling)
        {
            return PlayerObject.transform.position + PlayerObject.transform.forward * 40.0f;
        }else if(YukinoMain.currentState == YukinoMain.stateEnergyDush)
        {
            return PlayerObject.transform.position + PlayerObject.transform.forward * 40.0f;
        }else
        {
            Debug.Log("YukinoState:Nanika");
            return PlayerObject.transform.position;
        }
    }

    //Boss State Change desu
    private void RandomStateChanege(int PhaseNumber)
    {
        int randomvalue = Random.Range(0, 10); //state change
        if(PhaseNumber == 0) //debug
        {
            StateProcessor.State = StateAttack4;
        }
        else if(PhaseNumber == 1)
        {
            if(FloorNumber == 1)
            {
                //koko ni tama speed ireru
                StateProcessor.State = StateAttack1;
            }
            else if(FloorNumber == 2)
            {
                if(randomvalue > 2)
                {
                    StateProcessor.State = StateAttack1;
                }
                else if(randomvalue > -1)
                {
                    Attack3Count = 0;
                    StateProcessor.State = StateAttack3; //once
                }
            }
            else if(FloorNumber == 3)
            {
                if(randomvalue > 2)
                {
                    StateProcessor.State = StateAttack1;
                }
                else if(randomvalue > -1)
                {
                    StateProcessor.State = StateAttack2;
                }
            }
        }
        else if(PhaseNumber == 2)
        {
            if(FloorNumber == 1)
            {
                if(!isPhase2Beam)
                {
                    //Beam Value Phase2  Attack4Lookspeed = 3.0f 
                    isPhase2Beam = true;
                    StateProcessor.State = StateAttack4;
                }
                else
                {
                    StateProcessor.State = StateAttack1;
                }
            }
            else if(FloorNumber == 2)
            {
                if(randomvalue > 2)
                {
                    StateProcessor.State = StateAttack1;
                }
                else if(randomvalue > -1)
                {
                    Attack3Count = 3; //force
                    StateProcessor.State = StateAttack3; //force
                }
            }
            else if(FloorNumber == 3)
            {
                if(randomvalue > 5)
                {
                    StateProcessor.State = StateAttack1;
                }
                else if(randomvalue > 2)
                {
                    StateProcessor.State = StateAttack2;
                }
                else if(randomvalue > -1)
                {
                    Attack3Count = 3; //hane yottu
                    StateProcessor.State = StateAttack3; //4 ren
                }
            }
        }else if(PhaseNumber == 3)
        {
            if(FloorNumber == 1)
            {
                if(!isPhase3Beam)
                {
                    //Beam Value Phase3
                    isPhase3Beam = true;
                    StateProcessor.State = StateAttack4;
                }
                else
                {
                    StateProcessor.State = StateAttack2;
                }
            }
            else if(FloorNumber == 2)
            {
                if(randomvalue > 2)
                {
                    StateProcessor.State = StateAttack2;
                }
                else if(randomvalue > -1)
                {
                    Attack3Count = 3;
                    StateProcessor.State = StateAttack3; //4
                }
            }
            else if(FloorNumber == 3)
            {
                if(randomvalue > 5)
                {
                    StateProcessor.State = StateAttack2;
                }
                else if(randomvalue > 2)
                {
                    Attack3Count = 3;
                    StateProcessor.State = StateAttack3; //4
                }
                else if(randomvalue > -1)
                {
                    //Beam Value Phase3
                    StateProcessor.State = StateAttack4; 
                }
            }
        }
    }

    //近接攻撃の名残
    /*
    private void BossAttack3()
    {
        Attack3SwingTime -= Time.deltaTime;
        if(Attack3SwingTime <= 0)
        {
            Attack3SwingTime = InitAttack3SwingTime;
            isAttack3 = true;
            SwingField.SetActive(true);
            if(Attack3Count <= 0)
            {
                StateProcessor.State = StateMove;
                Attack3Count = InitAttack3Count;
                CountTime = 0;
            }
            else
            {
                Attack3Count -= 1;
            }
        }
        //判定 
    }
    */

    public void Idle()
    {
        //Debug.Log("CharacterStateがIdleに状態遷移しました。");
    }
    public void Move()
    {
        //Debug.Log("CharacterStateがMoveに状態遷移しました。");
    }
    public void Attack1()
    {
        //Debug.Log("CharacterStateがAttack1に状態遷移しました。");
    }
    public void Attack2()
    {
        //Debug.Log("CharacterStateがAttack2に状態遷移しました。");
    }
    public void Attack3()
    {
        //Debug.Log("CharacterStateがAttack3に状態遷移しました。");
    }
    public void Attack1Idle()
    {
        //Debug.Log("CharacterStateがAttack1に状態遷移しました。");
    }
    public void Attack3Idle()
    {
        //Debug.Log("CharacterStateがAttack1に状態遷移しました。");
    }
    public void Attack4Idle()
    {
        //Debug.Log("CharacterStateがAttack1に状態遷移しました。");
    }
    public void Attack4()
    {
        //Debug.Log("CharacterStateがAttack4に状態遷移しました。");
    }
    public void Attack5()
    {
        //Debug.Log("CharacterStateがAttack5に状態遷移しました。");
    }
    public void Attack5Idle()
    {
        //Debug.Log("CharacterStateがAttack5Idleに状態遷移しました。");
    }
    public void ChangeFloor()
    {
        //Debug.Log("CharacterStateがChangeFloorに状態遷移しました。");
    }

    void OnColliderEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && PlayerObject.transform.position.y >= transform.position.y)
        {
            isNearPlayer = true;
        }
    }
    void OnColliderExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && PlayerObject.transform.position.y >= transform.position.y)
        {
            isNearPlayer = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }
}
