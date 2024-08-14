using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn_Out_W : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float fadeSpeed;
    [SerializeField] float outStartCount = 0.5f;
    float count;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Speed", fadeSpeed);
    }

    private void OnEnable()
    {
        animator.SetFloat("Speed", fadeSpeed);
        animator.SetBool("fadeout", false);
        count = outStartCount;
    }
    // Update is called once per frame
    void Update()
    {
        count -= Time.timeScale;
        if (GameManager.Main.Ground == false)
        {
            count = outStartCount;
        }
        else if (count <= 0)
        {
            animator.SetBool("fadeout", true);
        }
    }
    public void OnRespawn()
    {
        GameManager.Main.Respawn();
    }
    public void setActiveFalse()
    {
        GameManager.Main.OnWhiteFade = false;
        GameManager.Main.fadeInOut_W.gameObject.SetActive(false);
    }
}
