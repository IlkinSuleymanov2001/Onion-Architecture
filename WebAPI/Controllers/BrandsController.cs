using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetById;
using Azure;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand request)
        {
            CreateBrandDto? response = await Mediator.Send(request);
            return Created("", response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetAllBrandQuery reuqest = new() { PageRequest = pageRequest };
            BrandListModel? response = await Mediator.Send(reuqest);
            return Created("", response);
        }
        [HttpPost("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery request)
        {
            GetByIdBrandDto response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
