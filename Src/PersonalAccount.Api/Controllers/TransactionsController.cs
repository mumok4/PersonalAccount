using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Common.Core;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ILoadingService _loadingService;

    public TransactionsController(ILoadingService loadingService)
    {
        _loadingService = loadingService;
    }

    [HttpPost("push/{companyId}")]
    public async Task<IActionResult> Push(Guid companyId, [FromBody] IEnumerable<JournalRowDto> transactions, CancellationToken token)
    {
        try
        {
            var company = new CompanyModel { Id = companyId }; 
            var result = await _loadingService.PushAsync(company, transactions, token);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }
}
