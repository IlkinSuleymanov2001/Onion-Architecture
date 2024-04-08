using Application.Features.Brands.Models;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetAllBrand
{
    public  class GetAllBrandQuery:IRequest<BrandListModel>
    {
       public PageRequest PageRequest {  get; set; }

        public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, BrandListModel>
        {
            private readonly IMapper _mappper;
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;

            public GetAllBrandQueryHandler(BrandBusinessRules brandBusinessRules,
                IMapper mappper, IBrandRepository brandRepository)
            {
                _mappper = mappper;
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
                
            }
            public async Task<BrandListModel> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands= await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                BrandListModel mappedBrandModel = _mappper.Map<BrandListModel>(brands);
                return mappedBrandModel;
            }
        }
    }
}
