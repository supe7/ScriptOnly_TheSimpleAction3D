using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�������ŏ㉺�ɓ���
public class SinMove : MonoBehaviour
{
    [SerializeField] private float roundTripSpeed = 0.5f; //�������x
    [SerializeField] private float rotateSpeed = 90f; //��]���x
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        //��葬�x�ŉ������A��]������
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time) * roundTripSpeed;
        transform.Rotate(transform.up * rotateSpeed * Time.fixedDeltaTime);
    }
}
