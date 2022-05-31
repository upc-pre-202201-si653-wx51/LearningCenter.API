using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/categories/{categoryId}/tutorials")]
public class CategoryTutorialsController : ControllerBase
{
    private readonly ITutorialService _tutorialService;
    private readonly IMapper _mapper;

    public CategoryTutorialsController(ITutorialService tutorialService, IMapper mapper)
    {
        _tutorialService = tutorialService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TutorialResource>> GetAllByCategoryIdAsync(int categoryId)
    {
        var tutorials = await _tutorialService.ListByCategoryIdAsync(categoryId);

        var resources = _mapper.Map<IEnumerable<Tutorial>, IEnumerable<TutorialResource>>(tutorials);

        return resources;
    }
}