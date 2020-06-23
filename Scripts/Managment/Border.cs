using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private BoxCollider2D _borderCollider;

    private void Awake()
    {
        _borderCollider = GetComponent<BoxCollider2D>();
        transform.position = new Vector3(0, 0, 0);
        _borderCollider.offset = new Vector3(0, 0, 0);
        _borderCollider.size = new Vector3(23, 13, 0);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player_Projectile" || collision.tag == "Enemy_Projectile")
        {
            Destroy(collision.gameObject, 0.2f);
        }

        else if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject, 2);
        }

        else if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject, 3);
        }

        else if (collision.tag == "Background")
        {
            Debug.Log("here");
            Destroy(collision.gameObject);
        }
    }
}
