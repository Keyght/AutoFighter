using System;
using UnityEngine;

namespace Characters
{
    public class Player : Character
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col.gameObject);
            var enemy = col.GetComponentInParent<Enemy>();
            if (enemy is not null)
            {
                Target = enemy;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<Enemy>();
            if (enemy is not null)
            {
                if (Target == enemy) Target = null;
            }
        }
    }
}
