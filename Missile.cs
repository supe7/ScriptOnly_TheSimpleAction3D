using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��C����o��e
public class Missile : MonoBehaviour
{
    [SerializeField] private float speed = 1; //�ړ����x
    [SerializeField] private float dethTime = 5; //������܂ł̎���
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        //�S�[�����邩���񂾂肵���炱�̃I�u�W�F�N�g������
        if (Goal.IsGoal() || Checkpoint.IsDeath())
        {
            Destroy(gameObject);
        }
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(dethTime);
        Destroy(gameObject);
    }
    
}
