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
			_animSet.Init(animation);
		}

		public override void OnUpdate()
		{
			if (mAnimFSM != null)
				mAnimFSM.Update(agent);
		}

		public void Play(string name)
		{
			if (animation.IsPlaying(name))
			{
				animation.PlayQueued(name);
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

		public float GetAnimLenth(string name)
		{
			return animation[name].length;
		}

	}

}
