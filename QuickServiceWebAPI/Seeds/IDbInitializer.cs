namespace QuickServiceWebAPI.Seeds
{
    public interface IDbInitializer
    {
        public void SeedPermissions();
        public void SeedServiceCategories();
        public void SeedSla();
    }
}
