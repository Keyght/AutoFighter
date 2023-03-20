using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Characters;
using Skills;
using UnityEngine;

namespace ForCheck
{
    public class MindControlCheck : CountableSkill
    {
        [SerializeField] private float _skillDuration;

        protected override async Task PerformSpell()
        {
            ControlEnemy();
        }

        private async Task ControlEnemy()
        {
            var list = Utilits.AllEnemies.Where(enemy => !enemy.IsBoss).ToList();
            if (list.Count == 0) return;

            var random = new System.Random();
            var next = random.Next(list.Count);
            var controlledEnemy = list[next];
            list.Clear();
            list.AddRange(Utilits.AllEnemies.Where(enemy => enemy != controlledEnemy));
            if (list.Count == 0) return;

            controlledEnemy.AttackingTag = AttackingTags.Player;
            var moveComponent = controlledEnemy.GetComponent<MoveToTargetCheck>();
            var player = moveComponent.Target.GetComponent<Player>();
            if (player.Targets.Contains(controlledEnemy)) player.Targets.Remove(controlledEnemy);
            var first = true;
            
            var timer = _skillDuration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                if (!moveComponent.Target.GetComponent<Character>().isActiveAndEnabled || first)
                {
                    first = false;
                    next = random.Next(list.Count);
                    moveComponent.Target = list[next].transform;
                    moveComponent.Move(moveComponent.CancellationTokenSource.Token);
                }
                await Task.Yield();
            }
            
            moveComponent.CancellationTokenSource.Cancel();
            await Task.Delay(100);
            moveComponent.CancellationTokenSource.Dispose();
            moveComponent.CancellationTokenSource = new CancellationTokenSource();
            foreach (var enemy in Utilits.AllEnemies.Where(enemy => enemy.Targets.Contains(controlledEnemy))) enemy.Targets.Remove(controlledEnemy);
            if ((controlledEnemy.transform.position - player.transform.position).magnitude < player.GetComponent<CircleCollider2D>().radius) player.Targets.Add(controlledEnemy);
            moveComponent.Target = player.transform;
            controlledEnemy.AttackingTag = AttackingTags.Enemy;
            await moveComponent.Move(moveComponent.CancellationTokenSource.Token);
        }
    }
}
