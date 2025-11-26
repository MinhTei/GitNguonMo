using GitNguonMo.Api.Models;

namespace GitNguonMo.Api.Services;

public class DataStore
{
    private readonly List<Product> _products = new();
    private readonly List<User> _users = new();

    public DataStore()
    {
        // Seed some sample products
        _products.Add(new Product
        {
            Id = "ca-chua",
            Name = "Cà Chua Đà Lạt Hữu Cơ",
            Description = "Cà chua tươi ngon, hữu cơ từ Đà Lạt.",
            Price = 55_000m,
            Image = "/images/ca-chua.jpg",
            IsOrganic = true
        });
        _products.Add(new Product
        {
            Id = "dua-leo",
            Name = "Dưa Leo Hữu Cơ",
            Description = "Dưa leo tươi, giòn.",
            Price = 55_000m,
            Image = "/images/dua-leo.jpg",
            IsOrganic = true
        });
        _products.Add(new Product
        {
            Id = "bap-my",
            Name = "Bắp Mỹ Ngọt",
            Description = "Bắp ngọt, ngon cho bé.",
            Price = 12_000m,
            Image = "/images/bap.jpg",
            IsOrganic = false
        });
    }

    public IEnumerable<Product> GetProducts() => _products;

    public Product? GetProduct(string id) => _products.FirstOrDefault(p => p.Id == id);

    public void AddUser(User user) => _users.Add(user);

    public User? GetUserByEmail(string email) => _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
}
