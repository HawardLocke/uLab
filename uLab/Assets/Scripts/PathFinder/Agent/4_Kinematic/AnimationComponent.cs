using UnityEngine;
using System.Collections;

using Lite.Anim;


namespace Lite
{

	public class AnimationComponent : IComponent
	{
		private Animation animation;

		private string currentAnimation = "";

		public KinematicAgent agent;

		private Anim.Fsm mAnimFSM;

		private Anim.AnimSet _animSet;
		public Anim.AnimSet animSet { get { return _animSet; } }

		public void Init(KinematicAgent agent)
		{
			this.agent = agent;
			mAnimFSM = FsmFactory.GetFsm(FsmType.SimpleFsm);
			_animSet = AnimSetFactory.GetAnimSet(1);
		}

		public override void OnStart()
		{
			animation = GetComponent<Animation>();
			animation.playAutomatically = false;
		}

		public override void OnUpdate()
		{
			if (mAnimFSM != null)
				mAnimFSM.Update(agent);
		}

		public void Play(string name, bool forceReplay = false)
		{
			if (animation.IsPlaying(name))
			{
				if (forceReplay)
				{
					animation.Stop();
				}
				animation.CrossFade(name);
			}
			else
			{
				animation.CrossFade(name);			
			}
			currentAnimation = name;
		}

		public void HandleAction(Bev.Action action)
		{
			mAnimFSM.DoAction(agent, action);
		}

		public bool IsCurrentFinished()
		{
			return animation.isPlaying && animation[currentAnimation].normalizedTime > 0.95f;
		}

	}

}
