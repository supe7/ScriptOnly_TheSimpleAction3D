using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̃x�N�g�������Z�b�g����
public class VelocityReset : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody rb;
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���������v���C���[�̃x�N�g����0�ɂ���
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector3.zero; 
        }
    }
}
