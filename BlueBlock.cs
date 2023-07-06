using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlock : MonoBehaviour
{
void OnCollisionEnter2D(Collision2D collider)
{
    if(collider.gameObject.tag == "BlueBullet")
    {
        Destroy(this);
    }
}
}
