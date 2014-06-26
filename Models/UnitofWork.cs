using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MvcWorking.Models{

    public class UnitOfWork : IDisposable
    {
        private APIContext context;

        public UnitOfWork()
        {
            context = new APIContext();
           //context.Products.Include(p => p.Category);
        }

        private APIRepository<Product> productRepository;
        public APIRepository<Product> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new APIRepository<Product>(context);                    
                }
                return productRepository;
            }
        }

        private APIRepository<Category> categoryRepository;
        public APIRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new APIRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

     
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}