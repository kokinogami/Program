using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class GameManager
{
    public enum Language
    {
        Japanease,
        English
    }
    public enum InputHardware
    {
        GamePad,
        KeyboardMouse,
    }
    [HeaderAttribute("�A�^�b�`�p�ϐ�")]
    [TooltipAttribute("YukinoMain"), SerializeField] YukinoMain yukinoMain;
    [TooltipAttribute("YukinoMain"), SerializeField] YukinoSound yukinoSound;
    [TooltipAttribute("Enemy1Demo"), SerializeField] List<EnemyDestroyEvent> AllEnemyList;
    [TooltipAttribute("Enemy1Demo"), SerializeField] TargetMarkCreate TargetMarkCreate;
    //���L�ϐ�
    public static YukinoMain Main;
    public static YukinoSound Sound;
    public static List<EnemyDestroyEvent> AllEnemy;
    public static TargetMarkCreate TaegwtMark;
    public static int EnemyCount;
    public static int Maxfps = 120;//�ő�fps�l
    public static float svValue;//���x�̒l
    public static float CmSvValue;//�J�������x�̒l
    public static float ChSvValue;//�`���[�W�u���[�L���x�̒l
    public static bool firestStart = false;
    public static bool ChangeSceenReset = false;
    public static bool titleBack = false;
    public static InputHardware currentInputHardware = InputHardware.GamePad;

    public static Language language = Language.Japanease;

    //�X�e�[�W�N���A�ϐ�
    public static bool tutorial1Clear;
    public static bool tutorial2Clear;
    public static bool stage1Clear;
    public static bool stage2Clear;
    public static bool stage3Clear;
    public static bool BossClear;


    //TimeLine�悤�ϐ�
    public static bool tutorialUnlock = false;
    public static bool MainmapUnlock = false;
    public static bool isStage2Unlock = false;
    public static bool isStage3Unlock = false;
    public static bool isBossUnlock = false;

    // Start is called before the first frame update
    void DataBaseStart()
    {
        Sound = yukinoSound;
        if (stageState != Stage.Title)
        {
            Debug.Log("SetSound");
            Main = yukinoMain;
            AllEnemy = AllEnemyList;
            EnemyCount = AllEnemy.Count;
            TaegwtMark = TargetMarkCreate;
            for (int i = 0; i < GameManager.AllEnemy.Count; i++)
            {
                GameManager.AllEnemy[i].listNum = i;
            }
        }
        else
        {
            Main = null;
        }
        yukinoMain = null;
        yukinoSound = null;
        AllEnemyList = null;
        startSvSet();
    }

    // Update is called once per frame
    void OnUpdate()
    {

    }

    void startSvSet()
    {
        if (firestStart == false)
        {
            svValue = 0.5f;
            CmSvValue = 1;
            ChSvValue = 1;
            firestStart = true;
        }
    }
    //��������ǎg��Ȃ��Ȃ������
    public const string Untagged = "Untagged";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string GameController = "GameController";
    public const string Shootable = "Shootable";
    public const string Ground = "Ground";
    public const string tenjou = "tenjou";
    public const string joushou = "joushou";
    public const string JumpPad = "JumpPad";
    public const string BrokenObject = "BrokenObject";
    public const string heal = "heal";
    public const string enemy = "enemy";
    public const string DeathZone = "DeathZone";
    public const string checkPoint = "checkPoint";
    public const string CollectibleItem = "CollectibleItem";
    public const string RespawnableStage = "RespawnableStage";
    public const string HipDropCollider = "HipDropCollider";
}
