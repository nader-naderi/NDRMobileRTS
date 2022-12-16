namespace NDRIsometricRTS
{
	public class Map
	{
		Tile[,] tiles;

		private int width;
		private int height;

		System.Action onTileChangedCallback;

		public int Width { get => width; }
		public int Height { get => height; }

		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
			tiles = new Tile[width, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					tiles[x, y] = new Tile(this, x, y);
				}
			}
		}
	}
}
