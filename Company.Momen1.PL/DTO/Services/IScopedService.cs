namespace Company.Momen1.PL.DTO.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
        //string GetGuid();
        public string GetGuid()
        {
            return Guid.ToString();
        }

    }
}
