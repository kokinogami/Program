using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGateUI : MonoBehaviour
{
    // オブジェクトを映すカメラ
    Camera _targetCamera;

    // UIを表示させる対象オブジェクト
    [SerializeField] Transform _target;

    // 表示するUI
    [SerializeField] Transform _targetUI;
    [SerializeField]Image[] targetUIImage = new Image[2];

    // オブジェクト位置のオフセット
    [SerializeField] Vector3 _worldOffset;

    RectTransform _parentUI;

    Transform yukinotransform;

    [SerializeField] float activeDistance = 50;
    [SerializeField] float ChangeDistance = 10;
    private void Awake()
    {
        _targetCamera = Camera.main;

        // CanvasのRectTransformを保持
        TryGetComponent(out _parentUI);

        targetUIImage = GetComponentsInChildren<Image>();
        targetUIImage[0].gameObject.SetActive(false);
        targetUIImage[1].gameObject.SetActive(false);
    }

    private void Start()
    {
        yukinotransform = GameManager.Main.transform;
    }
    // UIの位置を毎フレーム更新
    private void Update()
    {
        distancePlayer();
        if (targetUIImage[0].gameObject.activeSelf == false && targetUIImage[1].gameObject.activeSelf == false) return;
        OnUpdatePosition();
    }
    private void LateUpdate()
    {
        if (targetUIImage[0].gameObject.activeSelf == false && targetUIImage[1].gameObject.activeSelf == false) return;
        OnUpdatePosition();
    }
    private void FixedUpdate()
    {
        if (targetUIImage[0].gameObject.activeSelf == false && targetUIImage[1].gameObject.activeSelf == false) return;
        OnUpdatePosition();
    }

    void distancePlayer()
    {
        var a = yukinotransform.position;
        var tP = _target.transform.position;
        float distance = (a - tP).sqrMagnitude;

        if (distance <= Mathf.Pow(ChangeDistance, 2))//近い
        {
            if (targetUIImage[1].gameObject.activeSelf == false)
            {
                targetUIImage[0].gameObject.SetActive(false);
                targetUIImage[1].gameObject.SetActive(true);
            }
        }
        else if (distance <= Mathf.Pow(activeDistance, 2))//遠い
        {
            if (targetUIImage[0].gameObject.activeSelf == false)
            {
                targetUIImage[0].gameObject.SetActive(true);
                targetUIImage[1].gameObject.SetActive(false);
            }
        }
        else//描画外
        {
            if (targetUIImage[0].gameObject.activeSelf || targetUIImage[0].gameObject.activeSelf)
            {
                targetUIImage[0].gameObject.SetActive(false);
                targetUIImage[1].gameObject.SetActive(false);
            }
        }
    }


    // UIの位置を更新する
    private void OnUpdatePosition()
    {
        var cameraTransform = _targetCamera.transform;

        // カメラの向きベクトル
        var cameraDir = cameraTransform.forward;
        // オブジェクトの位置
        var targetWorldPos = _target.position + _worldOffset;
        // カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        // 内積を使ってカメラ前方かどうかを判定
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
        _targetUI.gameObject.SetActive(isFront);
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

        // RectTransformのローカル座標を更新
        _targetUI.localPosition = uiLocalPos;
    }
}
