namespace CarsSale.DataAccess.DTO
{
    public class Fuel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Fuel() { }

        public Fuel(FUEL entity = null)
        {
            if (entity == null) return;
            Id = entity.ID;
            Name = entity.NAME;
        }

        public FUEL Convert() =>
            new FUEL
            {
                ID = Id,
                NAME = Name
            };
    }
}
