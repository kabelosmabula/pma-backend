namespace API.Service.Dtos
{
    public class RolesDto
    {
        public Guid id { get; set; }

        public string Name { get; private set; } = string.Empty;    
    }
}
