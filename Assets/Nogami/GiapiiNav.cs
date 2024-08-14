using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiapiiNav : MonoBehaviour
{
    public GameObject Yukino;
    public GameObject Target;
    private float speed;
    public Vector3 YukinoPos;
    public Vector3 NavPositon;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        NavPositon = Target.transform.position;
        YukinoPos = Yukino.transform.position;
        if (Vector3.Distance(this.gameObject.transform.position, NavPositon) >= 0.1f)
        {
            transform.LookAt(NavPositon);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
                else
        {
            transform.rotation = Yukino.transform.rotation;
        }
    }
}
