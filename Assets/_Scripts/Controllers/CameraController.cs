using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;

namespace NDRIsometricRTS
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private GameObject outline;
		[SerializeField] private float dragSpeed = 2f;
		private Vector3 lastFramePosition;
		private Vector3 dragStartPosition;

		private void Update()
		{
			Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currFramePosition.z = -10;

			UpdateTileSelection(currFramePosition);

			UpdateCameraMovement(currFramePosition);

			lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currFramePosition.z = -10;
		}

		private void UpdateTileSelection(Vector3 currFramePosition)
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			if (Input.GetMouseButtonDown(0))
			{
				dragStartPosition = currFramePosition;

				int startX = Mathf.FloorToInt(dragStartPosition.x);
				int startY = Mathf.FloorToInt(dragStartPosition.y);

				Tile selectedTile = MapController.Instance.Map.GetTileAt(new Vector2Int(startX, startY));

				if (selectedTile == null)
				{
					outline.SetActive(false);
					return;
				}

				outline.SetActive(true);

				outline.transform.localPosition = new Vector3(startX, startY, 0);
			}
		}

		void UpdateCameraMovement(Vector3 currFramePosition)
		{
			if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
			{
				Vector3 dif = lastFramePosition - currFramePosition;
				Camera.main.transform.Translate(dif * Time.deltaTime * dragSpeed);
			}

			Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1f, 5f);
		}

		public void UpdateSelection(Vector3 position)
		{
			if (position == Vector3.zero)
			{
				outline.SetActive(false);
				return;
			}

			outline.SetActive(true);


			outline.transform.localPosition = position;
		}
	}
}
