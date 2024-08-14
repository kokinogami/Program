using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager_A : MonoBehaviour
{
    // Start is called before the first frame update
    //�@�񓯊�����Ŏg�p����AsyncOperation
    private AsyncOperation async;
    //�@�V�[�����[�h���ɕ\������UI���
    [SerializeField]
    private GameObject loadUI;
    //�@�ǂݍ��ݗ���\������X���C�_�[
    [SerializeField]
    private Slider slider;

    public void NextScene()
    {
        //�@���[�h���UI���A�N�e�B�u�ɂ���
        loadUI.SetActive(true);

        //�@�R���[�`�����J�n
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData()
    {
        // �V�[���̓ǂݍ��݂�����
        async = SceneManager.LoadSceneAsync("Goto_Stage3");

        //�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
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
