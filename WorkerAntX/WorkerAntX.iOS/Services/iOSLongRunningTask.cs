﻿using Foundation;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using UIKit;
using WorkerAntX.Messages;

namespace WorkerAntX.iOS.Services
{
    class iOSLongRunningTask
    {
		nint _taskId;
		CancellationTokenSource _cts;

		public async Task Start()
		{
			_cts = new CancellationTokenSource();

			_taskId = UIApplication.SharedApplication.BeginBackgroundTask("LongRunningTask", OnExpiration);

			try
			{
				//INVOKE THE SHARED CODE
				var counter = new TaskCounter();
				await counter.RunCounter(_cts.Token);

			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (_cts.IsCancellationRequested)
				{
					var message = new CancelledMe();
					Device.BeginInvokeOnMainThread(
						() => MessagingCenter.Send(message, "CancelledMessage")
					);
				}
			}

			UIApplication.SharedApplication.EndBackgroundTask(_taskId);
		}

		public void Stop()
		{
			_cts.Cancel();
		}

		void OnExpiration()
		{
			_cts.Cancel();
		}
	}
}