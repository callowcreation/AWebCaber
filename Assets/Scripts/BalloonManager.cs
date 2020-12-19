using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    [System.Serializable]
    public class BalloonPrefab
    {
        public Balloon ballon;
        public Sprite[] sprites;
    }

    [SerializeField]
    float m_StartY = 6.0f;

    [SerializeField]
    float m_XBounds = 7.0f;

    [SerializeField]
    float m_MaxAngularVelocity = 50.0f;

    [SerializeField]
    float m_SpawnIntervalMin = 5.0f;
    [SerializeField]
    float m_SpawnIntervalMax = 7.0f;

    [SerializeField]
    float m_ReflectThreshold = -0.15f;

    [SerializeField]
    BalloonPrefab[] m_Prefabs = null;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true)
        {
            Values.BalloonTypes balloonType = Values.BalloonTypes.Normal;

            SpawnBalloon(balloonType);
            yield return new WaitForSeconds(Random.Range(m_SpawnIntervalMin, m_SpawnIntervalMax));
        }
    }

    public void SpawnBalloon(Values.BalloonTypes balloonType)
    {
        int index = (int)balloonType;
        Vector3 spawnVector = new Vector3(Random.Range(-m_XBounds, m_XBounds), m_StartY, 0.0f);
        Balloon balloon = Instantiate(m_Prefabs[index].ballon, spawnVector, Quaternion.identity);
        balloon.rb.angularVelocity = Random.Range(-m_MaxAngularVelocity, m_MaxAngularVelocity);
        balloon.trans.localScale = Vector2.one * Random.Range(balloon.scaleMin, balloon.scaleMax);

        balloon.transform.parent = transform;
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
        if (collision.gameObject.layer == Values.PLAYER_LAYER)
        {
            if (collision.collider is CapsuleCollider2D)
            {
                List<ContactPoint2D> items = collision.contacts.Where(x => x.separation < m_ReflectThreshold).ToList();

                if (items.Count() > 0)
                {
                    Debug.Log($"{items.Count} {items[0].separation} separation");
                }

                int index = (int)balloon.balloonType;
                if (balloon.hits < m_Prefabs[index].sprites.Length)
                {
                    balloon.spriteRenderer.sprite = m_Prefabs[index].sprites[balloon.hits];
                }
                if (balloon.hits == balloon.maxHits)
                {
                    Destroy(balloon.gameObject);
                }
            }
        }
    }
}
