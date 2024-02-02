using CategoryService.Domain.Entity;

namespace CategoryService.Domain;
public interface ICategoryRepo{
    /// <summary>
    /// 创建分类
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(Category category);

    public Task<Category[]> GetAllAsync();

    public Task<Category?> FindByNameAsync(string name);

    public Task<Category?> FindByIdAsync(Guid id);


}