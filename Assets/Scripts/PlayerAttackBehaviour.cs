using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    public GameObject hitAttackEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mover")
        {
            var mover = collision.GetComponentInParent<Mover>();

            if (mover.Spook())
            {
                var hitAttack = Instantiate(hitAttackEffect, mover.transform);
                Destroy(hitAttack, 2f);
            }

        }
    }
}
