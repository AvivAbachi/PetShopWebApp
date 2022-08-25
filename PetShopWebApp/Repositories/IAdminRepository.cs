namespace PetShopWebApp.Repositories
{
    public interface IAdminRepository
    {
        void AddAnimal();
        void EditAnimal();
        bool Login();
        void RemoveAnimal();
    }
}