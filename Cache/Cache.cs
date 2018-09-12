using System;

namespace Cache
{
	public class Cache<T>
	{
		private Item<T> _head;
		private Item<T> _tail;

		private readonly IStore<T> _store;

		private readonly int _n;

		private int _itemCount;

		public Cache(int n, IStore<T> store)
		{
			_n = n;
			_store = store;
		}

		public T Get(Func<T, bool> filter)
		{
			var findResult = _head?.FindNext(filter);

			if (findResult != null)
			{
				findResult.Remove();

				_head.AddHead(findResult.Value);

				return findResult.Value;
			}

			var value = _store.Get(filter);

			if (_itemCount < _n)
			{
				_itemCount++;

				if (_itemCount == 1)
				{
					_head = new Item<T>(value, null, null);

					_tail = _head;
				} else
				{
					_head = _head.AddHead(value);
				}

				return value;
			}
			else
			{
				_head = _head.AddHead(value);

				var tmp = _tail.Prev;

				_tail.Remove();

				_tail = tmp;

				_tail.Next = null;
			}

			return value;
		}
	}
}