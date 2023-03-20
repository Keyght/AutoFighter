using System.Threading.Tasks;

namespace Skills
{
    public class Meteor : CountableSkill
    {
        protected override async Task PerformSpell()
        {
            await CastMeteor();
        }

        private async Task CastMeteor()
        {
            
        }
    }
}
