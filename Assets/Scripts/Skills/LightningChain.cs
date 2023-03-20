using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using UnityEngine;
using Random = System.Random;

namespace Skills
{
    public class LightningChain : CountableSkill
    {
        [SerializeField] private Character _player;
        [SerializeField] private float _range = 200;
        [SerializeField] private int _spellTicks = 6;
        [SerializeField] private int _damage = -7;

        private List<Enemy> _alreadyBeen;

        private void Awake()
        {
            _alreadyBeen = new List<Enemy>();
        }

        protected override async Task PerformSpell()
        {
            _alreadyBeen.Clear();
            await TickEnemies();
        }

        private Task TickEnemies()
        {
            var random = new Random();
            var currentTickTransform = _player.transform;
            for (var i = 0; i < _spellTicks; i++)
            {
                var targetsInRange = Utilits.AllEnemies.Where(character => (character.transform.position - currentTickTransform.position).magnitude <= _range).ToList();
                if (targetsInRange.Count == 0) return Task.CompletedTask;
                
                while (targetsInRange.Count > 0)
                {
                    var next = random.Next(targetsInRange.Count);
                    var nextChar = targetsInRange[next];
                    if (!_alreadyBeen.Contains(nextChar))
                    {
                        nextChar.Health.ChangeHealth(_damage);
                        _alreadyBeen.Add(nextChar);
                        currentTickTransform = nextChar.transform;
                        break;
                    }
                    targetsInRange.Remove(nextChar);
                }
            }
            return Task.CompletedTask;
        }
    }
}
