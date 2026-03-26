using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSD.Api.Models;
using TSD.Domain.Commands.Requests.Customers;
using TSD.Domain.Queries.Requests.Customers;

namespace TSD.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CustomersController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new customer in the Springfield Dungeon.
    /// </summary>
    /// <param name="request">The customer details.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CustomerDto request)
    {
        // 1. Map DTO to Command
        var command = _mapper.Map<AddCustomerCommandRequest>(request);

        try
        {
            // 2. Send Command to the Domain Handler via MediatR
            Guid customerId = await _mediator.Send(command);

            // 3. Return 201 Created with the new ID
            return CreatedAtAction(nameof(GetById), new { id = customerId }, new { id = customerId });
        }
        catch (InvalidOperationException ex)
        {
            // Handle business logic errors (like duplicate ExternalId)
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Dispatch the Query through MediatR
        var result = await _mediator.Send(new GetCustomerByIdQueryRequest(id));

        if (result == null)
        {
            return NotFound(new { Message = $"Customer with ID {id} not found." });
        }

        var dto = _mapper.Map<CustomerDto>(result);

        return Ok(dto);
    }
}