using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role : MonoBehaviour
{
    Rigidbody2D rb;//�إ߭���2D�A�W�srb(�ۤv��)
    public Vector2 v = new Vector2 (0, 0);
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
 
        rb = GetComponent<Rigidbody2D>(); //������󪺭���
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
        if (Input.GetKey("d"))  //��L��V�H������
        {
            rb.velocity = v;
        }
        if (Input.GetKey("a"))  //��L��V�H������
        {
            rb.velocity = -v;
        }
    }
}
