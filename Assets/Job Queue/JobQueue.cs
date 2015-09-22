using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class JobQueue : Singleton<JobQueue> {
	protected Queue<Job> jobQueue = new Queue<Job>();
	protected Queue<Job> finishedJobs = new Queue<Job> ();
	protected Queue<Exception> exceptionQueue = new Queue<Exception> ();
	protected Queue<Worker> availableWorkers = new Queue<Worker>();
	protected List<Worker> workers = new List<Worker>();

	public void Awake() {
		mainThread = Thread.CurrentThread;
		for(int i = 0; i < Environment.ProcessorCount; i++) {
			Worker worker = new Worker(OnWorkerFinished);
			workers.Add(worker);
			availableWorkers.Enqueue(worker);
		}
	}


	public void Update() {
		while (exceptionQueue.Count > 0) {
			throw exceptionQueue.Dequeue();
		}

		if(jobQueue.Count > 0 && availableWorkers.Count > 0) {
			Worker worker = availableWorkers.Dequeue();
			worker.PerformJob(jobQueue.Dequeue());
		}

		while (finishedJobs.Count > 0) {
			finishedJobs.Dequeue().finish();
		}
	}

	public void OnGUI() {
		String debugString = "";
		debugString += "Number of workers: " + workers.Count + "\n";
		debugString += "Number of active workers: " + (workers.Count - availableWorkers.Count) + "\n";
		debugString += "Queued Jobs: " + jobQueue.Count + "\n";

		GUI.Label (new Rect (10, 10, 1000, 1000), debugString);
	}

	public Thread mainThread;

	public void OnWorkerFinished(Worker worker) {
		availableWorkers.Enqueue (worker);
	}
	
	public static void QueueJob(Job job) {
		Instance.jobQueue.Enqueue (job);
	}

	public static void QueueJobForFinishing(Job job) {
		Instance.finishedJobs.Enqueue (job);
	}

	public static void QueueException(Exception exception) {
		Instance.exceptionQueue.Enqueue (exception);
	}
}