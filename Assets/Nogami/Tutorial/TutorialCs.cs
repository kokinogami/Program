using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCs : MonoBehaviour
{
    const string Lstick = "Lstick";
    const string Rstick = "Rstick";
    const string L1 = "L1";
    const string L2 = "L2";
    const string R1 = "R1";
    const string R2 = "R2";
    const string square = "Å†";
    const string triangle = "Å¢";
    const string circle = "ÅZ";
    const string cross = "Å~";
    const string crossUp = "Å™";
    const string crossright = "Å®";
    const string crossLeft = "Å©";
    const string crossDown = "Å´";

    const string Mouse = "Mouse";
    const string MouseRight = "MouseRight";
    const string MouseLeft = "MouseLeft";
    const string RKey = "RKey";
    const string QKey = "QKey";
    const string FKey = "FKey";
    const string CKey = "CKey";
    const string AKey = "AKey";
    const string WKey = "WKey";
    const string SKey = "SKey";
    const string DKey = "DKey";
    const string EKey = "EKey";
    const string space = "Space";

    string move;
    string cameraText;
    string jump;
    string doubleJump;
    string glider;
    string dash;
    string iceJump;
    string heal;
    string boost;
    string hipdrop;
    string chargeBreak;
    string chargeBreakDush;
    [SerializeField] windowscript windowscript;
    // Start is called before the first frame update
    void Start()
    {
        windowscript.animator = GetComponent<Animator>();
    }

    public void SetInput()
    {
        windowscript.SetInput();
    }
}
