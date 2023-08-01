using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D m_body2D;
    private Animator m_animator;
    private bool touchGround;
    private bool facingRight = true;
    public Collision2D collision;
    public float moveSpeed = 5f;

    private void Start()
    {
        m_body2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        Debug.Log("1."+LayerMask.NameToLayer("UI"));
        Debug.Log("2."+LayerMask.GetMask("UI"));

    }
    private void FixedUpdate()
    {
        Move();
        CheckPoint();


    }

    private void Move()
    {
        m_body2D.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), moveSpeed * Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0)
        {
            if ((facingRight && Input.GetAxis("Horizontal") < 0) || (!facingRight && Input.GetAxis("Horizontal") > 0))
                Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    private void CheckPoint()
    {
        touchGround = Physics2D.OverlapCircle(new Vector2(0, 0), 0.15f, LayerMask.GetMask("Default"));

    }
    
}
