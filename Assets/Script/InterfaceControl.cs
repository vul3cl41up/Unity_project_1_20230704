
using UnityEngine;

public class InterfaceControl : MonoBehaviour
{
    public GameObject BagPanel;

    private void Start()
    {
        BagPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            BagPanel.SetActive(!BagPanel.activeSelf);
        }
    }
}
