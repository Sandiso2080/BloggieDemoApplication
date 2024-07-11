using BloggieDemoApplication.Models.Domain;

namespace BloggieDemoApplication.Models.ViewModels
{
    public class HomeViewModel
    {
       public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
