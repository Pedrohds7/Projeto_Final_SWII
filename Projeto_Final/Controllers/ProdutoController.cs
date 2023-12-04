using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Projeto_Final.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace Projeto_Final.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ProdutoController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // ... (outros métodos)

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoDetalhes = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produtoDetalhes == null)
            {
                return NotFound();
            }

            return View(produtoDetalhes);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Status,IdUsuarioCadastro,IdUsuarioUpdate")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    produto.IdUsuarioCadastro = Convert.ToInt32(user.Id);

                    _context.Update(produto);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Produto {produto.Nome} atualizado por {user.UserName}.");

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar produto.");
                    ModelState.AddModelError(string.Empty, "Erro ao atualizar o produto. Tente novamente.");
                }
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            try
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();

                var user = await _userManager.GetUserAsync(User);
                _logger.LogInformation($"Produto {produto.Nome} excluído por {user.UserName}.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir produto.");
                ModelState.AddModelError(string.Empty, "Erro ao excluir o produto. Tente novamente.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
