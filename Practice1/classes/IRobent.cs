namespace Practice1
{
    public interface IRobent : INameClass
    {
        string Sex { get; set; }
        float Weight { get; set; }
        int Age { get; set; }
        
        public void Gnaw();
    }
}