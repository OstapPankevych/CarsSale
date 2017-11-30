namespace CarsSale.DataAccess.DTO
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Sign { get; set; }

        public Currency() { }

        public Currency(CURRENCY entity)
        {
            if (entity == null) return;
            Id = entity.ID;
            Code = entity.CODE;
            Sign = entity.SIGN;
        }
    }
}