using System;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Image { get; set; }

    public string Country { get; set; }

    public int UserId { get; set; }

    public int CommunityId { get; set; }

    public int MainCategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}