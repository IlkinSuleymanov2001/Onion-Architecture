using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public  class CreateBrandCommand : IRequest<CreateBrandDto>
    {
        public string  Name  { get; set; }
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreateBrandDto>
        {
            private readonly IMapper _mappper;
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(BrandBusinessRules brandBusinessRules, 
                IMapper mappper, IBrandRepository brandRepository)
            {
                _mappper = mappper;
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
;            }

            public async Task<CreateBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                 await _brandBusinessRules.BrandNameCanNotBeDupilcatedWhenInserted(request.Name);
                
                Brand mappedBrand  = _mappper.Map<Brand>(request);
                Brand createdBrand   =  await  _brandRepository.AddAsync(mappedBrand);
                var response = _mappper.Map<CreateBrandDto>(createdBrand);
                return response;
;               
            }
        }

    }
}
