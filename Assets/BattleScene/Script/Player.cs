using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject Icons;
    [SerializeField] private GameObject[] Icon = new GameObject[4];
    [SerializeField] private GameObject FixSphere;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] protected AudioClip[] SE = new AudioClip[0];

    //���o���֘A
    private LineRenderer lineRenderer;

    //���̃L�����𑀍삵�Ă���l����P��
    private int playerNum = 0;

    // �p�����[�^�[���v���p�e�B�����Ĕh���N���X�Őݒ�\��
    [SerializeField] private int stock = 3;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    //���񂾎��̕��A���Ԓ��̈ړ����x���グ��Ƃ��Ɏg�p
    //�����Ɍ��X��speed���ꎞ�I�ɕۑ�����
    private float speedTemp = 0;

    //L�X�e�B�b�N���X���Ă��邩�ǂ���
    private int L_Stick_Inclination = 0;
    private int R_Stick_Inclination = 0;

    protected bool isGrounded;
    protected Rigidbody rb;
    protected Collider col;

    // �X�L��1�ƃX�L��2�̃N�[���_�E������ (�t�B�[���h���V���A���C�Y)
    [SerializeField] private float skill1CooldownTime = 5.0f;
    [SerializeField] private float skill2CooldownTime = 7.0f;

    //�X�L���g�p���̃A�j���[�V�������ɑ�����󂯕t���Ȃ��Ȃ鎞��
    [SerializeField] private float skill1AT_Push = 1.0f;
    [SerializeField] private float skill2AT_Push = 1.0f;
    private float jumpPush = 0.7f;
    protected bool duringAnima = false;

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
    protected bool canJump = true;

    //�ꎞ�I�ɓ��͋֎~�ɂ������ۂɎg�p����t���O
    protected bool canMoveInput = false;

    private InputAction L_Stick;
    private InputAction R_Stick;

    //���X�|�[����ɕ����Ă��鎞��
    public float floatTime_Set = 0;
    protected float floatTime = 0;

    // �o�C���h�e�ɓ����������̍S������
    public float bindTime_Set = 0;
    private float bindTime = 0;

    //�A���Ŏ��ʂ��Ƃ�h������
    private float deathInterval = 0;

    //�A���ŃW�����v���邱�Ƃ�h������
    private bool jumpInterval = false;
    //�W�����v���Ă��邩���m�F
    private bool isJumping = false;

    //�A���̓��͂�h������
    private bool skill1InputStop = false;
    private bool skill2InputStop = false;

    //�X�e�[�^�X�p
    [SerializeField] private GameObject statusObj;
    private Status statusScript;

    //�Q�[���}�l�[�W���[�ւ̃A�N�Z�X�p
    private GameManager gameManager;

    //�X�L���̃Q�[�W
    [SerializeField] private GameObject GaugeObj;
    private Gauge gaugeScript;

    //�Q�[���̐i�s�ɂ�鑀��s�\�t���O
    protected bool systemStop = true;

    //Pause��ʂ𓮂����邩�ǂ����̃t���O
    private bool pauseControl = false;

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

        FirstSet();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMoveInput == true)
        {
            if (L_Stick.ReadValue<Vector2>() == Vector2.zero)
            {
                L_Stick_Inclination = 0;
            }

            if (R_Stick.ReadValue<Vector2>() == Vector2.zero)
            {
                R_Stick_Inclination = 0;
                RS_Input = Vector2.zero;
            }

            if (L_Stick.ReadValue<Vector2>() != Vector2.zero)
            {
                L_Stick_Inclination = 1;
                LS_Input = L_Stick.ReadValue<Vector2>();
            }

            if (R_Stick.ReadValue<Vector2>() != Vector2.zero)
            {
                R_Stick_Inclination = 1;
                RS_Input = R_Stick.ReadValue<Vector2>();
            }

            LS_Horizontal = LS_Input.x;
            LS_Vertical = LS_Input.y;

            RS_Horizontal = RS_Input.x;
            RS_Vertical = RS_Input.y;
        }

        if (pauseControl == true)
        {
            if (L_Stick.ReadValue<Vector2>().y > 0)
            {
                gameManager.MenuControl(0);
            }
            else if (L_Stick.ReadValue<Vector2>().y < 0)
            {
                gameManager.MenuControl(1);
            }
        }
    }

    // �ړ�����
    protected virtual void FixedUpdate()
    {
        if (systemStop == false)
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

            if (speed > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(0, -L_angle + 90, 0);
                this.transform.rotation = targetRotation;
            }

            lineRenderer.SetPosition(0, this.transform.position + new Vector3(0, 0.5f, 0));
            lineRenderer.SetPosition(1, this.transform.position + pointC * 5 + new Vector3(0, 0.5f, 0));

            if (floatTime > 0)
            {
                speed = 8f;
                floatTime -= Time.deltaTime;
                Vector3 newPosition = this.transform.position;
                newPosition.y = 4;
                transform.position = newPosition;
                canUseSkill1 = false;
                canUseSkill2 = false;

                if (floatTime <= 0)
                {
                    speed = speedTemp;
                    canUseSkill1 = true;
                    canUseSkill2 = true;
                    rb.useGravity = true;
                    col.enabled = true;
                }
            }

            if (bindTime > 0)
            {
                bindTime -= Time.deltaTime;
                canMoveInput = false;
            }
            else if (bindTime <= 0)
            {
                canMoveInput = true;
            }

            if (deathInterval > 0)
            {
                deathInterval -= Time.deltaTime;
            }

            GaugeObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            Icons.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
        }
    }

    private void FirstSet()
    {
        speedTemp = speed;

        lineRenderer = this.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

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
            else if (obj.name == "GameManager")
            {
                gameManager = obj.GetComponent<GameManager>();
            }
        }

        GameObject newObject = Instantiate(statusObj);
        statusScript = newObject.GetComponent<Status>();

        gaugeScript = GaugeObj.GetComponent<Gauge>();
        gaugeScript.FirstSet(skill1CooldownTime, skill2CooldownTime);

        this.transform.rotation = Quaternion.Euler(0, 180, 0);

        // �v���C���[�ԍ��ɉ����Ė��O�ƃX�e�[�^�X��ݒ�
        for (int i = 0; i < 4; i++)
        {
            if (!exist[i])
            {
                this.name = "Player" + (i + 1);
                playerNum = i + 1;
                Icon[i].SetActive(true);
                statusScript.FirstSet(playerNum);
                gameManager.Join();
                StartCoroutine(DeviceSet(CharacterSelect_Save.joinedDevices[i]));
                return;
            }
        }
    }

    protected IEnumerator DeviceSet(InputDevice device)
    {
        yield return new WaitForSeconds(1);
        PlayerInput playerInput = this.GetComponent<PlayerInput>();
        InputUser user = playerInput.user;
        user.UnpairDevices(); // �����̃f�o�C�X������
        InputUser.PerformPairingWithDevice(device, user);
        playerInput.enabled = true; // PlayerInput��L����
    }

    // �ړ����\�b�h
    protected void HandleMovement()
    {
        if (canMoveInput == true)
        {
            Vector3 movement = new Vector3(LS_Horizontal, 0.0f, LS_Vertical);
            Vector3 newPosition = transform.position + movement * Speed * L_Stick_Inclination * Time.deltaTime;

            // �ړ��͈͂𐧌�
            newPosition.x = Mathf.Clamp(newPosition.x, -2, 22);
            newPosition.z = Mathf.Clamp(newPosition.z, -2, 22);

            transform.position = newPosition;
        }
    }

    // Jump����
    public void OnJump(InputAction.CallbackContext Jump)
    {
        if (canMoveInput&&canJump)
        {
            if (Jump.started && isGrounded)
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); // JumpForce���g�p
                isGrounded = false;
                jumpInterval = true;
                StartCoroutine(JumpIntervalCountStart());
                jumping();
                isJumping = true;
                canJump = false;
            }
        }
    }

    //Skill1����
    public void OnSkill1(InputAction.CallbackContext Skill1)
    {
        if (skill1InputStop == false)
        {
            if (canMoveInput)
            {
                if (Skill1.started && canUseSkill1)
                {
                    // ��������
                    if (R_Stick_Inclination > 0)
                    {
                        this.transform.rotation = Quaternion.Euler(0, -R_angle + 90, 0);
                    }

                    Skill1Push();
                    skill1InputStop = true;
                    StartCoroutine(Skill1InputManager());
                }
            }
        }
    }

    //Skill2����
    public void OnSkill2(InputAction.CallbackContext Skill2)
    {
        if (skill2InputStop == false)
        {
            if (canMoveInput == true)
            {
                if (Skill2.started && canUseSkill2 == true)
                {
                    //��������
                    if (R_Stick_Inclination > 0)
                    {
                        this.transform.rotation = Quaternion.Euler(0, -R_angle + 90, 0);
                    }
                    Skill2Push();
                    skill2InputStop = true;
                    StartCoroutine(Skill2InputManager());
                }
            }
        }
    }

    public void OnStart(InputAction.CallbackContext Start)
    {
        if (canMoveInput == true)
        {
            if (Start.started)
            {
                if (Time.timeScale != 0f)
                {
                    gameManager.MenuControl(0);
                    gameManager.MenuDisplay(false);
                    pauseControl = true;
                    Time.timeScale = 0f;
                }
                else if(Time.timeScale == 0f && pauseControl == true)
                {
                    gameManager.MenuDisplay(true);
                    pauseControl = false;
                    Time.timeScale = 1f;
                }
            }
        }
    }

    public void OnDecision(InputAction.CallbackContext Decision)
    {
        if (Decision.started)
        {
            if (Time.timeScale == 0f && pauseControl == true)
            {
                int result = 0;
                result = gameManager.MenuControl(2);

                if (result == 1)
                {
                    gameManager.MenuDisplay(true);
                    pauseControl = false;
                    Time.timeScale = 1f;
                }
            }
        }
    }

    // �X�L��1�������ꂽ���̏���
    protected virtual void Skill1Push()
    {
    }

    // �X�L��2�������ꂽ���̏���
    protected virtual void Skill2Push()
    {
    }
    
     protected virtual void jumping()
    {
    }

    protected virtual void Landing()
    {
    }

    protected virtual void Binding()
    {

    }

    // �X�L��1�̑��d���̖͂h�~
    protected virtual IEnumerator Skill1InputManager()
    {
        yield return new WaitForSeconds(0.5f);
        skill1InputStop = false;
    }

    // �X�L��2�̑��d���̖͂h�~
    protected virtual IEnumerator Skill2InputManager()
    {
        yield return new WaitForSeconds(0.5f);
        skill2InputStop = false;
    }


    // �X�L��1�̓���
    protected virtual IEnumerator Skill1Cooldown()
    {
        gaugeScript.SkillUse(1);
        yield return new WaitForSeconds(Skill1CooldownTime);
        canUseSkill1 = true;
    }

    // �X�L��2�̃N�[���_�E������
    protected virtual IEnumerator Skill2Cooldown()
    {
        gaugeScript.SkillUse(2);
        yield return new WaitForSeconds(Skill2CooldownTime);
        canUseSkill2 = true;
    }

    // �X�L��1�̃A�j�����̏���
    protected virtual IEnumerator Skill1DuringAnima()
    {
        duringAnima = true;

        speed = 0;
        canJump = false;
        yield return new WaitForSeconds(skill1AT_Push);
        canJump = true;
        duringAnima = false;
        speed = speedTemp;
    }

    // �X�L��2�̃A�j�����̏���
    protected virtual IEnumerator Skill2DuringAnima()
    {
        duringAnima = true;

        speed = 0;
        canJump = false;
        yield return new WaitForSeconds(skill2AT_Push);
        canJump = true;
        duringAnima = false;
        speed = speedTemp;
    }
    protected virtual IEnumerator JumpDuringAnima()
    {
        yield return new WaitForSeconds(jumpPush);
        canJump = true;
    }

    // �����ɐڐG�����Ƃ��ɌĂ΂��
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (jumpInterval == false)
            {
                isGrounded = true;
                Landing();
                if (isJumping)
                {
                    
                    StartCoroutine(JumpDuringAnima());
                    isJumping = false;
                }
            }
        }
        else if (collision.gameObject.CompareTag("DeadLine"))
        {
            if (deathInterval <= 0)
            {
                stock--;
                deathInterval = 1;
                statusScript.StockMinus(stock);

                StopCoroutine("Skill1Cooldown");
                StopCoroutine("Skill2Cooldown");
                StopCoroutine("Skill1DuringAnima");
                StopCoroutine("Skill2DuringAnima");

                gaugeScript.SkillGaugeReset();


                PlaySoundEffect(SE[0]);
                Instantiate(FixSphere, this.transform.position, Quaternion.identity);

                if (stock >= 1)
                {
                    rb.useGravity = false;
                    col.enabled = false;
                    bindTime = 0;
                    duringAnima = false;
                    canUseSkill1 = true;
                    canUseSkill2 = true;
                    floatTime = floatTime_Set;
                }
                else if (stock < 1)
                {
                    gameManager.Retire(playerNum - 1);
                    Destroy(this.gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Bind"))
        {
            bindTime = bindTime_Set;
        }
    }

    // �����ɐڐG�����Ƃ��ɌĂ΂��
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bind"))
        {
            bindTime = bindTime_Set;
            Binding();
        }
    }

    // �����ɐڐG��������Ƃ��ɌĂ΂��
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SystemStopBreak"))
        {
            systemStop = false;
        }
    }

    private IEnumerator JumpIntervalCountStart()
    {
        yield return new WaitForSeconds(0.25f); // �w�肵���b���ҋ@
        jumpInterval = false;
    }

    protected void PlaySoundEffect(AudioClip se)
    {
        if (se != null)
        {
            audioSource.PlayOneShot(se);
        }
        else
        {
            Debug.LogWarning("�����ݒ肳��Ă��܂���I");
        }
    }
}