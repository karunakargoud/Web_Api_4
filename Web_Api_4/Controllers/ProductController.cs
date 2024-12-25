using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using Web_Api_4.DAL;
using Web_Api_4.Models;

namespace Web_Api_4.Controllers
{
    public class ProductController : ApiController
    {
        private readonly OnlineMarketContext _context;
        public ProductController()
        {
            _context = new OnlineMarketContext();
        }
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            try
            {
                List<Product> plist = _context.Products.ToList();
                if (plist.Count > 0)
                {
                    HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.OK, plist);
                    return resp;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Product to Display");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetProductById(int id)
        {
            try
            {
                Product p = _context.Products.Find(id);
                if (p == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "with given id " + id + " No Product found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, p);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteProductById(int id)
        {
            try
            {
                Product p = _context.Products.Find(id);
                if (p == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "with given Id " + id + "No Product found to Delete");
                }
                else
                {
                    _context.Products.Remove(p);
                    _context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, p);
                }


            }
            catch(Exception ex)
            {
               return  Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    Product x = _context.Products.Find(product.ProductId);
                    return Request.CreateResponse(HttpStatusCode.Created, x);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
        [HttpPut]
        public HttpResponseMessage updateProduct(int id,Product product)
        {
            try
            {
                if (id == product.ProductId)
                {
                    if (ModelState.IsValid)
                    {
                        Product p = _context.Products.Find(product.ProductId);
                        if (p != null)
                        {
                            p.ProductName = product.ProductName;
                            p.Quantity = product.Quantity;
                            p.Price = product.Price;
                            Product x = _context.Products.Find(id);
                            return Request.CreateResponse(HttpStatusCode.OK, x);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "With the given Id " + id + " no Product found to Update");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Product Data");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Id Mismatch");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPatch]
        public HttpResponseMessage updateProductPrice(int id,Product product)
        {
            try
            {
                if (id == product.ProductId)
                {
                    if (ModelState.IsValid)
                    {
                        Product p = _context.Products.Find(product.ProductId);

                        if (product != null)
                        {
                            p.Price = product.Price;
                            _context.SaveChanges();
                            Product x = _context.Products.Find(id);
                            return Request.CreateResponse(HttpStatusCode.OK, x);

                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "with the given id" + id + "no Product Found to update");
                        }


                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Product data");

                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Id Mismatch");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
