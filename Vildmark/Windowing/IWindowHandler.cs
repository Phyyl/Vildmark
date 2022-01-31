namespace Vildmark.Windowing
{
    public interface IWindowHandler
	{
		void Load();
		void Unload();
		void Resize(int width, int height);
		void Update(float delta);
		void Render(float delta);
        void Close();
	}
}
