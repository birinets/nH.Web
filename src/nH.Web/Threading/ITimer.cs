using System;

namespace nH.Web.Threading
{
	public interface ITimer : IDisposable
	{
		event EventHandler Tick;
		void Start();
		void Stop();
	}
}
