using UnityEngine;

public class Role_Child_Control_Magic : MonoBehaviour
{
    GameObject m_Parent;
    Role_Control_Magic parent_script;
    Animator ani;
    GameObject role_now;
    GameObject role_idle;
    GameObject role_walk;
    GameObject role_jump;

    private void Start()
    {
        m_Parent = transform.parent.gameObject;
        parent_script = m_Parent.GetComponent<Role_Control_Magic>();
        role_idle = transform.GetChild(0).gameObject;
        role_walk = transform.GetChild(1).gameObject;
        role_jump = transform.GetChild(2).gameObject;
        role_now = role_idle;
    }

    private void OnEnable()
    {
        if (parent_script.Status_Now == Role_Control_Magic.Status.Idle)
        {
            role_now = role_idle;
            role_now.SetActive(true);
        }
        else if (parent_script.Status_Now == Role_Control_Magic.Status.Walk)
        {
            role_now = role_walk;
            role_now.SetActive(true);
        }
        else if(parent_script.Status_Now == Role_Control_Magic.Status.Jump)
        {
            role_now = role_jump;
            role_now.SetActive(true);
        }
    }

    private void OnDisable()
    {
        role_now.SetActive(false);
    }

    private void FixedUpdate()
    {
        ChangeAni();
    }

    void ChangeAni()
    {
        if(parent_script.Status_Now == Role_Control_Magic.Status.Jump)
        {
            if (role_now != role_jump)
            {
                role_now.SetActive(false);
                role_now = role_jump;
                role_now.SetActive(true);
            }
        }
        else if (parent_script.Status_Now == Role_Control_Magic.Status.Idle)
        {
            if(role_now != role_idle)
            {
                role_now.SetActive(false);
                role_now = role_idle;
                role_now.SetActive(true);
            }
        }
        else if(parent_script.Status_Now == Role_Control_Magic.Status.Walk)
        {
            if (role_now != role_walk)
            {
                role_now.SetActive(false);
                role_now = role_walk;
                role_now.SetActive(true);
            }
        }

    }
}
