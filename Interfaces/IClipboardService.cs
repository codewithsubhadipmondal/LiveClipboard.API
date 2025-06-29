namespace LiveClipboard.API.Interfaces
{
	public interface IClipboardService
	{
		void SaveText(string key, string text);
		string? GetText(string key);
	}
}
