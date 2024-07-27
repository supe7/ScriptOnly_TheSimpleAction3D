using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//�Q�[���S�̂̏������Ǘ�����
public class Gamemanager : MonoBehaviour
{
    static Gamemanager I;
    [SerializeField] Checkpoint checkpoint;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] GameObject player;//�v���C���[
    [SerializeField] GameObject panel; //�p�l��
    [SerializeField] Text goalText; //�S�[���̃e�L�X�g
    [SerializeField] Text timerText;//�^�C�}�[�̃e�L�X�g
    [SerializeField] Text lifeText; //�c�@�̃e�L�X�g
    [SerializeField] Text checkText; //�`�F�b�N�|�C���g�ڐG���̃e�L�X�g
    [SerializeField] Image keyboardImage; //�L�[�{�[�h����̉摜
    [SerializeField] Image controllerImage; //�R���g���[���[����̉摜
    [SerializeField] private int timeLimit; //�^�C�����~�b�g��ݒ�
    [SerializeField] private string sceneName; //�؂芷����̃V�[����
    [SerializeField] private float changeTime = 3; //�V�[���̐؂�ւ�����
    [SerializeField] private Vector3 startPosition = new Vector3(0, 1, 0);//�v���C���[�̏����ʒu
    private float time;//�o�ߎ���
    private int remainingTime;//�������������݂̎���
    
    void Awake()
    {
        Application.targetFrameRate = 80; //�t���[�������Œ�
        QualitySettings.vSyncCount = 0; //�r���h���ɕi���������Ȃ��悤�ɂ���
    }
    void Start()
    {
        I = this;
        checkpoint.enabled = true;
        playerCamera.enabled = true;
        goalText.enabled = false;
        checkText.enabled = false;
        player.transform.position = startPosition; //�v���C���[���w�肵�������ʒu��
        panel.SetActive(false);
    }
    void Update()
    {
        Timer();
        lifeText.text = "LIFE:" + Checkpoint.Life();
        if (Goal.IsGoal())
        {
            GameClear();
        }
        //���C�t��0�ȉ��ɂȂ邩�������Ԃ�0�ȉ��ɂȂ��
        if (Checkpoint.Life() <= 0 || remainingTime <= 0)
        {
            GameOver();
        }
        if(Checkpoint.IsCheck())
        {
            checkText.enabled = true;
        }
        else
        {
            checkText.enabled = false;
        }
    }

    //�������Ԃ������Ă����\������
    private void Timer()
    {
        time -= Time.deltaTime;
        int remaining = timeLimit + (int)time;
        timerText.text = "TIME:" + remaining.ToString("D4");
        remainingTime = remaining;
    }

    public static int ReturnTime()
    {
        return I.remainingTime;
    }

    //�Q�[���I�[�o�[���̏���
    private void GameOver()
    {
        panel.SetActive(true);
        player.SetActive(false);
        timerText.enabled = false;
        lifeText.enabled = false;
        keyboardImage.enabled = false;
        controllerImage.enabled = false;
        playerCamera.enabled = false;
    }
    //�Q�[���N���A���̏���
    private void GameClear()
    {
        checkpoint.enabled = false;
        goalText.enabled = true;
        timerText.enabled = false;
        lifeText.enabled = false;
        keyboardImage.enabled = false;
        controllerImage.enabled = false;
        time = 0;
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(changeTime);
        SceneManager.LoadScene(sceneName);
    }
}
