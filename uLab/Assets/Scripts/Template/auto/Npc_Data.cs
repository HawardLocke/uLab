using System;
using System.Collections.Generic;


namespace Locke
{
	public class Npc_Data : IData
	{
		int _hp;	//hp
		public int hp { get { return _hp;} }

		float _speed;	//speed
		public float speed { get { return _speed;} }

		int _attack;	//attack
		public int attack { get { return _attack;} }

		float _sightDist;	//sightDist
		public float sightDist { get { return _sightDist;} }

		int _sightAngle;	//sightAngle
		public int sightAngle { get { return _sightAngle;} }

		float _attackDist;	//attackDist
		public float attackDist { get { return _attackDist;} }

		float _attackInterval;	//attackInterval
		public float attackInterval { get { return _attackInterval;} }

		int[] _skills = new int[10];	//skills
		public int[] skills { get { return _skills;} }

		int _bevTree;	//bevTree
		public int bevTree { get { return _bevTree;} }

		int _model;	//model
		public int model { get { return _model;} }

		public override int init(TabReader reader, int row, int column)
		{
			column = base.init(reader, row, column);

			_hp = 0;
			int.TryParse(reader.At(row, column), out _hp);
			column++;

			_speed = 1;
			float.TryParse(reader.At(row, column), out _speed);
			column++;

			_attack = 0;
			int.TryParse(reader.At(row, column), out _attack);
			column++;

			_sightDist = 0;
			float.TryParse(reader.At(row, column), out _sightDist);
			column++;

			_sightAngle = 0;
			int.TryParse(reader.At(row, column), out _sightAngle);
			column++;

			_attackDist = 0;
			float.TryParse(reader.At(row, column), out _attackDist);
			column++;

			_attackInterval = 1;
			float.TryParse(reader.At(row, column), out _attackInterval);
			column++;

			for(int i=0; i<10; i++)
				_skills[i] = 0;
			string[] skillsArray = reader.At(row, column).Split(',');
			int skillsCount = skillsArray.Length;
			for(int i=0; i<10; i++)
			{
				if(i < skillsCount)
					int.TryParse(skillsArray[i], out _skills[i]);
				else
					_skills[i] = 0;
			}
			column++;

			_bevTree = 0;
			int.TryParse(reader.At(row, column), out _bevTree);
			column++;

			_model = 0;
			int.TryParse(reader.At(row, column), out _model);
			column++;

			return column;
		}
	}
}