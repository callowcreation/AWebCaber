using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Contacts))]
public class BalloonChanger : MonoBehaviour
{
    [SerializeField]
    Sprite[] m_Sprites = null;

    SpriteRenderer m_SpriteRenderer = null;

    Contacts m_Contacts = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_Contacts = GetComponent<Contacts>();
        m_Contacts.onContact += M_Contacts_onContact;
    }

    void M_Contacts_onContact()
    {
        if(m_Contacts.hits < m_Sprites.Length)
        {
            m_SpriteRenderer.sprite = m_Sprites[m_Contacts.hits];
        }
    }
}
