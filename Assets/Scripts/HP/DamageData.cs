namespace HP
{
    /// <summary>
    /// Структура, которая сожержит в себе здоровье и процент от максимального здоровья
    /// </summary>
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