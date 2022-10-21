using Application.InfoCards.AddCard;
using Application.InfoCards.DeleteInfoCard;
using Application.InfoCards.GetImage;
using Application.InfoCards.GetInfoCards;
using Application.InfoCards.UpdateCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : BaseController
    {
        [HttpGet("getImage")]
        public async Task<IActionResult> GetCard([FromQuery]GetImageQuery query)
        {
            return await Mediator.Send(query);            
        }

        [HttpGet("getInfocards")]
        public async Task<IActionResult> GetInfoCards()
        {
            var query = new GetInfoCardsQuery();
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("addCard")]
        public async Task<IActionResult> AddCard(IFormFile uploadedFile, [FromForm]AddCardCommand command)
        {
            command.Image = uploadedFile;
            var result = await Mediator.Send(command);
                
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPut("updateCard")]
        public async Task<IActionResult> UpdateCard(IFormFile uploadedFile, [FromForm] UpdateCardCommand command)
        {
            command.Image = uploadedFile;
            var result = await Mediator.Send(command);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("deleteCard")]
        public async Task<IActionResult> DeleteCard([FromQuery] DeleteCardQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
