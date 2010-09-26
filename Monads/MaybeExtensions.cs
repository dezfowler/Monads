using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monads
{
	public static class MaybeExtensions
	{
		public static Maybe<T> Maybe<T>(this T t) where T : class
		{
			return t;
		}
	}
}
