namespace HP
{
    public struct DamageData
    {
        public readonly float CurrentHp;
        public readonly float CurrentHpAsPercantage;

        public DamageData(float currentHp, float currentHpAsPercantage)
        {
            CurrentHp = currentHp;
            CurrentHpAsPercantage = currentHpAsPercantage;
        }
    }
}