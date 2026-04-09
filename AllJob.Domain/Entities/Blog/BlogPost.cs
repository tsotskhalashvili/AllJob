    using AllJob.Domain.Common;
    using AllJob.Domain.Entities.Auth;

    namespace AllJob.Domain.Entities.Blog;

    public class BlogPost : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public int ViewCount { get; set; }

        public Guid AuthorId { get; set; }
        public Guid BlogCategoryId { get; set; }
        public User Author { get; set; } = null!;
        public BlogCategory Category { get; set; } = null!;
    }
