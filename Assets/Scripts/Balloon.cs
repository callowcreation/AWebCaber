using UnityEngine;
using static Values;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    float m_ScaleMin = 0.5f;
    [SerializeField]
    float m_ScaleMax = 1.5f;

    [SerializeField]
    int m_MaxHits = 5;

    [SerializeField]
    BalloonTypes m_BalloonTypes = BalloonTypes.Normal;

    public float maxY = 10.0f;

    public float scaleMin { get => m_ScaleMin; }
    public float scaleMax { get => m_ScaleMax; }
    public int maxHits { get => m_MaxHits; }
    public BalloonTypes balloonType { get => m_BalloonTypes; }
    public int hits { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Transform trans { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public static event System.EventHandler<Collision2D> OnContact;
    public static event System.EventHandler OnOutOfBounds;

    void Awake()
    {
        trans = transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Values.PLAYER_LAYER)
        {
            if (collision.collider is CapsuleCollider2D)
            {
                hits++;
            }
        }
        OnContact?.Invoke(this, collision);
    }

    void FixedUpdate()
    {
        if(trans.position.y < -maxY)
        {
            OnOutOfBounds?.Invoke(this, System.EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}
