using UnityEngine;

public class Player_Control_BagScene : MonoBehaviour
{
    [SerializeField, Header("���ʳt��")]
    private float speed = 5;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move_Transform_1();
    }

    /// <summary>
    /// �C�V����transform.position
    /// �ϥ�Input.GetKey(KeyCode)���o����O�_����(0or1)
    /// </summary>
    private void Move_Transform_1()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
}
