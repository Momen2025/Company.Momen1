namespace Company.Momen1.PL.DTO.Services
{
    public interface ITransentService
    {
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
