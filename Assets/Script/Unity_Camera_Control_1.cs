using UnityEngine;

public class Unity_Camera_Control_1 : MonoBehaviour
{
    public GameObject Role; //畫面指定跟隨的角色
    Vector3 offset; //一開始的距離
    void Start()
    {
        //主攝影機與跟隨角色的相對距離
        offset = this.transform.position - Role.transform.position;
    }
    void Update()
    {
        //當跟隨角色改變座標，主攝影機跟著改變座標
        this.transform.position = Role.transform.position + offset;
    }
}
