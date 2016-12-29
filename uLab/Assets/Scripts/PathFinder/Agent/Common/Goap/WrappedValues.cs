



namespace Lite.Common
{
	
	public abstract class AtomValue
	{
		public abstract bool Equals(AtomValue other);
		public virtual int ToInt() { return 0; }
		public virtual void SetInt(int value) { }
		public virtual long ToLong() { return 0; }
		public virtual void SetLong(long value) { }
		public virtual float ToFloat() { return 0; }
		public virtual void SetFloat(float value) { }
		public virtual bool ToBool() { return false; }
		public virtual void SetBool(bool value) { }
		public virtual string ToString() { return string.Empty; }
		public virtual void SetString(string value) { }
	}

	public class IntValue : AtomValue
	{
		int value;
		public override int ToInt()
		{
			return value;
		}
		public override void SetInt(int value)
		{
			this.value = value;
		}
		public override bool Equals(AtomValue other)
		{
			IntValue o = (IntValue)other;
			if (o != null)
				return this.value == o.value;
			return false;
		}
	}
	public class LongValue : AtomValue
	{
		long value;
		public override long ToLong()
		{
			return value;
		}
		public override void SetLong(long value)
		{
			this.value = value;
		}
		public override bool Equals(AtomValue other)
		{
			LongValue o = (LongValue)other;
			if (o != null)
				return this.value == o.value;
			return false;
		}
	}
	public class FloatValue : AtomValue
	{
		float value;
		public override float ToFloat()
		{
			return value;
		}
		public override void SetFloat(float value)
		{
			this.value = value;
		}
		public override bool Equals(AtomValue other)
		{
			FloatValue o = (FloatValue)other;
			if (o != null)
				return this.value == o.value;
			return false;
		}
	}
	public class BoolValue : AtomValue
	{
		bool value;
		public override bool ToBool()
		{
			return value;
		}
		public override void SetBool(bool value)
		{
			this.value = value;
		}
		public override bool Equals(AtomValue other)
		{
			BoolValue o = (BoolValue)other;
			if (o != null)
				return this.value == o.value;
			return false;
		}
	}
	public class StringValue : AtomValue
	{
		string value;
		public override string ToString()
		{
			return value;
		}
		public override void SetString(string value)
		{
			this.value = value;
		}
		public override bool Equals(AtomValue other)
		{
			StringValue o = (StringValue)other;
			if (o != null)
				return this.value == o.value;
			return false;
		}
	}

}