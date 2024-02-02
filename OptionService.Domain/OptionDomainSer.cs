using OptionService.Domain.Entity;
namespace OptionService.Domain
{
    public class OptionDomainSer
    {
        private readonly IOptionRepo _repo;

        public OptionDomainSer(IOptionRepo repo)
        {
            this._repo = repo;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Option? option = await this._repo.FindByIdAsync(id);
            if (option == null)
                return false;

            option.SoftDelete();
            return true;
        }
    }
}
