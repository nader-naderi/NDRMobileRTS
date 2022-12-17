using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace NDRIsometricRTS
{
	public class Map
	{
		Tile[,] tiles;

		private int width;
		private int height;

		public Dictionary<Vector2, Tile> TilesPositionMap { get; set; } = new Dictionary<Vector2, Tile>();


		System.Action<Tile> onTileChangedCallBack;

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

			Debug.Log("World created with " + (width * height) + " tiles.");
		}

		public Tile GetTileAt(int x, int y)
		{
			if (x >= Width || x < 0 || y >= Height || y < 0)
			{
				Debug.LogError("Tile (" + x + "," + y + ") is out of range.");
				return null;
			}

			return tiles[x, y];
		}

		public void RegisterTileChanged(System.Action<Tile> callback) => onTileChangedCallBack += callback;

		public void UnregisterTileChanged(System.Action<Tile> callback) => onTileChangedCallBack -= callback;

		private void OnTileChanged(Tile t)
		{
			if (onTileChangedCallBack == null)
				return;

			onTileChangedCallBack(t);

			InvalidateTileGraph();
		}

		public void InvalidateTileGraph()
		{
			//tileGraph = null;
		}

		public static Vector2 CartesianToIsometric(Vector2 cartPt)
		{
			Vector2 tempPt = new Vector2();
			tempPt.x = cartPt.x - cartPt.y;
			tempPt.y = (cartPt.x + cartPt.y) / 2;
			return (tempPt);
		}

		public static Vector2 IsometricToCartesian(Vector2 isoPt)
		{
			Vector2 tempPt = new Vector2();
			tempPt.x = (2 * isoPt.y + isoPt.x) / 2;
			tempPt.y = (2 * isoPt.y - isoPt.x) / 2;
			return (tempPt);
		}
	}
}
