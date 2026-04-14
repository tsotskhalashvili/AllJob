using AllJob.Application.DTOs.Blog;
using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Application.Interfaces.Services.Blog;
using AllJob.Domain.Enums.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController(
    IBlogService blogService,
    IContentModeratorService contentModeratorService,
    IValidator<CreateBlogPostDto> createBlogValidator) : AdminBaseController
{
    #region Public
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await blogService.GetAllAsync();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var result = await blogService.GetBySlugAsync(slug);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var result = await blogService.GetCategoriesAsync();
        return Ok(result);
    }
    #endregion

    #region Admin
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreateBlogPostDto dto)
    {
        if (!HasAccess(AdminRole.ContentModerator, AdminRole.FullAccess))
            return Forbid();
        await ValidateAsync(createBlogValidator, dto);
        var result = await contentModeratorService.CreateBlogPostAsync(dto, UserId);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] CreateBlogPostDto dto)
    {
        if (!HasAccess(AdminRole.ContentModerator, AdminRole.FullAccess))
            return Forbid();
        await ValidateAsync(createBlogValidator, dto);
        await contentModeratorService.UpdateBlogPostAsync(id, dto, UserId);
        return NoContent();
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        if (!HasAccess(AdminRole.ContentModerator, AdminRole.FullAccess))
            return Forbid();
        await contentModeratorService.DeleteBlogPostAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPatch("{id}/publish")]
    public async Task<IActionResult> PublishPost(Guid id)
    {
        if (!HasAccess(AdminRole.ContentModerator, AdminRole.FullAccess))
            return Forbid();
        await contentModeratorService.PublishBlogPostAsync(id);
        return NoContent();
    }
    #endregion
}