namespace MinimalApiDemo
{
    public class Dragon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EElementalType ElementalType { get; set; }

    }
    public enum EElementalType
    {
        Fire,
        Water,
        Earth,
        Wind,
        Ice,
        Electric
    }
}