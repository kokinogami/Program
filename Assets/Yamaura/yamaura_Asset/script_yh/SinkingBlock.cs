using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingBlock : MonoBehaviour
{
	private Rigidbody rigidBody;
	//�@�{�b�N�X�̏����ʒu
	private Vector3 defaultPosition;
	//�@�{�b�N�X�̈ړ����x
	private Vector3 velocity;
	//�@��΂����C�̋���
	[SerializeField]
	private float rayDistance = 1f;
	//�@���g�̃R���C�_
	private Collider myCollider;
	//�@�ڐG�𖳎�����R���C�_
	[SerializeField]
	private Collider floorCollider;
	//�@�߂�X�s�[�h
	[SerializeField]
	private float returnSpeed = 0.1f;
	//�@���ރX�s�[�h
	[SerializeField]
	private float sinkingSpeed = 2f;
	//�@�L�����N�^�[���u���b�N�ɏ���Ă��邩�ǂ���
	private bool characterIsOnBoard;
	[SerializeField]
	private Vector3 blockSize = Vector3.one;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		defaultPosition = rigidBody.position;
		myCollider = GetComponent<Collider>();
		//�@���g�̃R���C�_�ƃt���A�̃R���C�_�̐ڐG�����Ȃ��悤�ɂ���
		Physics.IgnoreCollision(myCollider, floorCollider, true);
	}

	// Update is called once per frame
	void Update()
	{
		//�@Box��Player���C���[�����R���C�_�ƐڐG���邩�m�F���A�v���C���[������Ă���Ή������A
		if (!characterIsOnBoard)
		{
			if (Physics.CheckBox(transform.position + Vector3.up * rayDistance, blockSize * 0.52f, Quaternion.identity, LayerMask.GetMask("Player")))
			{
				velocity = Vector3.down * sinkingSpeed;
				characterIsOnBoard = true;
			}
			else
			{
				velocity = Vector3.up * returnSpeed;
			}
			//�@�L�����N�^�[������Ă��鎞
		}
		else
		{
			// �L�����N�^�[������Ă���Ƃ���Ă��鎞�ɁA�{�b�N�X�ƃL�����N�^�[���ڐG���Ă��邩�m�F���A���Ă��Ȃ���Ώ���Ă��Ȃ��ɕύX
			if (!Physics.CheckBox(transform.position + Vector3.up * rayDistance, blockSize * 0.52f, Quaternion.identity, LayerMask.GetMask("Player")))
			{
				characterIsOnBoard = false;
			}
		}
	}
	private void FixedUpdate()
	{
		//�@�ړ���������i���ɖ߂�����j�ŏ����ʒu�ȏ�̏ꍇ�͈ړ������Ȃ�
		if (velocity.y > 0f
			&& rigidBody.position.y >= defaultPosition.y
			)
		{
			//�@���S�Ɉړ�������
			rigidBody.MovePosition(defaultPosition);
			return;
		}
		rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
	}
}