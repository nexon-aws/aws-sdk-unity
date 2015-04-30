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

using Amazon.Runtime.Internal.Util;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace Amazon.Runtime.Internal
{
    /// <summary>
    /// The exception handler for HttpErrorResponseException exception.
    /// </summary>
    public class WebExceptionHandler : ExceptionHandler<WebException>
    {
        public WebExceptionHandler(ILogger logger) :
            base(logger)
        {
        }

        public override bool HandleException(IExecutionContext executionContext, WebException exception)
        {
            var requestContext = executionContext.RequestContext;
            var httpErrorResponse = exception.Response as HttpWebResponse;

            if (httpErrorResponse != null)
                requestContext.Metrics.AddProperty(Metric.StatusCode, httpErrorResponse.StatusCode);

            var message = string.Format(CultureInfo.InvariantCulture,
                    "A WebException with status {0} was thrown.", exception.Status);
            throw new AmazonServiceException(message, exception);
        }
    }
}
