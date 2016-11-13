
using UnityEngine;
using System.Collections;
using Lite;


public class KeyboardControl : MonoBehaviour
{
	//private Agent mAgent = null;
	private float lastMultiTouchDistance = 0;
	private bool isMultiTouching = false;

	void Start()
	{
		//mAgent = GetComponent<Agent>();
	}

	void Update()
	{
		KeyboardMouse();
	}

	void KeyboardMouse()
	{
		/*if (null == mAgent)
			return;*/

		try
		{
			if (true)
			{
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
				if(!isMultiTouching && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
				{ 
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#else
				if (Input.GetMouseButtonUp(0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
					RaycastHit hit;
					int layerMask = 
						1 << LayerMask.NameToLayer(AppDefine.LayerTerrain)
						| 1 << LayerMask.NameToLayer(AppDefine.LayerNPC)
						| 1 << LayerMask.NameToLayer(AppDefine.LayerItem);

					if (Physics.Raycast(ray, out hit, 50, layerMask))
					{
						Vector3 hitPoint = hit.point;

						if (LayerMask.NameToLayer(AppDefine.LayerTerrain) == hit.collider.gameObject.layer)
						{
							//AgentActionGoTo act = AgentActionFactory.GetAction<AgentActionGoTo>();
							//act.mTargetPosition = hitPoint;
							//mAgent.PushAction(act);

							ShowClickEffect(hitPoint);
						}
						else if (LayerMask.NameToLayer(AppDefine.LayerNPC) == hit.collider.gameObject.layer)
						{
							/*Agent npcAgent = hit.collider.GetComponent<Agent>();
							if (npcAgent.blackboard.agentType == AgentType.Monster)
							{
								AgentActionAttack act = AgentActionFactory.GetAction<AgentActionAttack>();
								act.mTargetAgent = npcAgent;
								mAgent.PushAction(act);
							}*/
						}
						else if (LayerMask.NameToLayer(AppDefine.LayerItem) == hit.collider.gameObject.layer)
						{
							/*Item item = hit.collider.gameObject.GetComponent<Item>();
							if (item != null)
								item.OnClick(mAgent);*/
						}
					}
				}


				// mouse scroll
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
				if (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
				{
					isMultiTouching = true;
					float distance = 0;
					var touch1 = Input.GetTouch(0);
					var touch2 = Input.GetTouch(1);
					float curDist = Vector2.Distance(touch1.position, touch2.position);
					if (curDist > lastMultiTouchDistance)
					{
						distance += Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition) * 0.05f;
					}
					else
					{
						distance -= Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition) * 0.05f;
					}
					lastMultiTouchDistance = curDist;
					MainCameraControl.Instance.Scroll(distance);
				}
				else if (isMultiTouching && Input.touchCount == 0)
				{
					isMultiTouching = false;
				}
#else
				float scrollValue = Input.GetAxis("Mouse ScrollWheel");
				MainCameraControl.Instance.Scroll(scrollValue);
#endif
			}

			// mouse rotate
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR

#else
			if (Input.GetMouseButton(1))
			{
				float rotY = Input.GetAxis("Mouse X");
				float rotX = Input.GetAxis("Mouse Y");
				MainCameraControl.Instance.Rotate(-rotX, rotY);
			}
#endif
			// keyboard
			int x = 0;
			int z = 0;
			z += Input.GetKey(KeyCode.W) ? 1 : 0;
			z -= Input.GetKey(KeyCode.S) ? 1 : 0;
			x -= Input.GetKey(KeyCode.A) ? 1 : 0;
			x += Input.GetKey(KeyCode.D) ? 1 : 0;

			if (x != 0 || z != 0)
			{
				/*Vector3 dir = MainCameraControl.Instance.transform.rotation * (new Vector3(x, 0, z).normalized);
				AgentActionMoveTowards actMT = AgentActionFactory.GetAction<AgentActionMoveTowards>();
				actMT.mDirection = dir;
				mAgent.PushAction(actMT);*/
			}

			// skill
			if (Input.GetKey(KeyCode.Space))
			{
				//AgentActionJumpTo act = AgentActionFactory.GetAction<AgentActionJumpTo>();
				//mAgent.PushAction(act);
			}
			if (Input.GetKeyDown(KeyCode.J))
			{
				/*AgentActionAttack act = AgentActionFactory.GetAction<AgentActionAttack>();
				act.mAttackDir = transform.forward;
				mAgent.PushAction(act);*/
			}
			if (Input.GetKeyDown(KeyCode.K))
			{

			}
			if (Input.GetKeyDown(KeyCode.L))
			{

			}
		}
		catch (System.Exception e)
		{
			Log.Error(e.ToString());
		}
	}


	private void ShowClickEffect(Vector3 pos)
	{
		/*GameObject effectRoot = GameObject.Find(AppDefine.LayerEffect);
		ResourceLoader.CreateGameObjectAsync(AppDefine.EfxClickGreen, effectRoot.transform,
			(obj)=>
				{
					GameObject go = obj as GameObject;
					go.transform.position = pos;
				});*/
	}

}
