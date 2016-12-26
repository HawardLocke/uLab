
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lite
{
	public class GodCamera : MonoBehaviour
	{
		public static GodCamera Instance { get; set; }

		// camera positon
		private float camPosX = 0;
		private float camPosZ = 0;
		private float camPosY = 30;
		private const float maxX = 32;
		private const float minX = -32;
		private const float maxY = 35;
		private const float minY = 10;
		private const float maxZ = 20;
		private const float minZ = -20;
		
		private float ZoomSpeed = 30;
		private float DragSpeed = 30;

		public float dampingY = 10;
		public float dampingXZ = 20;

		void Awake()
		{
			Instance = this;
		}

		void Start()
		{
			transform.position = new Vector3(camPosX, camPosY, camPosZ);
		}

		void Update()
		{
			ProcessMouse();

			ProcessKeyboard();
		}

		void LateUpdate()
		{
			try
			{
				AdjustCamera();
			}
			catch (UnityException ue)
			{
				Log.Error(ue.ToString());
			}
		}

		public void ZoomInOut(float scrollValue)
		{
			camPosY -= scrollValue * ZoomSpeed;
			camPosY = Mathf.Clamp(camPosY, minY, maxY);
		}

		float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360)
				angle += 360;
			if (angle > 360)
				angle -= 360;
			return Mathf.Clamp(angle, min, max);
		}

		private void AdjustCamera()
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, camPosY, transform.position.z), Time.deltaTime * dampingY);

			transform.position = Vector3.Lerp(transform.position, new Vector3(camPosX, transform.position.y, camPosZ), Time.deltaTime * dampingXZ);
		}

		bool isMiddleButtonDown = false;
		Vector2 mouseLastPosition;
		Vector2 mousePositionOffset;
		void ProcessMouse()
		{
			ZoomInOut(Input.GetAxis("Mouse ScrollWheel"));

			/*if (Input.GetMouseButtonUp(0))
			{
			}
			if (Input.GetMouseButton(1))
			{
				float rotY = Input.GetAxis("Mouse X");
				float rotX = Input.GetAxis("Mouse Y");
			}*/
			if (Input.GetMouseButtonDown(2))
			{
				if (!isMiddleButtonDown)
				{
					isMiddleButtonDown = true;
					mouseLastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				}
			}
			if (Input.GetMouseButtonUp(2))
			{
				isMiddleButtonDown = false;
			}

			if (isMiddleButtonDown)
			{
				mousePositionOffset = new Vector2(Input.mousePosition.x - mouseLastPosition.x, Input.mousePosition.y - mouseLastPosition.y);
				mouseLastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				camPosX -= mousePositionOffset.x / Screen.width * DragSpeed;
				camPosZ -= mousePositionOffset.y / Screen.height * DragSpeed;
				camPosX = Mathf.Clamp(camPosX, minX, maxX);
				camPosZ = Mathf.Clamp(camPosZ, minZ, maxZ);
			}

		}

		void ProcessKeyboard()
		{
			if (Input.GetKey(KeyCode.Space))
			{
			}
		}

	}

}