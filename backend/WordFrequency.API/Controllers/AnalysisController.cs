namespace WordFrequency.API.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IWordFrequencyService _service;
    private readonly ILogger<AnalysisController> _logger;

    public AnalysisController(IWordFrequencyService service, ILogger<AnalysisController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Analyzes text and returns word frequency results
    /// </summary>
    /// <param name="request">The request containing the text to analyze (max 2048 characters)</param>
    /// <returns>Word frequency analysis results</returns>
    /// <response code="200">Returns the frequency analysis</response>
    /// <response code="400">If the text is empty or exceeds 2048 characters</response>
    /// <response code="500">If an internal error occurs</response>
    [HttpPost]
    [ProducesResponseType(typeof(AnalyzeTextResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnalyzeTextResponse>> AnalyzeText(
        [FromBody] AnalyzeTextRequest request)
    {
        try
        {
            var result = await _service.AnalyzeTextAsync(request.Text);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Validation error: {Error}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Title = "Validation Error",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while analyzing text");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request",
                    Status = StatusCodes.Status500InternalServerError
                });
        }
    }

    /// <summary>
    /// Analyzes a web page URL and returns word frequency results
    /// </summary>
    /// <param name="request">The request containing the URL to analyze</param>
    /// <returns>Word frequency analysis results from the URL content</returns>
    /// <response code="200">Returns the frequency analysis</response>
    /// <response code="400">If the URL is invalid or content extraction fails</response>
    /// <response code="500">If an internal error occurs</response>
    [HttpPost("url")]
    [ProducesResponseType(typeof(AnalyzeTextResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnalyzeTextResponse>> AnalyzeUrl(
        [FromBody] AnalyzeUrlRequest request)
    {
        try
        {
            var result = await _service.AnalyzeUrlAsync(request.Url);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Validation error: {Error}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Title = "Validation Error",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Invalid operation: {Error}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid Operation",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while analyzing URL");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request",
                    Status = StatusCodes.Status500InternalServerError
                });
        }
    }
}
