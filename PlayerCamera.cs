using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//���C���J�����̑���
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target; //��ʑ̂��w��
    [SerializeField] private float distanceToPlayer = 2.0f; //�J�����ƃv���C���[�Ƃ̋���
    [SerializeField] private float slideDistance; //�J���������ɃX���C�h������G�v���X�̎��E�ցC�}�C�i�X�̎�����
    [SerializeField] private float height = 1.2f; //�����_�̍���
    [SerializeField] private float sensitivity = 10f; //���x
    [SerializeField] private float downLimit = 0.3f; //�������̏���l
    [SerializeField] private float upLimit = -0.8f; //������̏���l
    private GameObject targetObj;
    private Vector3 targetPos;

    //InputActions�ɐݒ肵���L�[�̏��
    private InputAction lookAction, resetActionR, resetActionL;
    private Vector2 look;

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        lookAction = actionMap["Look"];
        resetActionR = actionMap["CameraResetRight"];
        resetActionL = actionMap["CameraResetLeft"];

        if (target == null)
        {
            Debug.LogError("�^�[�Q�b�g���ݒ肳��Ă��Ȃ�");
            Application.Quit();
        }
        targetObj = GameObject.Find("Player");
        targetPos = targetObj.transform.position;
    }
    void Update()
    {
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
    }

    private void FixedUpdate()
    {
        look = lookAction.ReadValue<Vector2>();
        var rotX = look.x * Time.deltaTime * sensitivity;
        var rotY = look.y * Time.deltaTime * sensitivity;
        var lookAt = target.position + Vector3.up * height;

        //��]
        transform.RotateAround(lookAt, Vector3.up, rotX);
        //�J�������v���C���[�̐^���^���ɂ���Ƃ��ɂ���ȏ��]�����Ȃ��悤�ɂ���
        if (transform.forward.y > downLimit && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y < upLimit && rotY > 0)
        {
            rotY = 0;
        }
        transform.RotateAround(lookAt, transform.right, rotY);

        //�J�����ƃv���C���[�Ƃ̊Ԃ̋����𒲐�
        transform.position = lookAt - transform.forward * distanceToPlayer;
        //�����_�̐ݒ�
        transform.LookAt(lookAt);
        //�J���������ɂ��炵�Ē������J����
        transform.position = transform.position + transform.right * slideDistance;

        
    }

    private void LateUpdate()
    {
        if ((resetActionR.IsPressed() && resetActionL.IsPressed()))
        {
            ResetCamera();
        }
    }

    public void ResetCamera()
    {
        //�J�����̌������v���C���[�̐��ʕ����Ɍ�������
        Vector3 direction = target.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
