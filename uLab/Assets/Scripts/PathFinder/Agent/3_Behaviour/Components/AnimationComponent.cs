using UnityEngine;
using System.Collections;


namespace Lite
{

	public class AnimationComponent : IComponent
	{
		private Animator animator;

		private string currentAnimation = "";

		public BehaviourAgent agent;

		//private Anim.Fsm mAnimFSM;

		//private Anim.AnimSet _animSet;
		//public Anim.AnimSet animSet { get { return _animSet; } }

		public void Init(BehaviourAgent agent)
		{
			this.agent = agent;
			//mAnimFSM = FsmFactory.GetFsm(FsmType.SimpleFsm);
			//_animSet = AnimSetFactory.GetAnimSet(1);
		}

		public override void OnStart()
		{
			animator = GetComponentInChildren<Animator>();
			//_animSet.Init(animator);
		}

		public override void OnUpdate()
		{
			//if (mAnimFSM != null)
			//	mAnimFSM.Update(agent);
		}

		/*public void Play(string name)
		{
			if (animator.IsPlaying(name))
			{
				//Log.Info("queued");
				animator.Stop();
				animator.PlayQueued(name);
			}
			else
			{
				//Log.Info("fade");
				animator.CrossFade(name);
			}
			currentAnimation = name;
		}*/

		/*public void HandleAction(Bev.Action action)
		{
			mAnimFSM.DoAction(agent, action);
		}*/

		/*public float GetAnimLenth(string name)
		{
			return 0;// animator[name].length;
		}*/

		/*public bool IsPlaying(string name)
		{
			return currentAnimation == name;
		}*/

		public void Set_MoveSpeed(float speed)
		{
			animator.SetFloat("moveSpeed", speed);
		}

	}

}
