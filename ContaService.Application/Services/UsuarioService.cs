using System;
using System.Linq;
using System.Threading.Tasks;
using ContaService.Domain.Interfaces;
using ContaService.Domain.Models;
using ContaService.Domain.SeedWork;
using ContaService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ContaService.Application.Services
{
    public class UsuarioService 
    {
        private readonly ContaServiceContext _context;

        public UsuarioService(ContaServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Usuario Find(int id)
        {
           return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }
    }
}