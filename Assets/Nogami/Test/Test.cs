using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Test : MonoBehaviour
{
    public static int A=1;
    // Start is called before the first frame update
    void Start()
    {
        A = 0;
    }

    // Update is called once per frame
    void Update()
    {
        A++;
        //Debug.Log(A);
        //Debug.Log(B);
        //new SubTest.OnUpdateS(this);
        //OnUpdate();
        //SubTest.OnUpdate();
        //new SubTest().OnUpdate(this);
    }
}
