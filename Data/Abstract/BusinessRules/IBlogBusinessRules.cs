using BlogApp.Entity;

namespace BlogApp.Data.Abstract.BusinessRules
{
    public interface IBlogBusinessRules
    {
        bool AnyBlogExistWithTitle(string title);
    }
}