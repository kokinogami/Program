using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipDropCollider : MonoBehaviour
{
    Vector3 FARST_SCALE = new Vector3(0, 0.1f, 0);
    Vector3 MAX_SCALE = new Vector3(0, 0, 0);
    float smoothTime = 0.4f;
    Vector2 scaleChange;
    Vector3 currentvelocity;
    float count;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        transform.localScale = FARST_SCALE;
        //count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 nowScale = new Vector2(transform.localScale.x, transform.localScale.z);
        //scaleChange = Vector2.SmoothDamp();
        transform.localScale = Vector3.SmoothDamp(transform.localScale, MAX_SCALE, ref currentvelocity, smoothTime);
        if (currentvelocity.x <= 0.5f) GameManager.Main.setactivHipdrop(false);
    }
    private void OnDisable()
    {
        transform.localScale = FARST_SCALE;
    }
}
