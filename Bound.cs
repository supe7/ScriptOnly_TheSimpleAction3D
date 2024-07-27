using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��ɏ�����v���C���[�𒵂˂�����
public class Bound : MonoBehaviour
{
    [SerializeField] private Vector3 boundPower; //�w�肵�������Ƀv���C���[���΂�
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip se; //���ʉ����w��
    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�v���C���[������������
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(se);
            rb.AddForce(boundPower, ForceMode.Impulse);
        }
    }
}
