using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�I�u�W�F�N�g�̉������]
public class ForeverMove : MonoBehaviour
{
    [SerializeField] private Vector3 angle; //��]�p�x���w��
    [SerializeField] private Vector3 speed; //�ړ����鑬�x
    [SerializeField] private float second = 1f;//��������܂łɂ����鎞��
    private float time = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        RoundTripMove();
        Rotation();
    }

    private void RoundTripMove()
    {
        //�����ړ�
        time += Time.fixedDeltaTime;
        float s = Mathf.Sin(time * Mathf.PI / second);
        Vector3 moveVelocity = speed * s * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + moveVelocity);
    }

    private void Rotation()
    {
        //��]
        Quaternion deltaRotation = Quaternion.Euler(angle * Time.fixedDeltaTime);
        rb.MoveRotation(transform.rotation * deltaRotation);
    }
}
