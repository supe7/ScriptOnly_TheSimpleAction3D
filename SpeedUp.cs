using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��ɏ�����v���C���[������������
public class SpeedUp : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 speed; //�w�肵�������ɉ����x��������
    private Rigidbody rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision) //�v���C���[������Ă�����
    {
        //���x������������
        rb.AddForce(speed, ForceMode.Force);
    }
}
