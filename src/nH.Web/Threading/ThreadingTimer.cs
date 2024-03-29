﻿using System;
using System.Threading;

namespace nH.Web.Threading
{
	public class ThreadingTimer : ITimer
	{
		public event EventHandler Tick;

		private readonly int _interval;
		private readonly Timer _timer;
		private bool _disposed;

		public ThreadingTimer(int interval)
		{
			_interval = interval;
			_timer = new Timer(_ => OnTick(), null, Timeout.Infinite, Timeout.Infinite);
		}

		public void Start()
		{
			_timer.Change(0, GetInterval());
		}

		public void Stop()
		{
			_timer.Change(Timeout.Infinite, Timeout.Infinite);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				_timer.Change(Timeout.Infinite, Timeout.Infinite);
				_timer.Dispose();
			}

			_disposed = true;
		}

		private void OnTick()
		{
			_timer.Change(Timeout.Infinite, Timeout.Infinite);
			try
			{
				var thread = new Thread(() =>
				{
					if (Tick != null)
					{
						Tick(this, EventArgs.Empty);
					}
				});
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
			finally
			{
				var interval = GetInterval();
				_timer.Change(interval, interval);
			}
		}

		private int GetInterval()
		{
			return (int)TimeSpan.FromSeconds(_interval).TotalMilliseconds;
		}
	}
}
