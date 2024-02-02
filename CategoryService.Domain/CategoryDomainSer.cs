
using DomainCommons.Models;
using CategoryService.Domain.Entity;
namespace CategoryService.Domain
{
    public class CategoryDomainSer
    {
        private readonly ICategoryRepo _repository;
        public CategoryDomainSer(ICategoryRepo repository)
        {
            _repository = repository;
        }

        public async Task<bool> JudgeExist(string name)
        {
            Category? judge = await this._repository.FindByNameAsync(name);
            if(judge == null)
                return false;
            return true;
        }

        public async Task<Guid> CreateAsync(string name)
        {
            bool flag = await JudgeExist(name);
            if (flag)
                throw new Exception($"分类已经存在, Name = {name}");
            Category category = new Category(name);
            return await _repository.CreateAsync(category);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Category? category = await this._repository.FindByIdAsync(id);
            if (category == null)
                return false;

            category.SoftDelete();
            return true;

        }


    }
}
