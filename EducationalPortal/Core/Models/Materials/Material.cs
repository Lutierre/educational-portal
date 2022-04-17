namespace Core.Models.Materials
{
    public class Material : BaseEntity
    {
        public string Title { get; set; }

        public string Type => GetType().Name;
    }
}
