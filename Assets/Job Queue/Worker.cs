using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class Worker : MonoBehaviour {
	public bool isWorking = false;
	public Action<Worker> onWorkerFinished;

	public Worker(Action<Worker> onWorkerFinished) {
		this.onWorkerFinished = onWorkerFinished;
	}

	public void PerformJob(Job job) {
		isWorking = true;
		Thread thread = new Thread(() => PerformJobAsync(job));
		thread.Start ();
	}

	public void OnJobBailed(Exception exception) {
		JobQueue.QueueException (exception);
		this.isWorking = false;
		onWorkerFinished (this);
	}

	public void OnJobComplete(Job job) {
		JobQueue.QueueJobForFinishing (job);
		this.isWorking = false;
		onWorkerFinished (this);
	}

	private void PerformJobAsync(Job job) {
		try {
			job.work ();
			OnJobComplete (job);
		} catch(Exception e) {
			OnJobBailed(e);
		}
	}
}
