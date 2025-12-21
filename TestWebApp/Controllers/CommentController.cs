using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestWebApp.Interfaces;
using TestWebApp.Mappers;

namespace TestWebApp.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            
            var comments = await _commentRepo.GetAllAsync();

            var commentDto = comments.Select(S => S.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) { 
            
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) {

                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
    }
}
