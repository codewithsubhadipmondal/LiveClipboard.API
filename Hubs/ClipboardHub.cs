using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LiveClipboard.API.Hubs
{
	public class ClipboardHub : Hub
	{
		private static readonly ConcurrentDictionary<string, string> ClipboardStore = new();

		public override async Task OnConnectedAsync()
		{
			var key = Context.GetHttpContext()?.Request.Query["key"].ToString();

			if (!string.IsNullOrWhiteSpace(key))
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, key);

				if (ClipboardStore.TryGetValue(key, out var existingText))
				{
					await Clients.Caller.SendAsync("ReceiveClipboardText", existingText);
				}
			}

			await base.OnConnectedAsync();
		}

		public async Task SendClipboardText(string text)
		{
			var key = Context.GetHttpContext()?.Request.Query["key"].ToString();

			if (!string.IsNullOrWhiteSpace(key))
			{
				ClipboardStore[key] = text;
				await Clients.Group(key).SendAsync("ReceiveClipboardText", text);
			}
		}
	}
}
