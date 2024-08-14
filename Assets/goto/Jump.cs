using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	// Start is called before the first frame update
	// ジャンプする力（上向きの力）を定義
	[SerializeField] private float jumpForce = 40.0f;

	/// <summary>
	/// Colliderが他のトリガーに入った時に呼び出される
	/// </summary>
	/// <param name="other">当たった相手のオブジェクト</param>
	private void OnTriggerEnter(Collider other)
	{
		// 当たった相手のタグがPlayerだった場合←操作するキャラクターにPlayerタグをつける
		if (other.gameObject.CompareTag("Player"))
		{
			// 当たった相手のRigidbodyコンポーネントを取得して、上向きの力を加える
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
