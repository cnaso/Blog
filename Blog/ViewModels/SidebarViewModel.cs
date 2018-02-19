using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class SidebarViewModel
    {
        public IList<SidebarCategoryViewModel> Categories { get; set; }
        public IList<SidebarTagViewModel> Tags { get; set; }

        public SidebarViewModel()
        {
            Categories = new List<SidebarCategoryViewModel>();
            Tags = new List<SidebarTagViewModel>();
        }
    }

    public class SidebarCategoryViewModel
    {
        public string Name { get; set; }
        public int PostCount { get; set; }
    }

    public class SidebarTagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int PostCount { get; set; }
    }
}