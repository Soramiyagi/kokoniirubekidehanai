using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // �X�s�[�h�ƃW�����v�͂��v���p�e�B�����Ĕh���N���X�Őݒ�\��
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    protected bool isGrounded;
    protected Rigidbody rb;

    // �X�L��1�ƃX�L��2�̃N�[���_�E������ (�t�B�[���h���V���A���C�Y)
    [SerializeField] private float skill1CooldownTime = 5.0f;
    [SerializeField] private float skill2CooldownTime = 7.0f;

    //L�X�e�B�b�N�̍��W��\��
    protected float moveHorizontal, moveVertical;
    //R�X�e�B�b�N�̍��W��\��
    protected float RS_Horizontal, RS_Vertical;

    //�������o�����߂̂���
    private Vector3 pointA = new Vector3(0, 0, 0);  //���g�̈ʒu
    private Vector3 pointB = new Vector3(0, 0, 0);  //L�X�e�B�b�N�̍��W
    private Vector3 pointC = new Vector3(0, 0, 0);  //R�X�e�B�b�N�̍��W
    protected float angle = 0;
    protected float R_angle = 0;

    //�X�L�����g�p����ۂɂ݂�t���O
    protected bool canUseSkill1 = true;
    protected bool canUseSkill2 = true;

    //�ꎞ�I�ɓ��͋֎~�ɂ������ۂɎg�p����t���O
    protected bool canMoveInput = true;

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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    // �ړ�����
    protected virtual void FixedUpdate()
    {
        HandleMovement();

        //�����v�Z
        pointB = new Vector3(moveHorizontal, 0, moveVertical);
        pointC = new Vector3(RS_Horizontal, 0, RS_Vertical);
        // �x�N�g���̌v�Z
        Vector3 direction = pointB - pointA;
        Vector3 R_direction = pointC - pointA;
        // �p�x���v�Z
        angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // Z�����g���Ċp�x���v�Z
        R_angle = Mathf.Atan2(R_direction.z, R_direction.x) * Mathf.Rad2Deg; // Z�����g���Ċp�x���v�Z

        Quaternion targetRotation = Quaternion.Euler(0, -angle + 90, 0);
        this.transform.rotation = targetRotation;
    }    

    // �ړ����\�b�h
    protected void HandleMovement()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * Speed * Time.deltaTime, Space.World); // Speed���g�p
    }

    // ���X�e�B�b�N�ňړ�
    public void OnMove(InputAction.CallbackContext Move)
    {
        if (canMoveInput == true)
        {
            Vector2 movementInput = Move.ReadValue<Vector2>();
            moveHorizontal = movementInput.x;
            moveVertical = movementInput.y;
        }
    }


    // �E�X�e�B�b�N���͂̎擾
    public void OnRightStick(InputAction.CallbackContext RightStick)
    {
        if (canMoveInput == true)
        {
            Vector2 RS_Input = RightStick.ReadValue<Vector2>();
            RS_Horizontal = RS_Input.x;
            RS_Vertical = RS_Input.y;
        }
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
            else if (Skill1.canceled)
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
            else if (Skill2.canceled)
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
    }
}
