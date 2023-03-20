namespace HP
{
    /// <summary>
    /// Интерфейс, для сохранения метода изменения здоровья
    /// </summary>
    public interface IHealthChangable
    {
        void OnHealthChanged(DamageData damageData);
    }
}