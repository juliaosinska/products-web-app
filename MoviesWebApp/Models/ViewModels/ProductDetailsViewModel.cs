using MoviesWebApp.Models;

namespace ProductsWebApp.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
		public int Id { get; set; }

		public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime CreationDate { get; set; }

        public Guid CreatorUserId { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Category> Categories { get; set; }

        public int TotalLikes { get; set; }

        public bool Liked { get; set; }

        public string CommentDescription { get; set; }

        public IEnumerable<ProductCommentViewModel> Comments { get; set; }
    }
}
