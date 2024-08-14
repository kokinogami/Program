using TMPro;
using UnityEngine;

public class TypeWriteEffect : MonoBehaviour
{
    // 対象のテキスト
    [SerializeField] private TMP_Text _text;

    // 次の文字を表示するまでの時間[s]
    [SerializeField] private float _delayDuration = 0.1f;

    // 演出処理に使用する内部変数
    private bool _isRunning;
    private float _remainTime;
    private int _currentMaxVisibleCharacters;

    public void Start()
    {
        // 演出を開始するように内部状態をセット
        _isRunning = true;
        _remainTime = _delayDuration;
        _currentMaxVisibleCharacters = 0;
    }

    private void Update()
    {
        // 演出実行中でなければ何もしない
        if (!_isRunning) return;

        // 次の文字表示までの残り時間更新
        _remainTime -= Time.deltaTime;
        if (_remainTime > 0) return;

        // 表示する文字数を一つ増やす
        _text.maxVisibleCharacters = ++_currentMaxVisibleCharacters;
        _remainTime = _delayDuration;

        // 文字を全て表示したら待機状態に移行
        if (_currentMaxVisibleCharacters >= _text.text.Length)
            _isRunning = false;
    }
}