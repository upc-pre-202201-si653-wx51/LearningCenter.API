using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Learning.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class TutorialsController : ControllerBase
{
    private readonly ITutorialService _tutorialService;
    private readonly IMapper _mapper;

    public TutorialsController(ITutorialService tutorialService, IMapper mapper)
    {
        _tutorialService = tutorialService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TutorialResource>> GetAllAsync()
    {
        var tutorials = await _tutorialService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Tutorial>, IEnumerable<TutorialResource>>(tutorials);

        return resources;

    }
    
    
}