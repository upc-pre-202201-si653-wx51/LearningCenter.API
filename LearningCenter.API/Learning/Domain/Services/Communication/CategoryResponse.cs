using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class CategoryResponse : BaseResponse<Category>
{
    public CategoryResponse(string message) : base(message)
    {
    }

    public CategoryResponse(Category resource) : base(resource)
    {
    }
}