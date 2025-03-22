namespace Company.Momen1.PL.DTO.Services
{
    public interface ISingletonService 
    {
       
       
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
