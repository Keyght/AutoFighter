using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Skills
{
    public class Meteor : CountableSkill
    {
        [SerializeField] private GameObject _meteor;
        [SerializeField] private float _baseDamage;
        [SerializeField] private float _bangRadius;
        [SerializeField] private int _fallTime;

        protected override async Task PerformSpell()
        {
            await CastMeteor();
        }

        private async Task CastMeteor()
        {
            var random = new Random();

            var x = (float) (random.NextDouble() - 0.5) * 1500;
            var y = (float) (random.NextDouble() - 0.5) * 1500;
            var meteorPos = new Vector3(x, y, 0);
            

            var meteor = Instantiate(_meteor, meteorPos , Quaternion.identity);
            var mainModule = meteor.GetComponent<ParticleSystem>().main;
            mainModule.startSize = (_bangRadius * 1.5f);
            meteor.GetComponent<MeteorObject>().Size = _bangRadius;
            await Task.Delay(_fallTime * 1000);

            var list = Utilits.AllEnemies.Where(enemy => (enemy.transform.position - meteorPos).magnitude < _bangRadius).ToList();

            Debug.Log("Done " + (_baseDamage - 10 * list.Count) + " damage to all");
            foreach (var enemy in list)
            {
                enemy.Health.ChangeHealth(_baseDamage - 10 * list.Count);
            }
            
            Destroy(meteor);
        }
    }
}
