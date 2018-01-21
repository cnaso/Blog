using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class Sidebar
    {
        public IList<CategoryViewModel> Categories { get; set; }
        public IList<TagViewModel> Tags { get; set; }

        public Sidebar()
        {
            Categories = new List<CategoryViewModel>();
            Tags = new List<TagViewModel>();
        }
    }

    public class CategoryViewModel
    {
        public string Name { get; set; }
        public int PostCount { get; set; }
    }

    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int PostCount { get; set; }
    }
}