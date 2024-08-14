using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukidamaAnimatin :MonoBehaviour
{
    Animator Yukidamaanimator;
    private string MoveStr = "isMove";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveAnimaStr()
    {
        this.Yukidamaanimator.SetBool(MoveStr, true);
    }
    public void MoveAnimafin()
    {
        this.Yukidamaanimator.SetBool(MoveStr, false);
    }
}
