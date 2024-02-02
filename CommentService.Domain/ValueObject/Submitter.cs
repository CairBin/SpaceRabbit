

namespace CommentService.Domain.ValueObject;

public record class Submitter(string Name, Uri? SiteUrl, string Email, string? IpAddr, string? Agent);
