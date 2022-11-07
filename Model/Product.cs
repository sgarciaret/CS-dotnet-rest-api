namespace ApiRest.Modelo
{
    public class Product
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime RegistrationDate { get; init; }

        public string SKU { get; init; }
    }
}
