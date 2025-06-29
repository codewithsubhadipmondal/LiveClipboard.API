using LiveClipboard.API.Interfaces;
using System.Collections.Concurrent;

namespace LiveClipboard.API.Services
{
	public class ClipboardService : IClipboardService
	{
		private readonly ConcurrentDictionary<string, string> _store = new();

		public void SaveText(string key, string text)
		{
			_store[key] = text;
		}

		public string? GetText(string key)
		{
			_store.TryGetValue(key, out var value);
			return value;
		}
	}
}
