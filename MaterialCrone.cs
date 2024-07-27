using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�V�F�[�_�[�̉摜�𗐂�Ȃ��悤�ɂ���
public class MaterialCrone : MonoBehaviour
{
    private Material mat; //�}�e���A�����擾
    private MeshRenderer mesh; //���b�V�����擾
    [SerializeField] private Vector2 Tlim = new Vector2(1, 1); //�g���~���O���鐔�l

    void Start()
    {
        //�}�e���A���̃N���[���𐶐����A�w�肵�����l�Ńg���~���O����
        mesh = GetComponent<MeshRenderer>();
        mat = Instantiate(mesh.material);
        mat.SetVector("_Tlim", Tlim);
        mat.SetVector("_Scroll", mat.GetVector("_Scroll") / Tlim.x);
        mesh.material = mat;
    }
}
