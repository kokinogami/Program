using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    YukinoMain Main;
    // Start is called before the first frame update
    void Start()
    {
         TryGetComponent(out Main);
    }

    // Update is called once per frame
    void Update()
    {
        if (Main.Ground == true)
        {

        }
    }
}
