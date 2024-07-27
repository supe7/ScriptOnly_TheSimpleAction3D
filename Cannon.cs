using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ݒ肵���e�����Ԋu�Ŕ��˂���
public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject missile; //���˂���e
    [SerializeField] private GameObject effect; //���ˎ��̉�
    [SerializeField] private GameObject generator; //missile�𐶐�������W�Ƃ���I�u�W�F�N�g
    [SerializeField] private float coolTime = 3; //�ҋ@����
    [SerializeField] private AudioClip se; //���ˎ��̌��ʉ�
    private bool playerFlag = false; //�v���C���[�����邩�ǂ���
    private bool shotFlag = true; //���˃t���O
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        //�v���C���[���g���K�[�͈͓̔��ɂ���Δ��˂���
        if (other.gameObject.CompareTag("Player"))
        {
            playerFlag = true;
            if((playerFlag && shotFlag) && !Checkpoint.IsDeath()) 
            {
                StartCoroutine(Fire());
            }
            else
            {
                playerFlag = false;
            }
        }
    }
    IEnumerator Fire()
    {
        shotFlag = false;
        if (shotFlag == false)
        {
            yield return new WaitForSeconds(coolTime);
            shotFlag = true;
        }
        //generator�̍��W����
        Vector3 newPos = generator.transform.position;
        //�v���n�u����Q�[���I�u�W�F�N�g�������
        audioSource.PlayOneShot(se);
        Instantiate(missile, newPos, transform.rotation);
        Instantiate(effect, newPos, transform.rotation);
    }
}
