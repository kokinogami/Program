using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    float Count;
    MeshRenderer Color;
    // Start is called before the first frame update
    void Start()
    {
        //TryGetComponent(out Color);
    }

    // Update is called once per frame
    void Update()
    {
        Count += Time.deltaTime;
        if (Count > 3.0f)
        {
            Destroy(this.gameObject);
        }
        /*if (Count > 0.0f)
        {
            int CountInt = Mathf.FloorToInt(255/Count);
            Color = GetComponent<MeshRenderer>();
            Color.material.color = new Color32(0, 0, 0, (byte)CountInt);
        }*/
    }
}
