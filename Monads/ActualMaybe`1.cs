using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
	class ActualMaybe<T> : Maybe<T> where T : class
	{
		readonly T t;
		public ActualMaybe(T t)
		{
			if (t == null) throw new ArgumentNullException("t");
			this.t = t;
		}

		public override Maybe<TResult> Apply<TResult>(Func<T, TResult> func)
		{
			return func(t);
		}

		public override Maybe<TResult> Cast<TResult>()
		{
			return t as TResult;
		}

		public override Maybe<T> Do(Action<T> action)
		{
			action(t);
			return this;
		}

		public override Maybe<T> Do(Func<T, bool> func)
		{
			return func(t) ? this : Maybe<T>.Nothing;
		}

		public override Maybe<T> If(Predicate<T> predicate)
		{
			return predicate(t) ? this : Maybe<T>.Nothing;
		}

		public override T Return()
		{
			return t;
		}

		public override T Return(T def)
		{
			return t;
		}		

		public override TResult Return<TResult>(Func<T, TResult> func)
		{
			return func(t);
		}

		public override TResult Return<TResult>(Func<T, TResult> func, TResult def)
		{
			return func(t);
		}

		public override IEnumerable<T> AsEnumerable()
		{
			yield return t;
		}
	}
}
