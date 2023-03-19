namespace HP
{
    public interface IHealthChangable
    {
        void OnHealthChanged(int currentHealth, float currentHealthAsPercantage);
    }
}