﻿using Unity.VisualScripting;
using UnityEngine;

public class Role_Control_Magic : MonoBehaviour
{
    #region 資料
    public enum Status { Idle, Walk, Jump, Attack}
    Status status_now;
    public Status Status_Now => status_now;
    private Rigidbody2D rb;

    GameObject role_now;
    GameObject role_front;
    GameObject role_back;
    GameObject role_side;

    [Header("方向")]
    private int x_direction = 0;
    private int change_x;
    private int y_direction = 0;
    private int change_y;

    [Header("移動參數")]
    private Vector2 oblique_acceleration = new Vector3(3.54f,3.54f);
    private Vector2 x_acceleration = new Vector3(5f, 0);
    private Vector2 y_acceleration = new Vector3(0, 5f);
    private Vector2 change_acceleration;
    private float resistance = 2.4f;

    [Header("跳躍參數")]
    private Vector2 oblique_jump_acceleration = new Vector3(4.24f, 4.24f);
    private Vector2 x_jump_acceleration = new Vector3(6f, 0);
    private Vector2 y_jump_acceleration = new Vector3(0, 6f);
    private float jump_resistance = 6.25f;
    private float oblique_jump_resistance = 4.42f;
    private float jump_time = 0.6f;
    private float jump_timer = 0;
    private bool isJump = true;
    private bool jump_happend = false;
    #endregion


    #region 事件
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        role_front = transform.GetChild(0).gameObject;
        role_back = transform.GetChild(1).gameObject;
        role_side = transform.GetChild(2).gameObject;
        role_now = role_front;
        status_now = Status.Idle;
    }

    private void FixedUpdate()
    {
        RunStatus();

        print($"速度({rb.velocity.x},{rb.velocity.y})");
    }

    #endregion

    #region 方法
    void RunStatus()
    {
        if (status_now == Status.Idle)
            RunIdleStatus();
        else if (status_now == Status.Walk)
            RunWalkStatus();
        else if (status_now == Status.Jump)
            RunJumpStatus();
    }
    void RunIdleStatus()
    {
        ChangeAcceleration();
        if (Input.GetKeyDown(KeyCode.Space))
            status_now = Status.Jump;
        else if (change_x != 0 || change_y != 0)
            status_now = Status.Walk;
    }
    void RunWalkStatus()
    {
        ChangeAcceleration();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            status_now = Status.Jump;
            return;
        }
        else if (rb.velocity != Vector2.zero)
            status_now = Status.Walk;
        else
            status_now = Status.Idle;

        ChangeDirect();
        ChangeVeolocity();
    }

    void RunJumpStatus()
    {
        jump_timer += Time.fixedDeltaTime;
        if(jump_timer >= jump_time)
            isJump = false;

        if(!isJump)
        {
            isJump = true;
            jump_timer = 0;
            status_now = Status.Walk;
            jump_happend = false;
            return;
        }

        if(!jump_happend)
        {
            jump_happend = true;

            if(change_x == 0 && change_y == 0)
            {
                return;
            }
            else if(change_x != 0 && change_y != 0)
            {
                if(Mathf.Abs(rb.velocity.x) <= oblique_jump_resistance)
                {
                    rb.velocity = new Vector2(0,rb.velocity.y);
                    x_direction = 0;
                }
                else
                {
                    rb.velocity -= new Vector2(x_direction * oblique_jump_resistance, rb.velocity.y);
                }

                if(Mathf.Abs(rb.velocity.y) <= oblique_jump_resistance)
                {
                    rb.velocity = new Vector2(rb.velocity.x,0);
                    y_direction = 0;
                }
                else
                {
                    rb.velocity -= new Vector2(rb.velocity.x, y_direction * oblique_jump_resistance);
                }

                rb.velocity += new Vector2(change_x * oblique_jump_acceleration.x, change_y * oblique_jump_acceleration.y);
            }
            else if(change_x != 0)
            {
                if (Mathf.Abs(rb.velocity.x) <= jump_resistance)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    x_direction = 0;
                }
                else
                {
                    rb.velocity -= new Vector2(x_direction * jump_resistance, rb.velocity.y);
                }

                rb.velocity += x_jump_acceleration * change_x;
            }
            else if(change_y != 0)
            {
                if (Mathf.Abs(rb.velocity.y) <= jump_resistance)
                {
                    rb.velocity = new Vector2(rb.velocity.x , 0);
                }
                else
                {
                    rb.velocity -= new Vector2(rb.velocity.x, y_direction * jump_resistance);
                }
                rb.velocity += y_jump_acceleration * change_y;
            }

            ChangeDirect();
        }


    }

    private void ChangeAcceleration()
    {
        if (rb.velocity.x > 0)
            x_direction = 1;
        else if (rb.velocity.x == 0)
            x_direction = 0;
        else if (rb.velocity.x < 0)
            x_direction = -1;

        if (rb.velocity.y > 0)
            y_direction = 1;
        else if (rb.velocity.y == 0)
            y_direction = 0;
        else if (rb.velocity.y < 0)
            y_direction = -1;

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            change_x = -1;
            change_y = 1;
            change_acceleration = oblique_acceleration;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            change_x = -1;
            change_y = -1;
            change_acceleration = oblique_acceleration;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            change_x = 1;
            change_y = 1;
            change_acceleration = oblique_acceleration;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            change_x = 1;
            change_y = -1;
            change_acceleration = oblique_acceleration;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            change_x = -1;
            change_y = 0;
            change_acceleration = x_acceleration;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            change_x = 1;
            change_y = 0;
            change_acceleration = x_acceleration;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            change_x = 0;
            change_y = 1;
            change_acceleration = y_acceleration;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            change_x = 0;
            change_y = -1;
            change_acceleration = y_acceleration;
        }
        else
        {
            change_x = 0;
            change_y = 0;
        }
    }

    void ChangeVeolocity()
    {
        if(change_x == 0 && change_y ==0)
        {
            rb.velocity -= rb.velocity / resistance * Time.fixedDeltaTime;
            if(Mathf.Abs(rb.velocity.x) < 4)
                rb.velocity = new Vector2 (0, rb.velocity.y);

            if (Mathf.Abs(rb.velocity.y) < 4)
                rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if(change_x + x_direction == 0)
        {
            if(Mathf.Abs(rb.velocity.x) < 7)
            {
                rb.velocity = new Vector2(0,rb.velocity.y);
                x_direction = 0;
            }
            else
            {
                rb.velocity += new Vector2(7*change_x,0);
            }
        }
        else if(change_x == 0)
        {
            if(Mathf.Abs(rb.velocity.x) < 4f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                x_direction = 0;
            }
        }
        else
        {
            x_direction = change_x;
            rb.velocity += new Vector2(change_acceleration.x*Time.fixedDeltaTime * change_x, 0);
        }

        if (change_y + y_direction == 0)
        {
            if (Mathf.Abs(rb.velocity.y) < 7)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                y_direction = 0;
            }
            else
            {
                rb.velocity += new Vector2(0, 7 * change_y);
            }
        }
        else if (change_y == 0)
        {
            if (Mathf.Abs(rb.velocity.y) < 4f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                y_direction = 0;
            }
        }
        else
        {
            y_direction = change_y;
            rb.velocity += new Vector2(0, change_acceleration.y * Time.fixedDeltaTime * change_y);
        }

        rb.velocity -= (rb.velocity / resistance * Time.fixedDeltaTime);
    }
    void ChangeDirect()
    {
        if (x_direction == 0 || y_direction == 0)
        {
            
            if (Input.GetKey(KeyCode.LeftArrow) && role_now == role_side && transform.rotation.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0f, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(role_now != role_side)
                {
                    role_now.SetActive(false);
                    role_now = role_side;
                    role_now.SetActive(true);
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && role_now == role_side && transform.rotation.y == 0)
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if(role_now != role_side)
                {
                    role_now.SetActive(false);
                    role_now = role_side;
                    transform.rotation = Quaternion.Euler(0, 180f, 0);
                    role_now.SetActive(true);
                }
                
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if(role_now != role_front)
                {
                    role_now.SetActive(false);
                    role_now = role_front;
                    transform.rotation = Quaternion.Euler(0, 0f, 0);
                    role_now.SetActive(true);
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                if (role_now != role_back)
                {
                    role_now.SetActive(false);
                    role_now = role_back;
                    transform.rotation = Quaternion.Euler(0, 0f, 0);
                    role_now.SetActive(true);
                }
            }
        }
    }
    #endregion




}
