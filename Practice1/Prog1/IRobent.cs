namespace Practice1
{
    public interface IRobent : INameClass
    {
        string Sex { get; set; }
        int Weight { get; set; }
        int Height { get; set; }
        
        public void Gnaw();
    }
}