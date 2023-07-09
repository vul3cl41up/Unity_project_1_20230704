using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Role;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Role.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Role.transform.position + offset;
    }
}
