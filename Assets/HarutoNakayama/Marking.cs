using UnityEngine;
using UnityEngine.UI;

public class Marking : MonoBehaviour
{
    // オブジェクトを映すカメラ
    /*[SerializeField] */
    private Camera _targetCamera;

    // プレイヤー
    private Transform _player;

    // UIを表示させる対象オブジェクト
    [SerializeField] public Transform target;

    // 表示するUI
    /*[SerializeField]*/
    private Transform _targetUI;
    /*[SerializeField]*/
    [System.NonSerialized] public Image _targetUIImage;

    // オブジェクト位置のオフセット
    [SerializeField] private Vector3 _worldOffset;

    [System.NonSerialized] public RectTransform _parentUI;

    bool first = true;
    /*[System.NonSerialized]*/public bool none = false;
    // 初期化メソッド（Prefabから生成する時などに使う）
    public void Initialize(Transform target, Camera targetCamera = null)
    {
        this.target = target;
        _targetCamera = targetCamera != null ? targetCamera : Camera.main;

        OnUpdatePosition();
    }

    private void Awake()
    {
        // カメラが指定されていなければメインカメラにする
        if (_targetCamera == null)
            _targetCamera = Camera.main;

        _targetUI = this.transform;
        _targetUIImage = GetComponent<Image>();

        // 親UIのRectTransformを保持
        //_parentUI = _targetUI.parent.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        _player = GameManager.Main.transform;
    }
    private void OnEnable()
    {
        if (target == null && first != true) Destroy(this.gameObject);
        first = false;
    }
    private void OnDisable()
    {
        _targetUIImage.enabled = false;
    }
    public void UIenable(bool On)
    {
        _targetUIImage.enabled = On;
    }
    // UIの位置を毎フレーム更新
    private void Update()
    {
        OnUpdatePosition();
        if (target == null && first != true) Destroy(this.gameObject);
    }

    // UIの位置を更新する
    private void OnUpdatePosition()
    {
        if (none)
        {
            _targetUIImage.enabled = false;
            return;
        }
        var cameraTransform = _targetCamera.transform;

        // カメラの向きベクトル
        var cameraDir = cameraTransform.forward;
        // オブジェクトの位置
        var targetWorldPos = target.position + _worldOffset;
        // カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        // 内積を使ってカメラ前方かどうかを判定
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
        //_targetUI.gameObject.SetActive(isFront);
        _targetUIImage.enabled = isFront;
        if (!isFront) return;

        // オブジェクトのワールド座標→スクリーン座標変換
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );
        var distance = (GameManager.Main.transform.position - target.transform.position).sqrMagnitude;
        var posUI = 2000 / distance;
        if (posUI > 4)
        {
            float posPlus = posUI - 4;
            posPlus = posUI / 80;
            posUI = posPlus + 1.27f;
        }
        else if (posUI > 3)
        {
            float posPlus = posUI - 3f;
            posPlus = posPlus / 50;
            posUI = posPlus + 1.25f;
        }
        else if (posUI > 2)
        {
            float posPlus = posUI - 2f;
            posPlus = posPlus / 20;
            posUI = posPlus + 1.2f;
        }
        else if (posUI > 1)
        {
            float posPlus = posUI - 1;
            posPlus = posPlus / 5;
            posUI = posPlus + 1;
        }
        posUI = posUI * 60;


        var size = 2000 / distance;
        if (size > 1.5)
        {
            size = 1.5f;
        }
        if (size < 1)
        {
            size += 1;
            size = size / 2;
        }
        else if (size > 1)
        {
            size -= 1;
            size = size / 2;
            size += 1;
        }

        // RectTransformのローカル座標を更新
        _targetUI.localPosition = uiLocalPos + new Vector2(0, posUI);//80);
        _targetUI.localScale = new Vector2(size, size);//80);
    }
}