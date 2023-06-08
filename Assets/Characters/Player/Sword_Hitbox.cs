using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Hitbox : MonoBehaviour
{


    Collider2D sword_collider;


    private void Start()
    {
        sword_collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        PlayerController player = gameObject.GetComponentInParent<PlayerController>();
        if (player.is_flipped)
        {
            sword_collider.transform.localScale = new Vector3(-1, 1);
        } else
        {

            sword_collider.transform.localScale = new Vector3(1, 1);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().Hit();
        }
    }

}
