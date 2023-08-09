using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role_move_physical : MonoBehaviour
{
    bool isWalk = false;
    bool isTurn = true;
    public Animator playAni;
    //每秒移動幾格
    public float speed;
    public float jump_height;
    Rigidbody2D player_rigid;
    bool groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        player_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isWalk = false;
        movement();
        player_rigid.velocity = new Vector2(player_rigid.velocity.x, player_rigid.velocity.y - 9.8f * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D selfbody)
    {
        if (selfbody.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }
    }
    void OnCollisionExit2D(Collision2D selfbodyexit)
    {
        if (selfbodyexit.gameObject.tag == "Ground")
            groundCheck = false;
    }


    private void movement()
    {
        if (Input.GetKey(KeyCode.Space) && groundCheck)
            player_rigid.velocity = new Vector2(player_rigid.velocity.x, jump_height);

        player_rigid.velocity = new Vector2(0, player_rigid.velocity.y);
        if (Input.GetKey("right"))
        {
            if (!isTurn)
            {
                this.transform.Rotate(new Vector3(0, 180, 0));
                isTurn = true;
            }

            isWalk = true;
            //this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            player_rigid.velocity = new Vector2(speed, player_rigid.velocity.y);
        }
        if (Input.GetKey("left"))
        {
            if (isTurn)
            {
                this.transform.Rotate(new Vector3(0, 180, 0));
                isTurn = false;
            }
            isWalk = true;
            //this.transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
            player_rigid.velocity = new Vector2(-speed, player_rigid.velocity.y);
        }
        if (isWalk)
        {
            playAni.SetBool("isWalk", true);
        }
        else
        {
            playAni.SetBool("isWalk", false);
        }
    }
}
