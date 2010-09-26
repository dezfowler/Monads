using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
	class NothingMaybe<T> : Maybe<T> where T : class
	{
		public override Maybe<TResult> Apply<TResult>(Func<T, TResult> func)
		{
			return Maybe<TResult>.Nothing;
		}

		public override Maybe<TResult> Cast<TResult>()
		{
			return Maybe<TResult>.Nothing;
		}
		
		public override Maybe<T> Do(Action<T> action)
		{
			return this;
		}

		public override Maybe<T> Do(Func<T, bool> func)
		{
			return this;
		}

		public override Maybe<T> If(Predicate<T> predicate)
		{
			return this;
		}

		public override T Return()
		{
			return null;
		}

		public override T Return(T def)
		{
			return def;
		}

		public override TResult Return<TResult>(Func<T, TResult> func)
		{
			return default(TResult);
		}
		
		public override TResult Return<TResult>(Func<T, TResult> func, TResult def)
		{
			return def;
		}
		
		public override IEnumerable<T> AsEnumerable()
		{
			yield break;
		}
	}
}
