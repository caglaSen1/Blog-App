using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents{

    public class PopularTagsMenu : ViewComponent{

        private readonly ITagRepository _tagRepository;

        public PopularTagsMenu(ITagRepository tagRepository){
            _tagRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            
            return View(await _tagRepository.GetPopularTags(6));
        }
    }
}