using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���C���[���ǂɈ���������Ȃ��悤�ɂ���
public class NoPhysics : MonoBehaviour
{
    [SerializeField] private PhysicMaterial pm; //�Ǘp��PhysicMaterial
    private Collider col;

    void Start()
    {
        //�I�u�W�F�N�g�̊p�x��z90�xor-90�x�Ȃ�PhysicMaterial��ݒ�
        if(transform.eulerAngles.z == 90 || transform.eulerAngles.z == -90)
        {
            col = GetComponent<Collider>();
            col.material = pm;
        }
    }
}
