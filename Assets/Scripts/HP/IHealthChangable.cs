namespace HP
{
    public interface IHealthChangable
    {
        void OnHealthChanged(DamageData damageData);
    }
}