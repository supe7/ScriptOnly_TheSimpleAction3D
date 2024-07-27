using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�G�t�F�N�g���Đ����I������炱�̃I�u�W�F�N�g������
public class EffectDestroy : MonoBehaviour
{
    new ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //�G�t�F�N�g���Đ������ǂ������m�F
        if (!particleSystem.isPlaying)
        {
            //�Đ����I�������I�u�W�F�N�g������
            Destroy(gameObject);
        }
    }
}
