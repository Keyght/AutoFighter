using UnityEngine;

namespace Characters
{
    public class Enemy : Character
    {
        private new void Start()
        {
            base.Start();
            AttackingTag = "Enemy";
        }
    }
}
