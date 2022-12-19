using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace NDRIsometricRTS
{
	public class TilesVisualsController : MonoBehaviour
	{
		[SerializeField] private Sprite groundSprite;
		[SerializeField] private Sprite floorSprite;
		[SerializeField] private Sprite emptySprite;
		[SerializeField] private CameraController cameraController;
		
		[SerializeField] private float tileSize = 1;

		private Dictionary<Tile, GameObject> tileObjectsMap = new Dictionary<Tile, GameObject>();

		Map Map { get => MapController.Instance.Map; }

		private void Start()
		{
			tileObjectsMap = new Dictionary<Tile, GameObject>();

			for (int x = 0; x < Map.Width; x++)
			{
				for (int y = 0; y < Map.Height; y++)
				{
					Tile newTile = Map.GetTileAt(x, y);

					GameObject tileGO = new GameObject();

					tileObjectsMap.Add(newTile, tileGO);

					tileGO.name = "Tile_" + x + "_" + y;

					float posX = (x * tileSize + y * tileSize) / 2f;
					float posY = (x * tileSize - y * tileSize) / 4f;

					tileGO.transform.position = new Vector3(posX, posY, 0);

					tileGO.transform.SetParent(transform, true);

					SpriteRenderer sprite = tileGO.AddComponent<SpriteRenderer>();
					sprite.sprite = groundSprite;
					sprite.sortingLayerName = "Tiles";

					int roundX = Mathf.FloorToInt(posX);
					int roundY = Mathf.FloorToInt(posY);

					Map.TilesPositionMap[new Vector2Int(roundX, roundY)] = newTile;
				}
			}

			Map.RegisterTileChanged(OnTileChanged);
		}

		void OnTileChanged(Tile tile_data)
		{
			if (tileObjectsMap.ContainsKey(tile_data) == false)
			{
				Debug.LogError("tileGameObjectMap doesn't contain the tile_data --- did you forget to add the tile to the dictionary ? or maybe forget to unregister a call bcak??");
				return;
			}

			GameObject tile_go = tileObjectsMap[tile_data];

			if (!tile_go)
			{
				Debug.LogError("tileGameObjectMap's returned GameObject is null --- did you forget to add the tile to dictionary?");
				return;
			}

			switch (tile_data.Type)
			{
				case ETileType.Ground:
					tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
					break;
				case ETileType.Empty:
					tile_go.GetComponent<SpriteRenderer>().sprite = emptySprite;
					break;
				default:
					Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
					break;
			}
		}

		
	}
}
