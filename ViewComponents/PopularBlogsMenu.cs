using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{

    public class PopularBlogsMenu : ViewComponent
    {

        private readonly IBlogRepository _blogRepository;

        public PopularBlogsMenu(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var popularBlogs = await _blogRepository.GetPopularBlogs(6);

            List<BlogDetailsViewModel> modelList = new List<BlogDetailsViewModel>();

            foreach (var blog in popularBlogs)
            {
                var commentCount = await _blogRepository.GetCommentCount(blog.Id);
                var likeCount = await _blogRepository.GetLikeCount(blog.Id);

                var model = new BlogDetailsViewModel
                {
                    Blog = blog,
                    CommentCount = commentCount,
                    LikeCount = likeCount
                };

                modelList.Add(model);
            }

            return View(modelList);
        }
    }
}