using System;

namespace Cache
{
	public interface IStore<out T>
	{
		T Get(Func<T, bool> filter);
	}
}