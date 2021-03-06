﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace com.jds.AWLauncher.classes.invoke
{
    public class InvokeManager
    {
        private volatile /*readonly*/ Queue<DelegateCall> _calls = new Queue<DelegateCall>(); 
        
        private readonly Thread _mainThread;
        private const int INTERVAL = 10;

        private volatile static InvokeManager _instance;

        public static InvokeManager Instance
        {
           get 
           {
               return _instance ?? (_instance = new InvokeManager()); 
           }
        }
              
        private InvokeManager()
        {
            ThreadStart d = delegate
            {
                while (!Shutdown)
                {
                    try
                    {
                      //  if (Free)
                        {
                            var delegateCall = Next;

                            if (delegateCall != null)
                            {
                                if (!delegateCall.Invoke())
                                {
                                    AddInvoke(delegateCall);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    
                    }   
                    finally
                    {
                        Thread.Sleep(INTERVAL);
                    }
                }
            }; 
            
            _mainThread = new Thread(d) {Name = "AWLauncher - Invoke Manager ", Priority = ThreadPriority.Highest};
            _mainThread.Start();
        }

       
        public void AddInvoke(DelegateCall c)
        {
            if(!c.Invoke())
            {
                _calls.Enqueue(c);   
            }
        }

        public DelegateCall Next
        {
            get { return _calls.Count <= 0 ? null : _calls.Dequeue(); }
        }

        public bool Shutdown
        {
            get; set;
        }
    }
}
