using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ch1
{

    #region thread intro
    /// A thread is an independent execution path, able to run simultaneously with other threads.
    /// C# Program starts in a single thread created automatically by the CLR and operating system (the “main” thread)
    #endregion

    #region thread state
    /// Once started, a thread’s IsAlive property returns true, until the point where the thread ends.
    /// A thread ends when the delegate passed to the Thread’s constructor finishes executing.
    /// Once ended, a thread cannot restart. 
    #endregion

    #region thread memory
    //The CLR assigns each thread its own "memory stack" so that local variables are kept separate.
    //public class TestClass
    //{
    //    static void Main()
    //    {
    //        new Thread(Go).Start();      // Call Go() on a new thread   
    //        Go();                         // Call Go() on the main thread 
    //        // ??????????
    //    }
    //    static void Go()
    //    {
    //        /// Declare and use a local variable - 'cycles'   
    //        /// local variables that declared inside thread are kept separate.
    //        for (int cycles = 0; cycles < 5; cycles++) Console.Write('?');
    //    }
    // }
    #endregion

    #region shared Memory
    /// 1- Threads share data if they have a common reference to the same object instance
    /// 2- Static fields are shared between all threads 
    /// 3- threads share (heap) memory with other threads running in the same application.
    //public class TestClass
    //{
    //    bool done;
    //    static bool staticDone;
    //    static void Main()
    //    {
    //        TestClass commonInstance = new TestClass();   // Create a common instance   
    //        new Thread(commonInstance.Go).Start();
    //        commonInstance.Go();

    //        Console.ReadKey();
    //    }

    //    void Go()
    //    {
    //        if (!done)
    //        {
    //            done = true;
    //            Console.WriteLine("Done");
    //        }
    //    }

    //    static void StaticGo()
    //    {
    //        if (!staticDone)
    //        {
    //            staticDone = true;
    //            Console.WriteLine("Done");
    //        }
    //    }
    //}
    /// The output is actually indeterminate for 2 cases, maybe "Done" once, or twice
    #endregion

    #region lock
    /// ensures only one thread can enter the critical section of code at a time,
    /// thread waits, or blocks, until the lock becomes available
    /// A thread, while blocked, doesn't consume CPU resources
    //class ThreadSafe
    //{
    //    static bool done;
    //    static readonly object locker = new object();
    //    static void Main()
    //    {
    //        new Thread(Go).Start(); Go();
    //    }

    //    static void Go()
    //    {
    //        lock (locker)
    //        {
    //            if (!done)
    //            {
    //                Console.WriteLine("Done");
    //                done = true;
    //            }
    //        }
    //    }
    //}
    #endregion

    #region JOIN, Sleep and Yield
    ///While waiting on a Sleep or Join, a thread is blocked and so does not consume CPU resources. 
    //class ThreadSafe
    //{
    //    static void Main()
    //    {
    //        Thread joinThread = new Thread(Go);
    //        joinThread.Start();
    //        //joinThread.Join(5);                       /// main thread will wait for joinThread to end
    //        Console.WriteLine("joinThread has ended!");

    //        //var finished = joinThread.Join(10);      /// main thread will wait for joinThread for (paramter Timeout) time
    //        //                                         /// It then returns true if the thread ended or false if it timed out
    //        //                                         /// if 10 sec finished, main thread will print "Thread joinThread has finished: false", 
    //        //                                         /// then joinThread will continue async with main thread 
    //        //Console.WriteLine($"joinThread has finished : {finished}!");

    //        GoYield();
    //    }

    //    static void Go()
    //    {
    //        for (int i = 0; i < 1000; i++)
    //            Console.Write($"{i}  ");
    //    }
    //    static void GoYield()
    //    {
    //        for (int i = 0; i < 1000; i++)
    //        {
    //            Thread.Yield();
    //            Console.Write($"Y");
    //        }
    //    }
    //}

    ///Thread.Sleep pauses the current thread for a specified period: 
    //Thread.Sleep (TimeSpan.FromHours (1));  // sleep for 1 hour 
    //Thread.Sleep (500);                     // sleep for 500 milliseconds
    /// Thread.Sleep(0) relinquishes the thread’s current time slice immediately, voluntarily handing over the CPU to other threads.
    /// Thread.Yield() method does the same thing "Thread.Sleep(0)" —except that it relinquishes only to threads running on the same processor.
    #endregion

    #region Passing Data to a Thread
    //This works because Thread’s constructor is overloaded to accept either of two delegates: 
    //  public delegate void ThreadStart();
    //  public delegate void ParameterizedThreadStart(object obj);
    //class ThreadSafe
    //{
    //    /// 1
    //    //static void Main()
    //    //{
    //    //    Thread t = new Thread(() => Print("Hello from t!"));
    //    //    t.Start();
    //    //}
    //    //static void Print(string message)
    //    //{
    //    //    Console.WriteLine(message);
    //    //}

    //    /// 2
    //    //static void Main()
    //    //{
    //    //    Thread t = new Thread(Print);
    //    //    t.Start("Hello from t!");
    //    //}
    //    //static void Print(object messageObj)
    //    //{
    //    //    string message = (string)messageObj;   // We need to cast here  
    //    //    Console.WriteLine(message);
    //    //}
    //}
    #endregion

    #region Foreground and Background Threads 
    ///By default, threads you create explicitly are foreground threads
    ///Foreground threads keep the application alive for as long as any one of them is running, whereas background threads do not
    ///A thread’s foreground/background status has no relation to its priority or allocation of execution time. 
    ///You can query or change a thread’s background status using its IsBackground property.
    //class PriorityTest
    //{
    //    static void Main(string[] args)
    //    {
    //        Thread worker = new Thread(() => Console.ReadLine());

    //        if (args.Length > 0)
    //            worker.IsBackground = true;

    //        worker.Start();
    //    }
    //    ///If this program is called with no arguments, 
    //    ///the worker thread assumes foreground status and will wait on the ReadLine statement for the user to press Enter. 
    //    ///Meanwhile, the main thread exits, but the application keeps running because a foreground thread is still alive.

    //   
    //}
    #endregion

    #region Thread Priority
    ///1- A thread’s Priority property determines how much execution time it gets relative to other active threads in the operating system
    ///2- enum ThreadPriority { Lowest, BelowNormal, Normal, AboveNormal, Highest }
    ///3- ThreadPriority => between threads in the same process, so it’s still throttled by the application’s process priority
    ///but you can elevate the process priority using the Process class in System.Diagnostics:
    ///     using (Process p = Process.GetCurrentProcess())
    ///     p.PriorityClass = ProcessPriorityClass.High;
    /// this will instructs the OS that you never want the process to yield CPU time to another process
    #endregion

    #region Exception Handling
    ///Any try/catch/finally blocks in scope when a thread is created are of no relevance to the thread when it starts executing.
    //public class MyClass
    //{
    //    /// 1-
    //    //public static void Main()
    //    //{
    //    //    try
    //    //    {
    //    //        new Thread(Go).Start();
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        // We'll never get here!
    //    //        Console.WriteLine("Exception!");
    //    //        ///This behavior makes sense when you consider that each thread has an independent execution path.
    //    //    }
    //    //}
    //    //static void Go() { throw null; } // Throws a NullReferenceException

    //    ///2-
    //    //public static void Main()
    //    //{
    //    //    new Thread(Go).Start();
    //    //}
    //    //static void Go()
    //    //{
    //    //    try
    //    //    {
    //    //        throw null; // The NullReferenceException will get caught below
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        // will print Exception!
    //    //        Console.WriteLine("Exception!");
    //    //    }
    //    //}

    //}

    #endregion

    #region Thread pool
    ///1- Whenever you start a thread, a few hundred microseconds are spent organizing such things as a fresh private local variable stack.
    ///Each thread also consumes (by default) around 1 MB of memory.
    ///The thread pool cuts these overheads by sharing and recycling threads
    ///2- Too many active threads throttle the operating system with administrative burden and render CPU caches ineffective.
    ///Once a limit is reached, jobs queue up and start only when another finishes.

    ///3- There are a number of ways to enter the thread pool:
    #region 1- Entering the Thread Pool via TPL
    ///You can enter the thread pool easily using the Task classes in the Task Parallel Library.
    ///the nongeneric Task class a replacement for ThreadPool.QueueUserWorkItem
    ///the generic Task<TResult> a replacement for asynchronous delegates.
    public class MyClass
    {
        static void Main() 
        {
            try
            {
                var task = Task.Factory.StartNew(Go);          ///To use the nongeneric Task class, call Task.Factory.StartNew. (background thread) 
                //Task.Factory.StartNew(unhandledException);   ///If you don’t call Wait and abandon the task,
                                                               ///an unhandled exception will shut down the thread
                
                var task2 = Task.Factory.StartNew(unhandledException);
                task2.Wait();                                   ///Any unhandled exceptions are conveniently "rethrown" onto the host thread (where this thread is created),
                                                                ///when you call a task's Wait method, and will go to catch without continue
                                                                
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"main no = {i}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Go()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine($"Hello from the thread pool!, no = {i}");
            }
        }
        static void unhandledException()
        {
            Console.WriteLine($"unhandledException, start");
            int y = 0;
            int z = 5 / y;
            Console.WriteLine($"unhandledException, end");
        }
    }

    #endregion

    /// 4- use the thread pool indirectly:
    ///         System.Timers.Timer and System.Threading.Timer
    ///         Framework methods that end in Async, and most BeginXXX methods
    ///         PLINQ
    ///         
    ///5- There are a few things to be wary of when using pooled threads:
    /// You cannot set the Name of a pooled thread.
    /// Pooled threads are always background threads (this is usually not a problem).
    /// Blocking a pooled thread may trigger additional latency in the early life of an application unless you call ThreadPool.SetMinThreads

    ///change the priority of a pooled thread—it will be restored to normal when released back to the pool.

    #endregion

}
