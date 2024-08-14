using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float followrate;
    Vector3 cloudStartpos;
    float YukinoStartpos;
    // Start is called before the first frame update
    void Start()
    {
        cloudStartpos = transform.position;
        YukinoStartpos = Player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(0.0f, followrate*(Player.transform.position.y - YukinoStartpos), 0.0f);
        this.transform.position = cloudStartpos + pos;
    }
}
