using UnityEngine;
using System.Collections;


namespace Lite
{

	public class AnimationComponent : IComponent
	{
		private Animator animator;

		private string currentAnimation = "";

		public BehaviourAgent agent;

		public void Init(BehaviourAgent agent)
		{
			this.agent = agent;
		}

		public override void OnStart()
		{
			animator = GetComponentInChildren<Animator>();
		}

		public override void OnUpdate()
		{
		}

		public float moveSpeed
		{
			set { animator.SetFloat("moveSpeed", value); }
			get { return animator.GetFloat("moveSpeed"); }
		}

		public bool attack1
		{
			set { animator.SetBool("attack1", value); }
			get { return animator.GetBool("attack1"); }
		}

	}

}
