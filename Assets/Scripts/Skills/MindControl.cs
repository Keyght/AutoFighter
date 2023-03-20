using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Skills
{
    public class MindControl : CountableSkill
    {
        [SerializeField] private float _skillDuration;

        protected override async Task PerformSpell()
        {
            
        }

        private async Task ControlEnemy()
        {
            var list = Spawner.AllEnemies.Where(enemy => !enemy.IsBoss).ToList();
            if (list.Count == 0) return;

            var random = new System.Random();
            var next = random.Next(list.Count);
            var controlledEnemy = list[next];
            controlledEnemy.AttackingTag = AttackingTags.Player;
            
            var timer = _skillDuration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                await Task.Yield();
            }

            controlledEnemy.AttackingTag = AttackingTags.Enemy;
        }
    }
}
