namespace MyProject.Data
{
    public interface IRecipeDbContextFactory
    {
        IRecipeDbContextFactory CreateDbContext();
    }
}
