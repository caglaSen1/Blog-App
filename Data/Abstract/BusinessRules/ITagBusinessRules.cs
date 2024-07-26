using BlogApp.Entity;

namespace BlogApp.Data.Abstract.BusinessRules
{
    public interface ITagBusinessRules
    {
        bool AnyTagExistWithName(string name);
    }
}