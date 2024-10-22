using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // スピードとジャンプ力をプロパティ化して派生クラスで設定可能に
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;

    protected bool isGrounded;
    protected Rigidbody rb;

    // スキル1とスキル2のクールダウン時間 (フィールドをシリアライズ)
    [SerializeField] private float skill1CooldownTime = 5.0f;
    [SerializeField] private float skill2CooldownTime = 7.0f;

    //Lスティックの座標を表す
    protected float moveHorizontal, moveVertical;
    //Rスティックの座標を表す
    protected float RS_Horizontal, RS_Vertical;

    //向きを出すためのもの
    private Vector3 pointA = new Vector3(0, 0, 0);  //自身の位置
    private Vector3 pointB = new Vector3(0, 0, 0);  //Lスティックの座標
    private Vector3 pointC = new Vector3(0, 0, 0);  //Rスティックの座標
    protected float angle = 0;
    protected float R_angle = 0;

    //スキルを使用する際にみるフラグ
    protected bool canUseSkill1 = true;
    protected bool canUseSkill2 = true;

    //一時的に入力禁止にしたい際に使用するフラグ
    protected bool canMoveInput = true;

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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    // 移動処理
    protected virtual void FixedUpdate()
    {
        HandleMovement();

        //向き計算
        pointB = new Vector3(moveHorizontal, 0, moveVertical);
        pointC = new Vector3(RS_Horizontal, 0, RS_Vertical);
        // ベクトルの計算
        Vector3 direction = pointB - pointA;
        Vector3 R_direction = pointC - pointA;
        // 角度を計算
        angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // Z軸を使って角度を計算
        R_angle = Mathf.Atan2(R_direction.z, R_direction.x) * Mathf.Rad2Deg; // Z軸を使って角度を計算

        Quaternion targetRotation = Quaternion.Euler(0, -angle + 90, 0);
        this.transform.rotation = targetRotation;
    }    

    // 移動メソッド
    protected void HandleMovement()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * Speed * Time.deltaTime, Space.World); // Speedを使用
    }

    // 左スティックで移動
    public void OnMove(InputAction.CallbackContext Move)
    {
        if (canMoveInput == true)
        {
            Vector2 movementInput = Move.ReadValue<Vector2>();
            moveHorizontal = movementInput.x;
            moveVertical = movementInput.y;
        }
    }


    // 右スティック入力の取得
    public void OnRightStick(InputAction.CallbackContext RightStick)
    {
        if (canMoveInput == true)
        {
            Vector2 RS_Input = RightStick.ReadValue<Vector2>();
            RS_Horizontal = RS_Input.x;
            RS_Vertical = RS_Input.y;
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
            else if (Skill1.canceled)
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
            else if (Skill2.canceled)
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
    }
}
