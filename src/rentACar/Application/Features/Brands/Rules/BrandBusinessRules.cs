using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Rules
{
    public  class BrandBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        public BrandBusinessRules(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task BrandNameCanNotBeDupilcatedWhenInserted(string name) 
        {
            IPaginate<Brand> result =await  _brandRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) {
                throw new BusinessException("Brand Name Exists..");
            }

        }
    }
}
