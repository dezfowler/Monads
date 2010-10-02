using System;
using System.Collections.Generic;

namespace Monads
{

	public abstract class Collected<T> where T : class
	{
		public static readonly Collected<T> Nothing = new NothingCollected<T>();
		public abstract Collected<T> Collect(Func<T, T> collector);
		public abstract Collected<T> End();
		public abstract IEnumerable<T> Return();
	}

	class ActualCollected<T> : Collected<T> where T : class
	{
		ActualCollected<T> last;
		T t;
		ActualCollected<T> scope;

		public override Collected<T> Collect(Func<T, T> collector)
		{
			throw new NotImplementedException();
		}

		public override Collected<T> End()
		{
			scope = scope.last;
			return this;
		}

		public override IEnumerable<T> Return()
		{
			if (last != null)
			{
				foreach (var thing in last.Return())
				{
					yield return thing;
				}
			}
			yield return t;
		}
	}

	class NothingCollected<T> : Collected<T> where T : class
	{
		public override Collected<T> Collect(Func<T, T> collector)
		{
			return this;
		}

		public override Collected<T> End()
		{
			return this;
		}

		public override IEnumerable<T> Return()
		{
			yield break;
		}
	}
}
