using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[��ǐՂ���e
public class Horming : MonoBehaviour
{
    [SerializeField] private float speed = 1; //�ړ����x
    [SerializeField] private float dethTime = 5; //������܂ł̎���
    private Vector3 v = Vector3.zero; //��U�x�N�g����0�ɂ���
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * (speed / 2) * Time.deltaTime, ForceMode.Impulse);
    }

    void LateUpdate()
    {
        HormingMissile();
        //�S�[�����邩���񂾂肵���炱�̃I�u�W�F�N�g������
        if (Goal.IsGoal()|| Checkpoint.IsDeath())
        {
            Destroy(gameObject);
        }
    }

    private void HormingMissile()
    {
        /*
        �v���C���[�̍��W�[���̃I�u�W�F�N�g�̍��W������
        �����ۂ����܂ܒ����𐳋K������1�ɂ���
        ��Ƀu���[�L�������Ȃ���v���C���[�Ɍ��������x�����Z
        �ݒ肵�����ԂɂȂ�Ə�����
         */
        var D = Player.GetPos() - transform.position;
        var d = D.normalized;
        v = speed * Time.deltaTime * d;
        transform.forward = rb.velocity;
        rb.AddForce(v - rb.velocity * 0.1f, ForceMode.Force);
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(dethTime);
        Destroy(gameObject);
    }
}
