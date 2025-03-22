namespace Company.Momen1.PL.DTO.Services
{
    public class SingleService :ISingletonService
    {
        public SingleService()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
