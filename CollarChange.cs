using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`�F�b�N�|�C���g�̐F��ς��A�����蔻�����������
public class CollarChange : MonoBehaviour
{
    [SerializeField] private Material mat = null; //�}�e���A�����w��
    private Collider col;
    void Start()
    {
        col = GetComponent<Collider>();
        mat = GetComponent<MeshRenderer>().sharedMaterial;
        mat.color = new Color(1.0f, 0.92f, 0.016f, 1.0f); //���F�ɐݒ肷��
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            mat.color = new Color(1.0f, 0.0f, 0.0f, 0.7f); //�������ȐԐF�ɂȂ�
            col.enabled = false; //�R���C�_�[���I�t�ɂ���
        }
    }
}
