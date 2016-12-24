using UnityEngine;
using System.Collections;

namespace Lite
{

	public class AnimationComponent : IComponent
	{
		public bool displayTrack;

		public float damping = 0.9f;

		private Animation animation;

		private string currentAnimation = "";

		public override void OnStart()
		{
			animation = GetComponent<Animation>();
			animation.playAutomatically = false;
			Play("idle break");
		}

		public override void OnUpdate()
		{
			animation.PlayQueued("idle break");
		}

		public void Play(string name, bool forceReplay = false)
		{
			if (animation.IsPlaying(name))
			{
				if (forceReplay)
				{
					animation.Stop();
				}
				animation.PlayQueued(name);
			}
			else
			{
				animation.CrossFade("", 0.9f);
				animation.Stop();
				animation.PlayQueued("idle break");
			}
		}

	}

}
