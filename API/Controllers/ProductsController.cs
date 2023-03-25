using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
       
       // Defining Constructor of the mentioned repository class.
        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType> productTypeRepo){
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
       }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var spec = new ProductsWithTypesAndBrandsSpecifications();
            var prods = await _productRepo.ListAsync(spec);
            return Ok(prods);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            var spec  = new ProductsWithTypesAndBrandsSpecifications(id);
            return await _productRepo.GetEntityWithSpec(spec);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand(){
            return Ok(await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType(){
            return Ok(await _productTypeRepo.ListAllAsync());
        }

    }
}