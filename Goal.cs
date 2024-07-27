using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//�S�[���������̏���
public class Goal : MonoBehaviour
{
    [SerializeField] private AudioClip se; //�S�[�����̌��ʉ�
    [SerializeField] GameObject[] goalEffect; //�S�[�����ɏo���G�t�F�N�g
    static bool goalFlag  = false;
    private AudioSource audioSource;
    void Start()
    {
        foreach(GameObject obj in goalEffect)
        {
            obj.SetActive(false);
        }
        goalFlag = false;
        audioSource = GetComponent<AudioSource>();
    }
    static public bool IsGoal()
    {
        return goalFlag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goalFlag = true;
            audioSource.PlayOneShot(se);
            //�S�[��������G�t�F�N�g���I���ɂ���
            foreach (GameObject obj in goalEffect)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            goalFlag = false;
        }
    }
}
