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
        // Arrange
        var description = "New Category";
        var createdCategory = new Category { Id = Guid.NewGuid(), Description = description };
        _mockCategoryLogic.Setup(x => x.CreateCategory(description)).ReturnsAsync(createdCategory);

        // Act
        var result = await _controller.CreateCategory(description);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetCategoryById", actionResult.ActionName);
        Assert.Equal(createdCategory, actionResult.Value);
        Assert.Equal(createdCategory.Id, actionResult.RouteValues["id"]);
    }
}
