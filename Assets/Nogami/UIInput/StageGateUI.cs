using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGateUI : MonoBehaviour
{
    // �I�u�W�F�N�g���f���J����
    Camera _targetCamera;

    // UI��\��������ΏۃI�u�W�F�N�g
    [SerializeField] Transform _target;

    // �\������UI
    [SerializeField] Transform _targetUI;
    [SerializeField]Image[] targetUIImage = new Image[2];

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] Vector3 _worldOffset;

    RectTransform _parentUI;

    Transform yukinotransform;

    [SerializeField] float activeDistance = 50;
    [SerializeField] float ChangeDistance = 10;
    private void Awake()
    {
        _targetCamera = Camera.main;

        // Canvas��RectTransform��ێ�
        TryGetComponent(out _parentUI);

        targetUIImage = GetComponentsInChildren<Image>();
        targetUIImage[0].gameObject.SetActive(false);
        targetUIImage[1].gameObject.SetActive(false);
    }

    private void Start()
    {
        yukinotransform = GameManager.Main.transform;
    }
    // UI�̈ʒu�𖈃t���[���X�V
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

        if (distance <= Mathf.Pow(ChangeDistance, 2))//�߂�
        {
            if (targetUIImage[1].gameObject.activeSelf == false)
            {
                targetUIImage[0].gameObject.SetActive(false);
                targetUIImage[1].gameObject.SetActive(true);
            }
        }
        else if (distance <= Mathf.Pow(activeDistance, 2))//����
        {
            if (targetUIImage[0].gameObject.activeSelf == false)
            {
                targetUIImage[0].gameObject.SetActive(true);
                targetUIImage[1].gameObject.SetActive(false);
            }
        }
        else//�`��O
        {
            if (targetUIImage[0].gameObject.activeSelf || targetUIImage[0].gameObject.activeSelf)
            {
                targetUIImage[0].gameObject.SetActive(false);
                targetUIImage[1].gameObject.SetActive(false);
            }
        }
    }


    // UI�̈ʒu���X�V����
    private void OnUpdatePosition()
    {
        var cameraTransform = _targetCamera.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = _target.position + _worldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        _targetUI.gameObject.SetActive(isFront);
        if (!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        _targetUI.localPosition = uiLocalPos;
    }
}
