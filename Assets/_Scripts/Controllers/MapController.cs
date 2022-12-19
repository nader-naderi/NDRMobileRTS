using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace NDRIsometricRTS
{
	public class MapController : MonoBehaviour
	{
		public static MapController Instance { get; private set; }
		
		[SerializeField] private Vector2Int MapDimensions = new Vector2Int(50, 50);

		[SerializeField] private GridLayout gridLayout;
		[SerializeField] private Tilemap tilemap;
		
		public Map Map { get; private set; }


		private void Awake()
		{
			Instance = this;
		}

		private void OnEnable()
		{
			CreateEmptyWorld();
		}

		private void CreateEmptyWorld()
		{
			Map = new Map(MapDimensions.x, MapDimensions.y);
		}

		// its for orthographics.
		//public Tile GetTileAtWorldCoord(Vector3 coord)
		//{
		//	int x = Mathf.FloorToInt(coord.x);
		//	int y = Mathf.FloorToInt(coord.y);

		//	return Map.GetTileAt(x, y);
		//}

		public Tile GetTileAtWorldCoord(Vector2 pos)
		{
			Vector2 p = Map.IsometricToCartesian(pos);
			
			return Map.GetTileAt(Mathf.FloorToInt(p.x), Mathf.FloorToInt(p.y));
		}

		public Tile GetTileAtWorldCoord(float x, float y)
		{
			int finalX = Mathf.Abs(Mathf.FloorToInt(x));
			int finalY = Mathf.Abs(Mathf.FloorToInt(y));

			return Map.GetTileAt(finalX, finalY);
		}

		public Tile GetTileAtWorldCoord(int x, int y)
		{
			return Map.GetTileAt(x, y);
		}
	}
}
