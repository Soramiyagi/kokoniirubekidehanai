using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject FixSphere;

    //視覚情報関連
    private LineRenderer lineRenderer;

    //このキャラを操作している人が何Pか
    private int playerNum = 0;

    // パラメーターをプロパティ化して派生クラスで設定可能に
    [SerializeField] private int stock = 3;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    //死んだ時の復帰時間中の移動速度を上げるときに使用
    //ここに元々のspeedを一時的に保存する
    private float speedTemp = 0;

    //Lスティックを傾けているかどうか
    private int L_Stick_Inclination = 0;
    private int R_Stick_Inclination = 0;

    protected bool isGrounded;
    protected Rigidbody rb;

    // スキル1とスキル2のクールダウン時間 (フィールドをシリアライズ)
    [SerializeField] private float skill1CooldownTime = 5.0f;
    [SerializeField] private float skill2CooldownTime = 7.0f;

    //スキル使用時のアニメーション中に操作を受け付けなくなる時間
    [SerializeField] private float skill1AT_Push = 1.0f;
    [SerializeField] private float skill2AT_Push = 1.0f;
    protected bool duringAnima = false;

    //Lスティックの座標を表す
    protected Vector2 LS_Input;
    protected float LS_Horizontal, LS_Vertical;
    //Rスティックの座標を表す
    protected Vector2 RS_Input;
    protected float RS_Horizontal, RS_Vertical;

    //向きを出すためのもの
    private Vector3 pointA = new Vector3(0, 0, 0);  //自身の位置
    private Vector3 pointB = new Vector3(0, 0, 0);  //Lスティックの座標
    private Vector3 pointC = new Vector3(0, 0, 0);  //Rスティックの座標
    protected float L_angle = 0;
    protected float R_angle = 0;

    //スキルを使用する際にみるフラグ
    protected bool canUseSkill1 = true;
    protected bool canUseSkill2 = true;

    //一時的に入力禁止にしたい際に使用するフラグ
    protected bool canMoveInput = false;

    private InputAction L_Stick;
    private InputAction R_Stick;

    //リスポーン後に浮いている時間
    public float floatTime_Set = 0;
    private float floatTime = 0;

    //  バインド弾に当たった時の拘束時間
    public float bindTime_Set = 0;
    private float bindTime = 0;

    //連続で死ぬことを防ぐため
    private float deathInterval = 0;

    //連続でジャンプするのを防ぐため
    private float jumpInterval = 0;

    //ステータス用
    [SerializeField] private GameObject statusObj;
    private Status statusScript;

    //ゲームマネージャーへのアクセス用
    private GameManager gameManager;

    //スキルのゲージ
    [SerializeField] private GameObject GaugeObj;
    private Gauge gaugeScript;

    //ゲームの進行による操作不能フラグ
    protected bool systemStop = true;

    // プロパティでフィールドを操作
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
    }

    // 移動処理
    protected virtual void FixedUpdate()
    {
        if (systemStop == false)
        {
            HandleMovement();

            //向き計算
            pointB = new Vector3(LS_Horizontal, 0, LS_Vertical);
            pointC = new Vector3(RS_Horizontal, 0, RS_Vertical);
            // ベクトルの計算
            Vector3 direction = pointB - pointA;
            Vector3 R_direction = pointC - pointA;
            // 角度を計算
            L_angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // Z軸を使って角度を計算
            R_angle = Mathf.Atan2(R_direction.z, R_direction.x) * Mathf.Rad2Deg; // Z軸を使って角度を計算

            if (speed > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(0, -L_angle + 90, 0);
                this.transform.rotation = targetRotation;
            }

            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, this.transform.position + pointC * 5);

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
        }
    }

    private void FirstSet()
    {
        speedTemp = speed;

        lineRenderer = this.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        // 名前が一致するオブジェクトが存在するかチェック

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

        // プレイヤー番号に応じて名前とステータスを設定
        for (int i = 0; i < 4; i++)
        {
            if (!exist[i])
            {
                this.name = "Player" + (i + 1);
                playerNum = i + 1;
                statusScript.FirstSet(playerNum);
                return;
            }
        }
        /*
        if (exist[0] == false)
        {
            this.name = "Player1";
            playerNum = 1;
            statusScript.FirstSet(playerNum);
            return;
        }

        if (exist[1] == false)
        {
            this.name = "Player2";
            playerNum = 2;
            statusScript.FirstSet(playerNum);
            return;
        }

        if (exist[2] == false)
        {
            this.name = "Player3";
            playerNum = 3;
            statusScript.FirstSet(playerNum);
            return;
        }

        if (exist[3] == false)
        {
            this.name = "Player4";
            playerNum = 4;
            statusScript.FirstSet(playerNum);
            return;
        }
        */
    }

    // 移動メソッド
    protected void HandleMovement()
    {
        if (canMoveInput == true)
        {
            Vector3 movement = new Vector3(LS_Horizontal, 0.0f, LS_Vertical);
            transform.Translate(movement * Speed * L_Stick_Inclination * Time.deltaTime, Space.World); // Speedを使用
        }
    }

    // Jump処理
    public void OnJump(InputAction.CallbackContext Jump)
    {
        if (canMoveInput == true)
        {
            if (Jump.started && isGrounded)
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); // JumpForceを使用
                isGrounded = false;
            }
        }
    }

    private bool skill1InputStop = false;
    private bool skill2InputStop = false;

    //Skill1処理
    public void OnSkill1(InputAction.CallbackContext Skill1)
    {
        if (skill1InputStop == false)
        {
            if (canMoveInput == true)
            {
                if (Skill1.started && canUseSkill1 == true)
                {
                    // 押した時
                    
                    if (R_Stick_Inclination > 0)
                    {
                        this.transform.rotation = Quaternion.Euler(0, -R_angle + 90, 0);
                    }

                    Skill1Push();
                    StartCoroutine(Skill1InputManager());
                    skill1InputStop = true;
                }
            }
        }
    }

    //Skill2処理
    public void OnSkill2(InputAction.CallbackContext Skill2)
    {
        if (skill2InputStop == false)
        {
            if (canMoveInput == true)
            {
                if (Skill2.started && canUseSkill2 == true)
                {
                    //押した時
                    
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

    // スキル1が押された時の処理
    protected virtual void Skill1Push()
    {
    }

    // スキル2が押された時の処理
    protected virtual void Skill2Push()
    {
    }

    // スキル1の多重入力の防止
    protected virtual IEnumerator Skill1InputManager()
    {
        yield return new WaitForSeconds(0.5f);
        skill1InputStop = false;
    }

    // スキル2の多重入力の防止
    protected virtual IEnumerator Skill2InputManager()
    {
        yield return new WaitForSeconds(0.5f);
        skill2InputStop = false;
    }

    // スキル1の入力
    protected virtual IEnumerator Skill1Cooldown()
    {
        gaugeScript.SkillUse(1);
        yield return new WaitForSeconds(Skill1CooldownTime);
        canUseSkill1 = true;
    }

    // スキル2のクールダウン処理
    protected virtual IEnumerator Skill2Cooldown()
    {
        gaugeScript.SkillUse(2);
        yield return new WaitForSeconds(Skill2CooldownTime);
        canUseSkill2 = true;
    }

    // スキル1のアニメ中の処理
    protected virtual IEnumerator Skill1DuringAnima()
    {
        duringAnima = true;

        speed = 0;

        yield return new WaitForSeconds(skill1AT_Push);

        duringAnima = false;
        speed = speedTemp;
    }

    // スキル2のアニメ中の処理
    protected virtual IEnumerator Skill2DuringAnima()
    {
        duringAnima = true;

        speed = 0;

        yield return new WaitForSeconds(skill2AT_Push);

        duringAnima = false;
        speed = speedTemp;
    }

    // 何かに接触したときに呼ばれる
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpInterval = 0.1f;
            StartCoroutine(JumpInterval());
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

                Instantiate(FixSphere, this.transform.position, Quaternion.identity);

                if (stock >= 1)
                {
                    bindTime = 0;
                    duringAnima = false;
                    canUseSkill1 = true;
                    canUseSkill2 = true;
                    floatTime = floatTime_Set;
                }
                else if (stock < 1)
                {
                    gameManager.Retire(playerNum);
                    Destroy(this.gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Bind"))
        {
            bindTime = bindTime_Set;
        }
    }

    // 何かに接触したときに呼ばれる
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bind"))
        {
            bindTime = bindTime_Set;
        }
    }

    // 何かに接触し続けるときに呼ばれる
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SystemStopBreak"))
        {
            systemStop = false;
        }
    }
    private IEnumerator JumpInterval()
    {
        while (jumpInterval > 0)
        {
            jumpInterval -= Time.deltaTime;
            yield return null;
        }

        if(jumpInterval <= 0)
        {
            isGrounded = true;
        }
    }
}