using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��Ƀv���C���[�����Ɨ������鏰
public class FallingFloor : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float height; //���ɖ߂鍂�x
    [SerializeField] private float startTime = 3f; //��������܂ł̎���
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if(transform.position.y <= height)
        {
            //�ŏ��̏�Ԃɖ߂�
            rb.isKinematic = true;
            rb.useGravity = false;
            transform.position = startPos;
            col.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling()
    {
        //�R���C�_�[��kinematic��false�ɂ��ė�������
        yield return new WaitForSeconds(startTime);
        rb.isKinematic = false;
        rb.useGravity = true;
        col.enabled = false;
    }
}
