using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�I�u�W�F�N�g��������Ƃ��ɉ���炷�悤�ɂ���
public class AudioObj : MonoBehaviour
{
    private AudioClip se;
    void Start()
    {
        se = GetComponent<AudioSource>().clip;
        Destroy(gameObject, se.length);
    }
}
