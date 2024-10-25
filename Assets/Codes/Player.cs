using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //視覚情報関連
    private LineRenderer lineRenderer;

    //このキャラを操作している人が何Pか
    private int playerNum = 0;

    // スピードとジャンプ力をプロパティ化して派生クラスで設定可能に
    [SerializeField] private int stock = 3;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    protected bool isGrounded;
    protected Rigidbody rb;

    // スキル1とスキル2のクールダウン時間 (フィールドをシリアライズ)
    [SerializeField] private float skill1CooldownTime = 5.0f;
    [SerializeField] private float skill2CooldownTime = 7.0f;

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
    protected bool canMoveInput = true;

    private InputAction L_Stick;
    private InputAction R_Stick;

    //リスポーン後に浮いている時間
    public float floatTime_Set = 0;
    private float floatTime = 0;

    private StatusUI statusUI;


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

    // 移動処理
    protected virtual void FixedUpdate()
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

    // 移動メソッド
    protected void HandleMovement()
    {
        Vector3 movement = new Vector3(LS_Horizontal, 0.0f, LS_Vertical);
        transform.Translate(movement * Speed * Time.deltaTime, Space.World); // Speedを使用
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

    //Skill1処理
    public void OnSkill1(InputAction.CallbackContext Skill1)
    {
        if (canMoveInput == true)
        {
            if (Skill1.started && canUseSkill1 == true)
            {
                //押した時
                Skill1Push();
            }
            else if (Skill1.canceled && canUseSkill2 == true)
            {
                //離した時
                Skill1Release();
            }
        }
    }

    //Skill2処理
    public void OnSkill2(InputAction.CallbackContext Skill2)
    {
        if (canMoveInput == true)
        {
            if (Skill2.started && canUseSkill2 == true)
            {
                //押した時
                Skill2Push();
            }
            else if (Skill2.canceled && canUseSkill2 == true)
            {
                //離した時
                Skill2Release();
            }
        }
    }

    // スキル1が押された時の処理
    protected virtual void Skill1Push()
    {
    }

    // スキル1が離された時の処理
    protected virtual void Skill1Release()
    {
    }

    // スキル2が押された時の処理
    protected virtual void Skill2Push()
    {
    }

    // スキル2が離された時の処理
    protected virtual void Skill2Release()
    {
    }

    // スキル1のクールダウン処理
    protected virtual IEnumerator Skill1Cooldown()
    {
        yield return new WaitForSeconds(Skill1CooldownTime);
        canUseSkill1 = true;
        Debug.Log("Skill 1 ready!");
    }

    // スキル2のクールダウン処理
    protected virtual IEnumerator Skill2Cooldown()
    {
        yield return new WaitForSeconds(Skill2CooldownTime);
        canUseSkill2 = true;
        Debug.Log("Skill 2 ready!");
    }

    // 地面に接触したときに呼ばれる
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