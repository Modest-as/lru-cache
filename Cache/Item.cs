using System;

namespace Cache
{
	public class Item<T>
	{
		public T Value { get; }

		public Item<T> Next { get; set; }
		public Item<T> Prev { get; set; }

		public Item(T value, Item<T> next, Item<T> prev)
		{
			Value = value;
			Next = next;
			Prev = prev;
		}

		public Item<T> AddHead(T value)
		{
			if (Prev != null)
			{
				throw new ArgumentException("This is not the current head!");
			}

			var item = new Item<T>(value, this, null);

			Prev = item;

			return item;
		}

		public void Remove()
		{
			if (Next != null) Next.Prev = Prev;
			if (Prev != null) Prev.Next = Next;
		}

		public Item<T> FindNext(Func<T, bool> filter)
		{
			return filter(Value) ? this : Next?.FindNext(filter);
		}
	}
}