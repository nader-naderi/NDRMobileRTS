using System;

using UnityEngine;

namespace NDRIsometricRTS
{
	public class TileGO : MonoBehaviour
	{
		Action<Transform> onMouseDown;
		Action onMouseUp;

		public void Init(Action<Transform> onMouseDown, Action onMouseUp)
		{
			this.onMouseDown = onMouseDown;
			this.onMouseUp = onMouseUp;
		}

		private void OnMouseDown()
		{
			if (onMouseDown == null) return;
			onMouseDown(transform);
		}

		private void OnMouseUp()
		{
			if (onMouseUp == null) return;
			onMouseUp();
		}
	}
}
