using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
//�v���C���[�̈ړ��ȂǂɊւ��鏈��
public class Player : MonoBehaviour
{
    static Player I;
    [SerializeField] private float speed = 475f; //�ړ����x
    [SerializeField] private float dashSpeed = 675f; //�_�b�V�������Ƃ��̑���
    [SerializeField] private float jumpPower = 420f; //�W�����v��
    [SerializeField] private float rotateSpeed = 90f; //��]���x
    [SerializeField] private AudioClip se; //���ʉ����w��
    [SerializeField] private GameObject jumpEffect; //�W�����v����������G�t�F�N�g
    [SerializeField] private GameObject effectGenerator; //�G�t�F�N�g�𐶐�����ʒu
    [SerializeField] private GameObject panel; //�|�[�Y��ʂɂ���p�l�����w��
    [SerializeField] private GameObject pauseButton; //�|�[�Y���ɑ���ł���{�^��
    [SerializeField] private GameObject mainButton; //�Q�[���I�[�o�[���̃{�^��
    [SerializeField] private PhysicMaterial pm; //�v���C���[�p��PhysicMaterial
    private CapsuleCollider cCol;

    //InputActions�ɐݒ肵���L�[�̏��
    private InputAction jumpAction,
                        dashAction,
                        pauseAction,
                        stickMoveAction;

    private bool jumpFlag = false; //�W�����v��Ԃ��ǂ���
    private bool dashFlag = false; //�_�b�V����Ԃ��ǂ���
    private bool pushPauseFlag = false; //�|�[�Y�{�^���������ꂽ���ǂ���
    private bool pauseFlag = false; //�|�[�Y��ʂ��J���Ă��邩
    private bool groundFlag = false; //�n�ʂɐG��Ă��邩�ǂ���

    private Transform mainCam; //���C���J������Transform���擾
    private Rigidbody rb;
    private AudioSource audioSource;
    //�����̍��W���擾����
    static public Vector3 GetPos()
    {
        if (I == null)
        {
            return Vector3.zero;
        }
        return I.transform.position;
    }
    private void Awake()
    {
        //InputActions�ɓo�^����Ă�����͏����擾
        var playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        jumpAction = actionMap["Jump"];
        dashAction = actionMap["Dash"];
        pauseAction = actionMap["Pause"];
        stickMoveAction = actionMap["Move"];
    }
    void Start()
    {
        Time.timeScale = 1;
        I = this;
        mainCam = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        cCol = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        //���͂��󂯎��A�t���O�𑀍삷��
        InputDash();
        Pause();
        if(!pauseFlag)
        {
            Jump();
        }
        //�ڒn���ĂȂ����PhysicMaterial��ݒ�
        if (!groundFlag)
        {
            cCol.material = pm;
        }
        else
        {
            cCol.material = null;
        }
    }

    private void FixedUpdate()
    {
        //�v���C���[�̋��������i���ۂɓ����Ƃ���j
        Move();
    }
    private void InputDash()
    {
        //�_�b�V���̓���
        dashFlag = dashAction.IsPressed();
    }
    private void Move()
    {
        //���X�e�B�b�N�̓��͏����擾
        Vector2 ls = stickMoveAction.ReadValue<Vector2>();

        //�J�����̕������擾���AY���̉�]�𖳎�
        Vector3 cameraForward = mainCam.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize(); //�J�����̌����𐳋K��

        //�v���C���[���J�����̌����Ă�������Ɉړ�������
        Vector3 movement = cameraForward * ls.y + mainCam.right * ls.x;
        movement.Normalize();

        rb.AddForce(movement * (dashFlag ? dashSpeed : speed) * Time.fixedDeltaTime, ForceMode.Force);
        //�[���łȂ��ړ�������ꍇ
        if (movement.magnitude > 0.1f)
        {
            //�J�����̌����Ɋ�Â��Ĉړ�������ݒ�
            Vector3 lookDirection = Vector3.ProjectOnPlane(movement, Vector3.up);
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
            }
        }

    }

    private void Jump()
    {
        jumpFlag = jumpAction.triggered;
        if (jumpFlag && groundFlag)
        {
            //�����p�̃I�u�W�F�N�g�̍��W�������G�t�F�N�g�𐶐�����
            Vector3 newPos = effectGenerator.transform.position;
            jumpFlag = false;
            audioSource.PlayOneShot(se);
            Instantiate(jumpEffect, newPos, jumpEffect.transform.rotation);
            rb.AddForce(transform.up * jumpPower * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }
    private void Pause()
    {
        //�|�[�Y��ʂ��J��������x�����������
        pushPauseFlag = pauseAction.triggered;
        if (!pauseFlag && pushPauseFlag)
        {
            pauseFlag = true;
            EventSystem.current.SetSelectedGameObject(pauseButton);
            Time.timeScale = 0;
            panel.SetActive(true);
        }
        else if (pauseFlag && pushPauseFlag)
        {
            pauseFlag = false;
            Time.timeScale = 1;
            EventSystem.current.SetSelectedGameObject(mainButton);
            panel.SetActive(false);
        }

    }
    private void OnTriggerStay(Collider other) 
    {
        //�n�ʂɐG�ꂽ��ڒn�t���O��true�ɂ���
        if (other.gameObject.CompareTag("Ground"))
        {
            groundFlag = true;
        }

    }
    private void OnTriggerExit(Collider other) 
    {
        //�����G��Ȃ�������ڒn�t���O��false�ɂ���
        groundFlag = false;
    }
}
