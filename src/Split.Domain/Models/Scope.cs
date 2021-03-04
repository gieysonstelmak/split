namespace Split.Model
{
    public class Scope
    {
        public Scope(string code, bool isActive)
        {
            Code = code;
            IsActive = isActive;
        }

        public Scope()
        {
        }

        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}