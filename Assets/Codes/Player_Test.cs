using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Test : MonoBehaviour
{
    //���o���֘A
    private LineRenderer lineRenderer;

    //���̃L�����𑀍삵�Ă���l����P��
    private int playerNum = 0;

    // �X�s�[�h�ƃW�����v�͂��v���p�e�B�����Ĕh���N���X�Őݒ�\��
    [SerializeField] private int stock = 3;
    [SerializeField] private int hp = 5;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    protected bool isGrounded;
    protected Rigidbody rb;

    // �N�[���_�E������ (�t�B�[���h���V���A���C�Y)
    [SerializeField] private float A_skillCooldownTime = 1.0f;
    [SerializeField] private float D_skillCooldownTime = 1.0f;
    [SerializeField] private float SP1_skillCooldownTime = 5.0f;
    [SerializeField] private float SP2_skillCooldownTime = 7.0f;

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
    protected bool A_canUseSkill = true;
    protected bool D_canUseSkill = true;
    protected bool SP1_canUseSkill = true;
    protected bool SP2_canUseSkill = true;

    //�ꎞ�I�ɓ��͋֎~�ɂ������ۂɎg�p����t���O
    protected bool canMoveInput = true;

    private InputAction L_Stick;
    private InputAction R_Stick;

    //���X�|�[����ɕ����Ă��鎞��
    public float floatTime_Set = 0;
    private float floatTime = 0;

    private StatusUI_Test statusUI_Test;


    // �v���p�e�B�Ńt�B�[���h�𑀍�
    protected virtual int HP
    {
        get { return hp; }
        set { hp = value; }
    }


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

    protected virtual float A_SkillCooldownTime
    {
        get { return A_skillCooldownTime; }
        set { A_skillCooldownTime = value; }
    }

    protected virtual float D_SkillCooldownTime
    {
        get { return D_skillCooldownTime; }
        set { D_skillCooldownTime = value; }
    }

    protected virtual float SP1_SkillCooldownTime
    {
        get { return SP1_skillCooldownTime; }
        set { SP1_skillCooldownTime = value; }
    }

    protected virtual float SP2_SkillCooldownTime
    {
        get { return SP2_skillCooldownTime; }
        set { SP2_skillCooldownTime = value; }
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
        Vector3 L_direction = pointB - pointA;
        Vector3 R_direction = pointC - pointA;
        // �p�x���v�Z
        L_angle = Mathf.Atan2(L_direction.z, L_direction.x) * Mathf.Rad2Deg; // Z�����g���Ċp�x���v�Z
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
                statusUI_Test = obj.GetComponent<StatusUI_Test>();
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

    // �U���X�L������
    public void OnAttack_Skill(InputAction.CallbackContext Attack_Skill)
    {
        if (Attack_Skill.started && A_canUseSkill == true)
        {
            //��������
            A_SkillPush();
        }
        else if (Attack_Skill.canceled && A_canUseSkill == true)
        {
            //��������
            A_SkillRelease();
        }
    }

    //�@�h��X�L������
    public void OnDefense_Skill(InputAction.CallbackContext Deffence_Skill)
    {
        if (canMoveInput == true)
        {
            if (Deffence_Skill.started && D_canUseSkill == true)
            {
                //��������
                D_SkillRelease();
            }
            else if (Deffence_Skill.canceled && D_canUseSkill == true)
            {
                //��������
                D_SkillRelease();
            }
        }
    }

    //�@�X�y�V�����X�L��1����
    public void OnSP1_Skill(InputAction.CallbackContext SP1_Skill)
    {
        if (canMoveInput == true)
        {
            if (SP1_Skill.started && SP1_canUseSkill == true)
            {
                //��������
                SP1_SkillRelease();
            }
            else if (SP1_Skill.canceled && SP1_canUseSkill == true)
            {
                //��������
                SP1_SkillRelease();
            }
        }
    }

    //�@�X�y�V�����X�L��2����
    public void OnSP2_Skill(InputAction.CallbackContext SP2_Skill)
    {
        if (canMoveInput == true)
        {
            if (SP2_Skill.started && SP2_canUseSkill == true)
            {
                //��������
                SP2_SkillRelease();
            }
            else if (SP2_Skill.canceled && SP2_canUseSkill == true)
            {
                //��������
                SP2_SkillRelease();
            }
        }
    }

    // �U���X�L���������ꂽ���̏���
    protected virtual void A_SkillPush()
    {
    }

    // �U���X�L���������ꂽ���̏���
    protected virtual void A_SkillRelease()
    {
    }

    // �h��X�L���������ꂽ���̏���
    protected virtual void D_SkillPush()
    {
    }

    // �h��X�L���������ꂽ���̏���
    protected virtual void D_SkillRelease()
    {
    }

    // �X�y�V�����X�L��1�������ꂽ���̏���
    protected virtual void SP1_SkillPush()
    {
    }

    // �X�y�V�����X�L��1�������ꂽ���̏���
    protected virtual void SP1_SkillRelease()
    {
    }

    // �X�y�V�����X�L��2�������ꂽ���̏���
    protected virtual void SP2_SkillPush()
    {
    }

    // �X�y�V�����X�L��2�������ꂽ���̏���
    protected virtual void SP2_SkillRelease()
    {
    }

    // �U���X�L���̃N�[���_�E������
    protected virtual IEnumerator A_SkillCooldown()
    {
        yield return new WaitForSeconds(A_SkillCooldownTime);
        A_canUseSkill = true;
        Debug.Log("A_Skill ready!");
    }

    // �h��X�L���̃N�[���_�E������
    protected virtual IEnumerator D_SkillCooldown()
    {
        yield return new WaitForSeconds(D_SkillCooldownTime);
        D_canUseSkill = true;
        Debug.Log("D_Skill ready!");
    }

    // �X�y�V�����X�L��1�̃N�[���_�E������
    protected virtual IEnumerator SP1_SkillCooldown()
    {
        yield return new WaitForSeconds(SP1_SkillCooldownTime);
        SP1_canUseSkill = true;
        Debug.Log("SP1_Skill ready!");
    }

    // �X�y�V�����X�L��2�̃N�[���_�E������
    protected virtual IEnumerator SP2_SkillCooldown()
    {
        yield return new WaitForSeconds(SP2_SkillCooldownTime);
        SP2_canUseSkill = true;
        Debug.Log("SP2_Skill ready!");
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

            statusUI_Test.StockMinus(playerNum, stock, hp);
        }
    }
}