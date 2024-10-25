using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //���o���֘A
    private LineRenderer lineRenderer;

    //���̃L�����𑀍삵�Ă���l����P��
    private int playerNum = 0;

    // �X�s�[�h�ƃW�����v�͂��v���p�e�B�����Ĕh���N���X�Őݒ�\��
    [SerializeField] private int stock = 3;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    protected bool isGrounded;
    protected Rigidbody rb;

    // �X�L��1�ƃX�L��2�̃N�[���_�E������ (�t�B�[���h���V���A���C�Y)
    [SerializeField] private float skill1CooldownTime = 5.0f;
    [SerializeField] private float skill2CooldownTime = 7.0f;

    //L�X�e�B�b�N�̍��W��\��
    protected Vector2 LS_Input;
    protected float LS_Horizontal, LS_Vertical;
    //R�X�e�B�b�N�̍��W��\��
    protected Vector2 RS_Input;
    protected float RS_Horizontal, RS_Vertical;

    //�������o�����߂̂���
    private Vector3 pointA = new Vector3(0, 0, 0);  //���g�̈ʒu
    private Vector3 pointB = new Vector3(0, 0, 0);  //L�X�e�B�b�N�̍��W
    private Vector3 pointC = new Vector3(0, 0, 0);  //R�X�e�B�b�N�̍��W
    protected float L_angle = 0;
    protected float R_angle = 0;

    //�X�L�����g�p����ۂɂ݂�t���O
    protected bool canUseSkill1 = true;
    protected bool canUseSkill2 = true;

    //�ꎞ�I�ɓ��͋֎~�ɂ������ۂɎg�p����t���O
    protected bool canMoveInput = true;

    private InputAction L_Stick;
    private InputAction R_Stick;

    //���X�|�[����ɕ����Ă��鎞��
    public float floatTime_Set = 0;
    private float floatTime = 0;

    private StatusUI statusUI;


    // �v���p�e�B�Ńt�B�[���h�𑀍�
    protected virtual float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    protected virtual float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    protected virtual float Skill1CooldownTime
    {
        get { return skill1CooldownTime; }
        set { skill1CooldownTime = value; }
    }

    protected virtual float Skill2CooldownTime
    {
        get { return skill2CooldownTime; }
        set { skill2CooldownTime = value; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        LS_Input = Vector2.zero;
        RS_Input = Vector2.zero;

        L_Stick = GetComponent<PlayerInput>().actions["L_Stick"];
        R_Stick = GetComponent<PlayerInput>().actions["R_Stick"];

        rb = GetComponent<Rigidbody>();
        NameSet();


        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMoveInput == true)
        {
            if (L_Stick.ReadValue<Vector2>() == Vector2.zero)
            {
                LS_Input = Vector2.zero;
            }

            if (R_Stick.ReadValue<Vector2>() == Vector2.zero)
            {
                RS_Input = Vector2.zero;
            }

            if (L_Stick.ReadValue<Vector2>() != Vector2.zero)
            {
                LS_Input = L_Stick.ReadValue<Vector2>();
            }

            if (R_Stick.ReadValue<Vector2>() != Vector2.zero)
            {
                RS_Input = R_Stick.ReadValue<Vector2>();
            }

            LS_Horizontal = LS_Input.x;
            LS_Vertical = LS_Input.y;

            RS_Horizontal = RS_Input.x;
            RS_Vertical = RS_Input.y;
        }
    }

    // �ړ�����
    protected virtual void FixedUpdate()
    {
        HandleMovement();

        //�����v�Z
        pointB = new Vector3(LS_Horizontal, 0, LS_Vertical);
        pointC = new Vector3(RS_Horizontal, 0, RS_Vertical);
        // �x�N�g���̌v�Z
        Vector3 direction = pointB - pointA;
        Vector3 R_direction = pointC - pointA;
        // �p�x���v�Z
        L_angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // Z�����g���Ċp�x���v�Z
        R_angle = Mathf.Atan2(R_direction.z, R_direction.x) * Mathf.Rad2Deg; // Z�����g���Ċp�x���v�Z

        Quaternion targetRotation = Quaternion.Euler(0, -L_angle + 90, 0);
        this.transform.rotation = targetRotation;


        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position + pointC * 5);

        if (floatTime > 0)
        {
            floatTime -= Time.deltaTime;
            Vector3 newPosition = this.transform.position;
            newPosition.y = 4;
            transform.position = newPosition;
        }
    }

    private void NameSet()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        // ���O����v����I�u�W�F�N�g�����݂��邩�`�F�b�N

        bool[] exist = new bool[4];

        exist[0] = false;
        exist[1] = false;
        exist[2] = false;
        exist[3] = false;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Player1")
            {
                exist[0] = true;
            }
            else if (obj.name == "Player2")
            {
                exist[1] = true;
            }
            else if (obj.name == "Player3")
            {
                exist[2] = true;
            }
            else if (obj.name == "Player4")
            {
                exist[3] = true;
            }
            else if (obj.name == "StatusUI")
            {
                statusUI = obj.GetComponent<StatusUI>();
            }
        }

        if (exist[0] == false)
        {
            this.name = "Player1";
            playerNum = 1;
            return;
        }

        if (exist[1] == false)
        {
            this.name = "Player2";
            playerNum = 2;
            return;
        }

        if (exist[2] == false)
        {
            this.name = "Player3";
            playerNum = 3;
            return;
        }

        if (exist[3] == false)
        {
            this.name = "Player4";
            playerNum = 4;
            return;
        }
    }

    // �ړ����\�b�h
    protected void HandleMovement()
    {
        Vector3 movement = new Vector3(LS_Horizontal, 0.0f, LS_Vertical);
        transform.Translate(movement * Speed * Time.deltaTime, Space.World); // Speed���g�p
    }

    // Jump����
    public void OnJump(InputAction.CallbackContext Jump)
    {
        if (canMoveInput == true)
        {
            if (Jump.started && isGrounded)
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); // JumpForce���g�p
                isGrounded = false;
            }
        }
    }

    //Skill1����
    public void OnSkill1(InputAction.CallbackContext Skill1)
    {
        if (canMoveInput == true)
        {
            if (Skill1.started && canUseSkill1 == true)
            {
                //��������
                Skill1Push();
            }
            else if (Skill1.canceled && canUseSkill2 == true)
            {
                //��������
                Skill1Release();
            }
        }
    }

    //Skill2����
    public void OnSkill2(InputAction.CallbackContext Skill2)
    {
        if (canMoveInput == true)
        {
            if (Skill2.started && canUseSkill2 == true)
            {
                //��������
                Skill2Push();
            }
            else if (Skill2.canceled && canUseSkill2 == true)
            {
                //��������
                Skill2Release();
            }
        }
    }

    // �X�L��1�������ꂽ���̏���
    protected virtual void Skill1Push()
    {
    }

    // �X�L��1�������ꂽ���̏���
    protected virtual void Skill1Release()
    {
    }

    // �X�L��2�������ꂽ���̏���
    protected virtual void Skill2Push()
    {
    }

    // �X�L��2�������ꂽ���̏���
    protected virtual void Skill2Release()
    {
    }

    // �X�L��1�̃N�[���_�E������
    protected virtual IEnumerator Skill1Cooldown()
    {
        yield return new WaitForSeconds(Skill1CooldownTime);
        canUseSkill1 = true;
        Debug.Log("Skill 1 ready!");
    }

    // �X�L��2�̃N�[���_�E������
    protected virtual IEnumerator Skill2Cooldown()
    {
        yield return new WaitForSeconds(Skill2CooldownTime);
        canUseSkill2 = true;
        Debug.Log("Skill 2 ready!");
    }

    // �n�ʂɐڐG�����Ƃ��ɌĂ΂��
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("DeadLine"))
        {
            if (stock > 0)
            {
                stock--;
                floatTime = floatTime_Set;
            }
            else if (stock <= 0)
            {
                this.gameObject.SetActive(false);
            }

            statusUI.StockMinus(playerNum, stock);
        }
    }
}