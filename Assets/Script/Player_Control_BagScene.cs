using Unity.VisualScripting;
using UnityEngine;

public class Player_Control_BagScene : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float speed = 5;

    private bool isTouch = false;
    private GameObject itemObject = null;

    public Inventory myBag;


    private void Update()
    {
        Move_Transform_1();
        pickUp();
    }

    private void pickUp()
    {
        if (isTouch && itemObject != null && Input.GetKeyDown(KeyCode.Z)) 
        {
            Item item = itemObject.GetComponent<ItemOnWorld>().thisItem;
            
            int index = myBag.itemList.FindIndex(x => x == item);
            if (index != -1)
                myBag.itemHold[index]++;
            else
            {
                myBag.itemList.Add(item);
                myBag.itemHold.Add(1);
            }

            Destroy(itemObject);
            Inventory_Manager.Refresh();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Prop")
        {
            isTouch = true;
            itemObject = collision.gameObject;
            print(itemObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Prop")
        {
            isTouch = false;
            itemObject = null;

        }
    }

    /// <summary>
    /// 每幀改變transform.position
    /// 使用Input.GetKey(KeyCode)取得按鍵是否按著(0or1)
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
