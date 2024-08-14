using TMPro;
using UnityEngine;

public class TypeWriteEffect : MonoBehaviour
{
    // �Ώۂ̃e�L�X�g
    [SerializeField] private TMP_Text _text;

    // ���̕�����\������܂ł̎���[s]
    [SerializeField] private float _delayDuration = 0.1f;

    // ���o�����Ɏg�p��������ϐ�
    private bool _isRunning;
    private float _remainTime;
    private int _currentMaxVisibleCharacters;

    public void Start()
    {
        // ���o���J�n����悤�ɓ�����Ԃ��Z�b�g
        _isRunning = true;
        _remainTime = _delayDuration;
        _currentMaxVisibleCharacters = 0;
    }

    private void Update()
    {
        // ���o���s���łȂ���Ή������Ȃ�
        if (!_isRunning) return;

        // ���̕����\���܂ł̎c�莞�ԍX�V
        _remainTime -= Time.deltaTime;
        if (_remainTime > 0) return;

        // �\�����镶����������₷
        _text.maxVisibleCharacters = ++_currentMaxVisibleCharacters;
        _remainTime = _delayDuration;

        // ������S�ĕ\��������ҋ@��ԂɈڍs
        if (_currentMaxVisibleCharacters >= _text.text.Length)
            _isRunning = false;
    }
}