using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]
    int m_Direction = 0;

    Animator m_Animator = null;

    GotoMouse m_GotoMouse = null;

    ClickSelect m_ClickSelect = null;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_GotoMouse = GetComponent<GotoMouse>();
        m_ClickSelect = GetComponent<ClickSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = m_GotoMouse.target.x - transform.position.x;
        m_Direction = Mathf.RoundToInt(direction);
        m_Animator.SetInteger("Direction", m_Direction);

        if(m_Direction == 0)
        {
            if(m_ClickSelect.selected != null)
            {
                m_Animator.SetTrigger("JumpUp");
                m_ClickSelect.selected = null;
            }
        }
    }

    void LateUpdate()
    {
        if (m_Direction > 0 && transform.localScale.x < 0 || m_Direction < 0 && transform.localScale.x > 0)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }
}
