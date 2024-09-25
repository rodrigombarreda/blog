using BackEnd.Controllers;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class CategoryControllerTests
{
    private readonly Mock<ICategoryLogic> _mockCategoryLogic;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _mockCategoryLogic = new Mock<ICategoryLogic>();
        _controller = new CategoryController(_mockCategoryLogic.Object);
    }

    [Fact]
    public async Task CreateCategory_ReturnsCreatedAtActionResult_WhenCategoryIsCreated()
    {
        var description = "New Category";
        var createdCategory = new Category { Id = Guid.NewGuid(), Description = description };
        _mockCategoryLogic.Setup(x => x.CreateCategory(description)).ReturnsAsync(createdCategory);

        var result = await _controller.CreateCategory(description);

        
        var actionResult = Xunit.Assert.IsType<CreatedAtActionResult>(result.Result);
        Xunit.Assert.Equal("GetCategoryById", actionResult.ActionName);
        Xunit.Assert.Equal(createdCategory, actionResult.Value);
        Xunit.Assert.Equal(createdCategory.Id, actionResult.RouteValues["id"]);
    }

    [Fact]
    public async Task GetAllCategories_ReturnsOkResult_WithListOfCategories()
    {
        var categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Description = "Category 1" },
            new Category { Id = Guid.NewGuid(), Description = "Category 2" }
        };
        _mockCategoryLogic.Setup(x => x.GetAllCategories()).ReturnsAsync(categories);

        var result = await _controller.GetAllCategories();

        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnCategories = Assert.IsType<List<Category>>(actionResult.Value);
        Assert.Equal(2, returnCategories.Count);
    }
}
