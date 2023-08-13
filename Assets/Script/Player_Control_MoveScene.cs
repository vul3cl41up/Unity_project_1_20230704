using UnityEngine;

public class Player_Control_MoveScene: MonoBehaviour
{
    enum move_mode { Move_Transform_1=0, 
        Move_Transform_2, 
        Move_Transform_3, 
        Move_Transform_4,
        Move_Transform_5,
        Move_Velocity_1,
        Move_AddForce_1,
        NumberOfEnum
    }
    move_mode move_Mode = (move_mode)0;

    [SerializeField, Header("移動速度")]
    private float speed = 5;
    [SerializeField, Header("敏感度")]
    private float sensitive = 1;

    public bool snap = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        //改變移動方式
        if (Input.GetKeyDown(KeyCode.Z))
        {
            move_Mode = (move_mode)(((int)move_Mode + 1) % (int)move_mode.NumberOfEnum);
            print($"目前的移動模式:{move_Mode}");
            if(rb.velocity != Vector2.zero)
                rb.velocity = Vector2.zero;
        }
        switch (move_Mode)
        {
            case move_mode.Move_Transform_1:
                Move_Transform_1();
                break;
            case move_mode.Move_Transform_3:
                Move_Transform_3();
                break;
            case move_mode.Move_Transform_4:
                Move_Transform_4();
                break;
            case move_mode.Move_Transform_5:
                Move_Transform_5();
                break;
            case move_mode.Move_Velocity_1:
                Move_Velocity_1();
                break;
            
        }

    }
    private void FixedUpdate()
    {
        switch (move_Mode)
        {
            case move_mode.Move_Transform_2:
                Move_Transform_2();
                break;
            case move_mode.Move_AddForce_1:
                Move_AddForce_1();
                break;
        }
    }

    /// <summary>
    /// 每幀改變transform.position
    /// 使用Input.GetKey(KeyCode)取得按鍵是否按著(0or1)
    /// </summary>
    private void Move_Transform_1()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        
        if( Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }

    /// <summary>
    /// 每固定時間改變transform.position
    /// 使用Input.GetKey(KeyCode)取得按鍵是否按著(0or1)
    /// </summary>
    private void Move_Transform_2()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.fixedDeltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.fixedDeltaTime * speed;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * Time.fixedDeltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * Time.fixedDeltaTime * speed;
        }
    }

    /// <summary>
    /// 每幀使用transform.translate()改變人物座標
    /// 使用Input.GetKey(KeyCode)取得按鍵是否按著(0or1)
    /// </summary>
    private void Move_Transform_3()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// 每幀改變transform.position
    /// 使用Input.GetAxis(String)取得按鍵是否按著(-1~1)
    /// </summary>
    private void Move_Transform_4()
    {
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.position += Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * speed;
    }

    /// <summary>
    /// 每幀改變transform.position
    /// 使用Input.GetAxisRaw(String)取得按鍵是否按著(-1,0,1)
    /// </summary>
    private void Move_Transform_5()
    {
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += Vector3.up * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
    }

    /// <summary>
    /// 每幀改變rigidbody.velocity
    /// 使用Input.GetKey(KeyCode)取得按鍵是否按著(0or1)
    /// </summary>
    private void Move_Velocity_1()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    /// <summary>
    /// 使用AddForce()每幀改變rigidbody.velocity
    /// 使用Input.GetKey(KeyCode)取得按鍵是否按著(0or1)
    /// </summary>
    private void Move_AddForce_1()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (snap && rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (rb.velocity.x <= -speed)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
                rb.AddForce(Vector2.left * sensitive * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if( snap && rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (rb.velocity.x >= speed)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
                rb.AddForce(Vector2.right * sensitive * speed);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (snap && rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x,0);
            }

            if (rb.velocity.y >= speed)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else
                rb.AddForce(Vector2.up * sensitive * speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (snap && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            if (rb.velocity.y <= -speed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            else
                rb.AddForce(Vector2.down * sensitive * speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        

    }
}
