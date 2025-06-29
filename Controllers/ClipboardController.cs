using LiveClipboard.API.Interfaces;
using LiveClipboard.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiveClipboard.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClipboardController : ControllerBase
	{
		private readonly IClipboardService _clipboardService;

		public ClipboardController(IClipboardService clipboardService)
		{
			_clipboardService = clipboardService;
		}

		[HttpPost]
		public IActionResult SaveText([FromBody] ClipboardModel model)
		{
			_clipboardService.SaveText(model.Key, model.Text);
			return Ok(new { message = "Text saved successfully" });
		}

		[HttpGet("{key}")]
		public IActionResult GetText(string key)
		{
			var text = _clipboardService.GetText(key);
			if (text == null)
				return NotFound(new { message = "Key not found" });

			return Ok(new { text });
		}
	}
}
