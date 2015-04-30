﻿//
// Copyright 2014-2015 Amazon.com, Inc. or its affiliates. All Rights Reserved.
//
//
// Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
// You may not use this file except in compliance with the License.
// A copy of the License is located in the "license" file accompanying this file.
// See the License for the specific language governing permissions and limitations under the License.
//
//

using System;
using Amazon.Runtime.Internal.Util;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// This handler executes the subsequent handlers on a ThreadPool thread.
    /// This feature is only implemented for APM based async methods.
    /// </summary>
    public class ThreadPoolExecutionHandler : PipelineHandler
    {
        private static ThreadPoolThrottler<IAsyncExecutionContext> _throttler;
        private static object _lock = new object();

        public ThreadPoolExecutionHandler(int concurrentRequests)
        {
            lock (_lock)
            {
                if (_throttler == null)
                    _throttler = new ThreadPoolThrottler<IAsyncExecutionContext>(concurrentRequests);
            }
        }

#if AWS_APM_API
        public override IAsyncResult InvokeAsync(IAsyncExecutionContext executionContext)
        {
            if (UnityInitializer.IsMainThread())
            {
                _throttler.Enqueue(executionContext, InvokeAsyncHelper, ErrorCallback);
                return null;
            }
            else
            {
                return base.InvokeAsync(executionContext);
            }
        }
#endif
        void InvokeAsyncHelper(IAsyncExecutionContext executionContext)
        {
            base.InvokeAsync(executionContext);
        }

        void ErrorCallback(Exception exception, IAsyncExecutionContext executionContext)
        {
            // Handle the exception by logging it and setting the exception on the context,
            // so that the exception is visible to the caller
            executionContext.ResponseContext.AsyncResult.Exception = exception;
            this.Logger.Error(exception,
                "An exception was thrown from the runtime pipeline invoked by the thread pool.");
        }
    }



}
