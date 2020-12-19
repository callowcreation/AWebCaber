using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoMouse : MonoBehaviour
{
    [SerializeField]
    float m_Speed = 7.0f;

    public Vector3 target;

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.y = transform.position.y;
            target.z = transform.position.z;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, m_Speed * Time.deltaTime);
    }
}
