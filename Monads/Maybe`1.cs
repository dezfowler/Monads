using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
	public abstract class Maybe<T> where T : class
	{
		public static readonly Maybe<T> Nothing = new NothingMaybe<T>();
		
		public static implicit operator Maybe<T>(T t)
		{
			return t == null ? Maybe<T>.Nothing : new ActualMaybe<T>(t);
		}

		public static implicit operator T(Maybe<T> t)
		{
			return t.Return();
		}

		public abstract Maybe<TResult> Apply<TResult>(Func<T, TResult> func) where TResult : class;

		public abstract Maybe<TResult> Cast<TResult>() where TResult : class;

		public abstract Maybe<T> Do(Action<T> action);
		public abstract Maybe<T> Do(Func<T, bool> func);

		public abstract Maybe<T> If(Predicate<T> predicate);
		
		public abstract T Return();
		public abstract T Return(T def);
		public abstract TResult Return<TResult>(Func<T, TResult> func);
		public abstract TResult Return<TResult>(Func<T, TResult> func, TResult def);

		public abstract IEnumerable<T> AsEnumerable();
	}

}