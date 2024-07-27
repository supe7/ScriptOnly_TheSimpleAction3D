using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����n�_���L�^����
public class Checkpoint : MonoBehaviour
{
    static Checkpoint I;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkPoints; //�`�F�b�N�|�C���g�ɂ���I�u�W�F�N�g��o�^
    [SerializeField] private int lifePoint = 3; //�c�@�̐�
    [SerializeField] private Vector3 vectorPoint; //�����n�_
    [SerializeField] private float dead; //���ʍ���
    [SerializeField] private float offTime = 3f; //�e�L�X�g���\���ɂ���܂ł̎���
    [SerializeField] private AudioClip checkSe; //�`�F�b�N�|�C���g�ɐG�ꂽ���̌��ʉ�
    [SerializeField] private AudioClip deathSe; //����Ń��C�t�����鎞�ɖ���ʉ�
    private bool deathFlag = false;//���񂾂��ǂ���
    private bool hitFlag = false; //�G�ɓ���������
    private bool checkFlag = false; //�`�F�b�N�|�C���g�ɐG�ꂽ���ǂ���
    private Rigidbody rb;
    private AudioSource audioSource;
    private PlayerCamera playerCamera;
    void Start()
    {
        I = this;
        rb = player.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<PlayerCamera>();
    }
    public static bool IsDeath()
    {
        return I.deathFlag;
    }
    public static bool IsCheck()
    {
        return I.checkFlag;
    }
    public static int Life()
    {
        return I.lifePoint;
    }
    //��������Ƃ��ɋN���鏈��
    private void Revival()
    {
        rb.velocity = Vector3.zero; //�x�N�g����0�ɂ���
        player.transform.position = vectorPoint; //�v���C���[�𕜊��n�_�ɖ߂�
        player.transform.rotation = Quaternion.identity; //�v���C���[�𐳖ʂɌ�����
        playerCamera.ResetCamera(); //�J�����̌����𐳖ʂɂ���
        lifePoint--; //���C�t�����炷
        audioSource.PlayOneShot(deathSe);
    }
    
    void Update()
    {
        if(deathFlag)
        {
            Revival();
            deathFlag = false;
        }
        if(player.transform.position.y < -dead || hitFlag)            
        {
            deathFlag = true;
            hitFlag = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�`�F�b�N�|�C���g�ɐG�ꂽ��
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            vectorPoint = player.transform.position; //�����n�_���X�V
            lifePoint++; //���C�t�𑝂₷
            checkFlag = true;
            audioSource.PlayOneShot(checkSe);
            if (checkFlag)
            {
                StartCoroutine(CheckTextOff());
            }
        }
        //�G�ɓ���������
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitFlag = true;
        }
    }

    IEnumerator CheckTextOff()
    {
        yield return new WaitForSeconds(offTime);
        checkFlag = false;
    }
}
