using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
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

    [SerializeField]
    float m_StartY = 6.0f;

    [SerializeField]
    float m_XBounds = 7.0f;

    [SerializeField]
    int m_MaxHits = 5;

    [SerializeField]
    float m_MaxAngularVelocity = 50.0f;
    [SerializeField]
    float m_ScaleIntervalMin = 0.5f;
    [SerializeField]
    float m_ScaleIntervalMax = 1.5f;

    [SerializeField]
    float m_SpawnIntervalMin = 5.0f;
    [SerializeField]
    float m_SpawnIntervalMax = 7.0f;

    [SerializeField]
    float m_ReflectThreshold = -0.15f;

    [SerializeField]
    Balloon[] m_BalloonPrefabs = null;
    [SerializeField]
    GameObject[] m_PrefabsPop = null;

    [SerializeField]
    Sprite[] m_ChangeSprites = null;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true)
        {
            Vector3 spawnVector = new Vector3(Random.Range(-m_XBounds, m_XBounds), m_StartY, 0.0f);
            Balloon balloon = Instantiate(m_BalloonPrefabs[0], spawnVector, Quaternion.identity);
            balloon.rb.angularVelocity = Random.Range(-m_MaxAngularVelocity, m_MaxAngularVelocity);
            balloon.trans.localScale = Vector2.one * Random.Range(m_ScaleIntervalMin, m_ScaleIntervalMax);

            balloon.transform.parent = transform;
            yield return new WaitForSeconds(Random.Range(m_SpawnIntervalMin, m_SpawnIntervalMax));
        }
    }

    void OnEnable()
    {
        Balloon.OnContact -= Balloon_OnContact;
        Balloon.OnContact += Balloon_OnContact;
    }

    void OnDisable()
    {
        Balloon.OnContact -= Balloon_OnContact;
    }

    void Balloon_OnContact(object sender, Collision2D collision)
    {
        Balloon balloon = (Balloon)sender;
        if (collision.gameObject.layer == PLAYER_LAYER)
        {
            if (collision.collider is CapsuleCollider2D)
            {
                //Debug.Log($"{collision.collider is CapsuleCollider2D}");
                balloon.hits++;

                List<ContactPoint2D> items = collision.contacts.Where(x => x.separation < m_ReflectThreshold).ToList();

                if (items.Count() > 0)
                {
                    Debug.Log($"{items.Count} {items[0].separation} separation");
                }
                if (balloon.hits < m_ChangeSprites.Length)
                {
                    balloon.spriteRenderer.sprite = m_ChangeSprites[balloon.hits];
                }

                if(balloon.hits == m_MaxHits)
                {
                    GameObject popGO = Instantiate(m_PrefabsPop[0], balloon.transform.position, Quaternion.identity);
                    Destroy(balloon.gameObject);
                }
                SoundManager.instance.Play(Surface.Head);
            }
            else
            {
                SoundManager.instance.Play(Surface.Body);
            }
        }
        else if (collision.gameObject.layer == GROUND_LAYER)
        {
            SoundManager.instance.Play(Surface.Ground);
        }
        else if (collision.gameObject.layer == BALLOON_LAYER)
        {
            SoundManager.instance.Play(Surface.Balloon);
        }
    }
}
