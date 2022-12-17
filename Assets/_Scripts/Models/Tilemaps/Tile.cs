using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDRIsometricRTS
{
	public enum ETileType { Empty, Ground }

	public class Tile
	{
		private int x;
		private int y;
		private Map map;

		Action<Tile> OnTileChanged;

		ETileType type = ETileType.Empty;

		public ETileType Type
		{
			get { return type; }

			set 
			{ 
				ETileType oldType = type; type = value;
				if (OnTileChanged != null && oldType != type) 
					OnTileChanged(this);
			}
		}

		public Tile(Map map, int x, int y)
		{
			this.map = map;
			this.x = x;
			this.y = y;
		}

		public int X { get => x; private set => x = value; }
		public int Y { get => y; private set => y = value; }

		public void RegisterOnTileTypeChangedCallback(Action<Tile> callback)
		{
			OnTileChanged += callback;
		}

		public void UnregisterOnTileTypeChangedCallback(Action<Tile> callback)
		{
			OnTileChanged -= callback;
		}
	}
}