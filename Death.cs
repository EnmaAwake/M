using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D colider)
    {
        if(colider.gameObject.tag=="RedEnemy" || colider.gameObject.tag=="BlueEnemy")
        {
            Destroy(this.gameObject);
        }
    }
}
