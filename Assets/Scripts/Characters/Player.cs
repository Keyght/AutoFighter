using System;

namespace Characters
{
    public class Player : Character
    {
        private new void Start()
        {
            base.Start();
            AttackingTag = "Player";
        }
    }
}
