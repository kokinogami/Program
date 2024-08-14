using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SubTest : Test
{
    // Start is called before the first frame update

    // Update is called once per frame
    public void Update()
    {
        //A++;
        Debug.Log(A);
    }
    /*public partial class SubTest : ThirdTest
    {
        public override void OnUpdate(Test owner)
        {
            A++;
            //Debug.Log(A);
        }
    }*/
}
public partial class Test
{
    public static int B = 12;
    public void OnUpdate()
    {
        
    }
}
