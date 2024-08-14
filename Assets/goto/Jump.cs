using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	// Start is called before the first frame update
	// �W�����v����́i������̗́j���`
	[SerializeField] private float jumpForce = 40.0f;

	/// <summary>
	/// Collider�����̃g���K�[�ɓ��������ɌĂяo�����
	/// </summary>
	/// <param name="other">������������̃I�u�W�F�N�g</param>
	private void OnTriggerEnter(Collider other)
	{
		// ������������̃^�O��Player�������ꍇ�����삷��L�����N�^�[��Player�^�O������
		if (other.gameObject.CompareTag("Player"))
		{
			// �������������Rigidbody�R���|�[�l���g���擾���āA������̗͂�������
			other.gameObject.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
		}
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
