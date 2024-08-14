using UnityEngine;
using UnityEngine.UI;

public class Marking : MonoBehaviour
{
    // �I�u�W�F�N�g���f���J����
    /*[SerializeField] */
    private Camera _targetCamera;

    // �v���C���[
    private Transform _player;

    // UI��\��������ΏۃI�u�W�F�N�g
    [SerializeField] public Transform target;

    // �\������UI
    /*[SerializeField]*/
    private Transform _targetUI;
    /*[SerializeField]*/
    [System.NonSerialized] public Image _targetUIImage;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 _worldOffset;

    [System.NonSerialized] public RectTransform _parentUI;

    bool first = true;
    /*[System.NonSerialized]*/public bool none = false;
    // ���������\�b�h�iPrefab���琶�����鎞�ȂǂɎg���j
    public void Initialize(Transform target, Camera targetCamera = null)
    {
        this.target = target;
        _targetCamera = targetCamera != null ? targetCamera : Camera.main;

        OnUpdatePosition();
    }

    private void Awake()
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (_targetCamera == null)
            _targetCamera = Camera.main;

        _targetUI = this.transform;
        _targetUIImage = GetComponent<Image>();

        // �eUI��RectTransform��ێ�
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
    // UI�̈ʒu�𖈃t���[���X�V
    private void Update()
    {
        OnUpdatePosition();
        if (target == null && first != true) Destroy(this.gameObject);
    }

    // UI�̈ʒu���X�V����
    private void OnUpdatePosition()
    {
        if (none)
        {
            _targetUIImage.enabled = false;
            return;
        }
        var cameraTransform = _targetCamera.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = target.position + _worldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        //_targetUI.gameObject.SetActive(isFront);
        _targetUIImage.enabled = isFront;
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

        // RectTransform�̃��[�J�����W���X�V
        _targetUI.localPosition = uiLocalPos + new Vector2(0, posUI);//80);
        _targetUI.localScale = new Vector2(size, size);//80);
    }
}