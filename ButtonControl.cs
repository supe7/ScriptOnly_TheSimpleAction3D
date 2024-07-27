using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//�{�^���֘A�̏���
public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject activeObj = null; //�\���������I�u�W�F�N�g
    [SerializeField] private GameObject negativeObj = null; //��\���ɂ������I�u�W�F�N�g
    [SerializeField] private GameObject button; //�C�x���g�V�X�e���ő��삷��{�^��
    [SerializeField] private GameObject audioObj; //����炷�p�̃I�u�W�F�N�g
    [SerializeField] private string changeSceneName = null; //�؂�ւ���̃V�[����
    [SerializeField] private float changeTime = 0.5f; //�؂�ւ��܂ł̎���
    [SerializeField] private AudioClip se;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnObj()
    {
        //��������I�u�W�F�N�g��\������
        audioSource.PlayOneShot(se);
        activeObj.SetActive(true);
        Instantiate(audioObj);
    }

    public void OffObj()
    {
        //��������I�u�W�F�N�g���\���ɂ���
        EventSystem.current.SetSelectedGameObject(button);
        audioSource.PlayOneShot(se);
        negativeObj.SetActive(false);
        Instantiate(audioObj);
    }

    public void ChangeScene()
    {
        //�V�[���̐؂�ւ�
        audioSource.PlayOneShot(se);
        StartCoroutine(Change());
    }
    IEnumerator Change()
    {
        yield return new WaitForSecondsRealtime(changeTime);
        SceneManager.LoadScene(changeSceneName);
    }

    public void QuitGame()
    {
        //�Q�[������߂�
        Application.Quit();
    }
}
