namespace SlotMachine.Domain
{
    /// <summary>
    /// Random value generator
    /// </summary>
    public interface IRandomValueGenerator
    {
        /// <summary>
        /// Generate random decimal
        /// </summary>
        /// <returns>decimal value</returns>
        decimal GetRandomdecimal();
    }
}
