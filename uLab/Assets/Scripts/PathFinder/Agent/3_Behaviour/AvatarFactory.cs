
using UnityEngine;

using Lite.Bev;


namespace Lite
{

	public class AvatarFactory : Singleton<AvatarFactory>
	{
		string[] botFilePath = { "Prefabs/Dwarf/dwarf_01"/*, "Prefabs/Dwarf/dwarf_02", "Prefabs/Dwarf/dwarf_03"*/ };

		public Agent CreateAgent(int id, float x, float y, float z)
		{
			var agent = new Agent(GuidGenerator.NextLong());
			AppFacade.Instance.bevAgentManager.AddAgent(agent);

			var go = new GameObject("dwarf");

			var prefab = Resources.Load(botFilePath[id-1]);
			var goAvatar = GameObject.Instantiate(prefab) as GameObject;
			goAvatar.transform.parent = go.transform;
			goAvatar.transform.position = Vector3.zero;
			goAvatar.transform.localScale = Vector3.one;

			var agentCom = go.AddComponent<AgentComponent>();
			agentCom.agent = agent;
			agent.agentComponent = agentCom;
			var loco = go.AddComponent<LocomotionComponent>();
			agent.locomotion = loco;
			var animCom = go.AddComponent<AnimationComponent>();
			animCom.Init(agent);
			agent.animComponent = animCom;

			goAvatar.layer = LayerMask.NameToLayer(AppDefine.LayerBot);

			loco.SetPosition(new Vector3(x, y, z));

			return agent;
		}

	}

}