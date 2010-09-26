using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Monads;

namespace Monads.Test
{
	[TestFixture]
	public class Maybe
	{
		#region Maybe
		[Test]
		public void NullYieldsNull()
		{
			// Arrange
			string obj = null;

			// Act
			var maybe = obj.Maybe();

			// Assert
			string result = maybe;
			Assert.IsNotNull(maybe);
			Assert.IsNull(result);
		}

		[Test]
		public void NotNullYieldsOriginal()
		{
			// Arrange
			string obj = "test";

			// Act
			var maybe = obj.Maybe();

			// Assert
			string result = maybe;
			Assert.IsNotNull(maybe);
			Assert.IsNotNull(result);
			Assert.AreSame(obj, result);
		}
		#endregion

		#region Apply
		[Test]
		public void DeepRetrieveFromObjectGraph()
		{
			// Arrange
			Node node = new Node
			{
				Number = 1,
				Parent = new Node
				{
					Number = 2,
					Parent = new Node
					{
						Number = 3
					}
				}
			};

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.Apply(n => n.Parent)
				.Return();
			
			// Assert
			Assert.IsNotNull(third);
			Assert.AreEqual(3, third.Number);
		}

		[Test]
		public void DeepRetrieveFromNullGraph()
		{
			// Arrange
			Node node = null;

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.Apply(n => n.Parent)
				.Return();

			// Assert
			Assert.IsNull(third);
		}

		[Test]
		public void DeepRetrieveFromPartialObjectGraph()
		{
			// Arrange
			Node node = new Node
			{
				Number = 1,
				Parent = new Node
				{
					Number = 2,
				}
			};

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.Apply(n => n.Parent)
				.Return();

			// Assert
			Assert.IsNull(third);
		}
		#endregion

		#region Return
		[Test]
		public void DeepRetrieveValueFromObjectGraph()
		{
			// Arrange
			Node node = new Node
			{
				Number = 1,
				Parent = new Node
				{
					Number = 2,
					Parent = new Node
					{
						Number = 3
					}
				}
			};

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.Apply(n => n.Parent)
				.Return(n => n.Number);

			// Assert
			Assert.AreEqual(3, third);
		}

		[Test]
		public void DeepRetrieveValueFromNullGraph()
		{
			// Arrange
			Node node = null;

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.Apply(n => n.Parent)
				.Return(n => n.Number);

			// Assert
			Assert.AreEqual(0, third);
		}
		#endregion

		#region Cast
		[Test]
		public void FailingCast()
		{
			// Arrange
			Node node = new Node
			{
				Number = 1,
				Parent = new Node
				{
					Number = 2,
					Parent = new Node
					{
						Number = 3
					}
				}
			};

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.Cast<Blah>()
				.Apply(n => n.Parent)
				.Return(n => n.Number);

			// Assert
			Assert.AreEqual(0, third);
		}
		#endregion

		#region If
		[Test]
		public void FailingIf()
		{
			// Arrange
			Node node = new Node
			{
				Number = 1,
				Parent = new Node
				{
					Number = 2,
					Parent = new Node
					{
						Number = 3
					}
				}
			};

			// Act
			var third = node.Maybe()
				.Apply(n => n.Parent)
				.If(n => n.Number == 1)
				.Apply(n => n.Parent)
				.Return(n => n.Number);

			// Assert
			Assert.AreEqual(0, third);
		}
		#endregion

		#region Do
		#endregion

		#region AsEnumerable
		[Test]
		public void AsEnumerableOverNull()
		{
			// Arrange
			string obj = null;

			// Act
			var enumerable = obj.Maybe()
				.AsEnumerable();
			
			// Assert
			Assert.IsFalse(enumerable.Any());
		}

		[Test]
		public void AsEnumerableOverNotNull()
		{
			// Arrange
			string obj = "test";

			// Act
			var enumerable = obj.Maybe()
				.AsEnumerable();

			// Assert
			Assert.AreEqual(1, enumerable.Count());
		}
		#endregion
	}

	class Node
	{
		public int Number { get; set; }
		public Node Parent { get; set; }
	}

	class Blah : Node { }
}
