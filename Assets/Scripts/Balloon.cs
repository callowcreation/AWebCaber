using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int hits = 0;
    public float maxY = 10.0f;

    public SpriteRenderer spriteRenderer { get; private set; }
    public Transform trans { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public static event System.EventHandler<Collision2D> OnContact;

    void Awake()
    {
        trans = transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnContact?.Invoke(this, collision);
    }

    void FixedUpdate()
    {
        if(trans.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }
}
