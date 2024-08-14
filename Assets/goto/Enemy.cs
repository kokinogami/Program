using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���f����Renderer
    [SerializeField]
    private Renderer _renderer;

    private Sequence _seq;

    private void OnTriggerEnter(Collider other)
    {
        HitBlink();
    }

    //�����̓_�łɂ��_���[�W���o
    private void HitBlink()
    {
        _seq.Kill();
        _seq = DOTween.Sequence();
        _seq.AppendCallback(() => _renderer.enabled = false);
        _seq.AppendInterval(0.07f);
        _seq.AppendCallback(() => _renderer.enabled = true);
        _seq.AppendInterval(0.07f);
        _seq.SetLoops(2);
        _seq.Play();
    }
}
