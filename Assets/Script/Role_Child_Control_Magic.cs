using UnityEngine;

public class Role_Child_Control_Magic : MonoBehaviour
{

    #region 資料
    GameObject m_Parent;
    Role_Control_Magic parent_script;
    Animator ani;
    GameObject ani_now;
    GameObject ani_idle;
    GameObject ani_walk;
    GameObject ani_jump;
    GameObject ani_attack;

    #endregion



    private void Start()
    {
        m_Parent = transform.parent.gameObject;
        parent_script = m_Parent.GetComponent<Role_Control_Magic>();
        ani_idle = transform.GetChild(0).gameObject;
        ani_walk = transform.GetChild(1).gameObject;
        ani_jump = transform.GetChild(2).gameObject;
        ani_attack = transform.GetChild(3).gameObject;
        ani_now = ani_idle;
    }

    private void OnEnable()
    {
        if (parent_script.Status_Now == Role_Control_Magic.Status.Idle)
        {
            ani_now = ani_idle;
            ani_now.SetActive(true);
        }
        else if (parent_script.Status_Now == Role_Control_Magic.Status.Walk)
        {
            ani_now = ani_walk;
            ani_now.SetActive(true);
        }
        else if(parent_script.Status_Now == Role_Control_Magic.Status.Jump)
        {
            ani_now = ani_jump;
            ani_now.SetActive(true);
        }
        else if(parent_script.Status_Now == Role_Control_Magic.Status.Attack)
        {
            ani_now = ani_attack;
            ani_now.SetActive(true);
        }
    }

    private void OnDisable()
    {
        ani_now.SetActive(false);
    }

    private void FixedUpdate()
    {
        ChangeAni();
    }

    void ChangeAni()
    {
        if(parent_script.Status_Now == Role_Control_Magic.Status.Attack)
        {
            if (ani_now != ani_attack)
            {
                ani_now.SetActive(false);
                ani_now = ani_attack;
                ani_now.SetActive(true);
            }
        }
        else if(parent_script.Status_Now == Role_Control_Magic.Status.Jump)
        {
            if (ani_now != ani_jump)
            {
                ani_now.SetActive(false);
                ani_now = ani_jump;
                ani_now.SetActive(true);
            }
        }
        else if (parent_script.Status_Now == Role_Control_Magic.Status.Idle)
        {
            if(ani_now != ani_idle)
            {
                ani_now.SetActive(false);
                ani_now = ani_idle;
                ani_now.SetActive(true);
            }
        }
        else if(parent_script.Status_Now == Role_Control_Magic.Status.Walk)
        {
            if (ani_now != ani_walk)
            {
                ani_now.SetActive(false);
                ani_now = ani_walk;
                ani_now.SetActive(true);
            }
        }

    }
}
