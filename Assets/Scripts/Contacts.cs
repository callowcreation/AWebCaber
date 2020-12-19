using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Contacts : MonoBehaviour
{
    const int PLAYER_LAYER = 8;
    const int BALLOON_LAYER = 9;
    const int GROUND_LAYER = 10;

    public enum Surface
    {
        Ground,
        Body,
        Head,
        Balloon
    }

    public event System.Action<Surface, AudioSource> onContactSurface;
    public event System.Action onContact;

    [System.Serializable]
    public class Power
    {
        public int player = 1;
        public int ground = 3;
    }

    [SerializeField]
    Power m_Power = new Power();

    [SerializeField]
    int m_MaxHits = 3;
    
    [SerializeField]
    int m_Hits = 0;

    [SerializeField]
    float m_ResponseTime = 0.5f;
    [SerializeField]
    float m_ReflectThreshold = -0.15f;

    public int maxHits { get => m_MaxHits; }
    public int hits { get => m_Hits; }

    Rigidbody2D rb2d = null;

    AudioSource m_AudioSource = null;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (rb2d.isKinematic == true) return;

        if(collision.gameObject.layer == PLAYER_LAYER)
        {
            if(collision.collider is CapsuleCollider2D)
            {
                m_Hits += m_Power.player;

                //Debug.Log($"{collision.collider is CapsuleCollider2D}");

                List<ContactPoint2D> items = collision.contacts.Where(x => x.separation < m_ReflectThreshold).ToList();

                if (items.Count() > 0)
                {
                    Debug.Log($"{items.Count} {items[0].separation} separation");
                }

                //rb2d.velocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal) * m_ReflectMultiplier;
                //rb2d.isKinematic = true;
                Invoke("ResetCollision", m_ResponseTime);
                onContactSurface?.Invoke(Surface.Head, m_AudioSource);
            } 
            else
            {
                onContactSurface?.Invoke(Surface.Body, m_AudioSource);
            }
        } 
        else if(collision.gameObject.layer == GROUND_LAYER)
        {
            m_Hits += m_Power.ground;
            onContactSurface?.Invoke(Surface.Ground, m_AudioSource);
        }
        else if(collision.gameObject.layer == BALLOON_LAYER)
        {
            onContactSurface?.Invoke(Surface.Balloon, m_AudioSource);
        }

        onContact?.Invoke();
    }

    void ResetCollision()
    {
        //rb2d.isKinematic = false;
    }

    Vector2 lastVelocity;
    void FixedUpdate()
    {
        lastVelocity = rb2d.velocity;
    }
}
