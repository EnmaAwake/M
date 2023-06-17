using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "RedEnemy")
    {
        Destroy(hit.gameObject);
    }
        Debug.Log(hit.name);
        Destroy(gameObject);
    }
}
