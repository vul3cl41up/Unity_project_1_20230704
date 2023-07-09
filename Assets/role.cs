using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role : MonoBehaviour
{
    bool isWalk = false;
    bool isTurn = true;
    public Animator playAni;
    //每秒移動幾格
    public Vector3 v = new Vector3 (5f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        isWalk = false;
        movement();
    }

    private void movement()
    {
        if (Input.GetKey("right"))  
        {
            if (!isTurn)
            {
                this.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                isTurn = true;
            }
                
            isWalk = true;
            this.gameObject.transform.position += v*0.02f;
        }
        if (Input.GetKey("left"))  
        {
            if (isTurn)
            {
                this.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                isTurn = false;
            }
            isWalk = true;
            this.gameObject.transform.position -= v*0.02f;
        }
        if(isWalk)
        {
            playAni.SetBool("isWalk", true);
        }
        else
        {
            playAni.SetBool("isWalk", false);
        }
    }
}
