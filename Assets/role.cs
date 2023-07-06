using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role : MonoBehaviour
{

    public Vector3 xv = new Vector3 (0.1f, 0, 0);
    public Vector3 yv = new Vector3(0, 0.4f, 0);
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
        movement();
    }

    private void movement()
    {
        if (Input.GetKey("right"))  
        {
            this.gameObject.transform.position += xv;
        }
        if (Input.GetKey("left"))  
        {
            this.gameObject.transform.position -= xv;
        }
        if(Input.GetKey("up"))
        {
            this.gameObject.transform.position += yv;
        }
    }
}
