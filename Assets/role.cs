using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role : MonoBehaviour
{
    Rigidbody2D rb;//建立剛體2D，名叫rb(自己取)
    public Vector2 v = new Vector2 (0, 0);
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
 
        rb = GetComponent<Rigidbody2D>(); //抓取物件的剛體
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        timer += Time.deltaTime;
        Debug.Log(timer);
    }

    private void movement()
    {
        if (Input.GetKey("d"))  //其他方向以此類推
        {
            rb.velocity = v;
        }
        if (Input.GetKey("a"))  //其他方向以此類推
        {
            rb.velocity = -v;
        }
    }
}
