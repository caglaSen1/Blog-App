using BlogApp.Entity;

namespace BlogApp.Data.Abstract.BusinessRules
{
    public interface ITagBusinessRules
    {
        bool IsTagExistWithName(string name);
    }
}