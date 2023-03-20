using System.Linq;
using System.Threading.Tasks;
using Characters;
using Skills;
using UnityEngine;

namespace ForCheck
{
    /// <summary>
    /// Класс для работоспособности тестовой сцены
    /// </summary>
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
            next = random.Next(list.Count);
            moveComponent.Target = list[next].transform;
            var targetMove = moveComponent.Target.GetComponent<MoveToTargetCheck>();
            moveComponent.Move(moveComponent.CancellationTokenSource.Token);
            Debug.Log(targetMove);
            
            var timer = _skillDuration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                //if (moveComponent.CancellationTokenSource.Token.IsCancellationRequested) break;
                await Task.Yield();
            }
            
            Debug.Log(targetMove);
            if (targetMove is not null)
            {
                targetMove.Target = player.transform;
                await targetMove.ReMove();
            }
            foreach (var enemy in Utilits.AllEnemies.Where(enemy => enemy.Targets.Contains(controlledEnemy))) enemy.Targets.Remove(controlledEnemy);
            if ((controlledEnemy.transform.position - player.transform.position).magnitude < player.GetComponent<CircleCollider2D>().radius) player.Targets.Add(controlledEnemy);
            moveComponent.Target = player.transform;
            controlledEnemy.AttackingTag = AttackingTags.Enemy;
            await moveComponent.ReMove();
        }
    }
}
