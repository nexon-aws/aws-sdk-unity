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

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.S3.Model
{
    /// <summary>
    /// Container for the parameters to the PutBucketWebsite operation.
    /// <para>Set the website configuration for a bucket.</para>
    /// </summary>
    public partial class PutBucketWebsiteRequest : AmazonWebServiceRequest
    {
        private string bucketName;
        private WebsiteConfiguration websiteConfiguration;

        /// <summary>
        /// The name of the bucket to apply the configuration to.
        /// </summary>
        public string BucketName
        {
            get { return this.bucketName; }
            set { this.bucketName = value; }
        }

        // Check to see if BucketName property is set
        internal bool IsSetBucketName()
        {
            return this.bucketName != null;
        }

        /// <summary>
        /// The website configuration to apply. The configuration defines the index
        /// document suffix and custom error page.
        /// </summary>
        public WebsiteConfiguration WebsiteConfiguration
        {
            get { return this.websiteConfiguration; }
            set { this.websiteConfiguration = value; }
        }

        // Check to see if WebsiteConfiguration property is set
        internal bool IsSetWebsiteConfiguration()
        {
            return this.websiteConfiguration != null;
        }

    }
}
    
