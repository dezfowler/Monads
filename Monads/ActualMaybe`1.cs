using System;
using System.Collections.Generic;

namespace Monads
{
	class ActualMaybe<T> : Maybe<T> where T : class
	{
		readonly T _t;
		public ActualMaybe(T t)
		{
			if (t == null) throw new ArgumentNullException("t");
			_t = t;
		}

		public override Maybe<TResult> Apply<TResult>(Func<T, TResult> func)
		{
			return func(_t);
		}

		public override Maybe<TResult> Cast<TResult>()
		{
			return _t as TResult;
		}

		public override Maybe<T> Do(Action<T> action)
		{
			action(_t);
			return this;
		}

		public override Maybe<T> Do(Func<T, bool> func)
		{
			return func(_t) ? this : Nothing;
		}

		public override Maybe<T> If(Predicate<T> predicate)
		{
			return predicate(_t) ? this : Nothing;
		}

		public override T Return()
		{
			return _t;
		}

		public override T Return(T def)
		{
			return _t;
		}		

		public override TResult Return<TResult>(Func<T, TResult> func)
		{
			return func(_t);
		}

		public override TResult Return<TResult>(Func<T, TResult> func, TResult def)
		{
			return func(_t);
		}

		public override IEnumerable<T> AsEnumerable()
		{
			yield return _t;
		}
	}
}
