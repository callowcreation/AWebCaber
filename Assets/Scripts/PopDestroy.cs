using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopDestroy : MonoBehaviour
{
    // Called from the animator
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
