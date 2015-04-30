//
// Copyright 2014-2015 Amazon.com, Inc. or its affiliates. All Rights Reserved.
//
//
// Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
// You may not use this file except in compliance with the License.
// A copy of the License is located in the "license" file accompanying this file.
// See the License for the specific language governing permissions and limitations under the License.
//
//

/*
 * Do not modify this file. This file is generated from the dynamodb-2012-08-10.normal.json service model.
 */
using System;
using System.Net;
using Amazon.Runtime;

namespace Amazon.DynamoDBv2.Model
{
    ///<summary>
    /// DynamoDB exception
    /// </summary>
    public class InternalServerErrorException : AmazonDynamoDBException 
    {
        /// <summary>
        /// Constructs a new InternalServerErrorException with the specified error
        /// message.
        /// </summary>
        /// <param name="message">
        /// Describes the error encountered.
        /// </param>
        public InternalServerErrorException(string message) 
            : base(message) {}
          
        public InternalServerErrorException(string message, Exception innerException) 
            : base(message, innerException) {}
            
        public InternalServerErrorException(Exception innerException) 
            : base(innerException) {}
            
        public InternalServerErrorException(string message, Exception innerException, ErrorType errorType, string errorCode, string RequestId, HttpStatusCode statusCode) 
            : base(message, innerException, errorType, errorCode, RequestId, statusCode) {}

        public InternalServerErrorException(string message, ErrorType errorType, string errorCode, string RequestId, HttpStatusCode statusCode) 
            : base(message, errorType, errorCode, RequestId, statusCode) {}

    }
}