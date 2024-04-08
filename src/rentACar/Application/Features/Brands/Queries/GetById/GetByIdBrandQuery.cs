using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetById
{
    public  class GetByIdBrandQuery:IRequest<GetByIdBrandDto>
    {

        public int Id { get; set; }

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandDto>
        {

            private readonly IMapper _mapper;
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;

            public GetByIdBrandQueryHandler(IMapper mapper, 
                IBrandRepository brandRepository, 
                BrandBusinessRules brandBusinessRules)
            {
                _mapper = mapper;
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
            }

            public async  Task<GetByIdBrandDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
               Brand?  brand =  await _brandRepository.GetAsync(b => b.Id == request.Id);
               _brandBusinessRules.BrandShouldExistsWhenRequested(brand);
               GetByIdBrandDto brandDto =  _mapper.Map<GetByIdBrandDto>(brand);
               return brandDto;
            }
        }
    }
}
