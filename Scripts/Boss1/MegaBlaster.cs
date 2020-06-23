using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBlaster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDamage.instance.Damage(10);
        }
    }
}
