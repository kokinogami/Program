using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailAdjuster : MonoBehaviour
{
    [System.NonSerialized] public YukinoMain Main;
    private float adjusterForFallingY;
    private float adjusterForFallingZ;
    // Start is called before the first frame update
    void Start()
    {
        Main = FindObjectOfType<YukinoMain>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Main.rb.velocity.y <= -3f)
        adjusterForFallingY = 0.05f * Main.rb.velocity.y;

        else
        adjusterForFallingY = 0.01f * Main.rb.velocity.y;

        adjusterForFallingZ = Mathf.Max(Mathf.Min(-1f, Main.rb.velocity.y), -2f);
        this.transform.localPosition = new Vector3(0.0f, adjusterForFallingY, Main.rb.velocity.magnitude * 0.08f + adjusterForFallingZ);
    }
}
