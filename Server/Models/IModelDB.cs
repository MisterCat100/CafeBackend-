namespace Server.Models
{
    public interface IModelDB
    {
        void AddOrder(Order order);
        void DeleteOrder(string orderName);
        List<Order> GetAllOrders();
        void EraseDataBase(string password);
    }
}