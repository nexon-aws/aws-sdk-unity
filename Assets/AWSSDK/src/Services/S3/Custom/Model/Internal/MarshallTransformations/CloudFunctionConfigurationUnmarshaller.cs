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

using System.Collections.Generic;

using Amazon.S3.Model;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.S3.Model.Internal.MarshallTransformations
{
    public class CloudFunctionConfigurationUnmarshaller : IUnmarshaller<CloudFunctionConfiguration, XmlUnmarshallerContext>, IUnmarshaller<CloudFunctionConfiguration, JsonUnmarshallerContext> 
    {
        public CloudFunctionConfiguration Unmarshall(XmlUnmarshallerContext context)
        {
            CloudFunctionConfiguration cloudFunctionConfiguration = new CloudFunctionConfiguration();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Id", targetDepth))
                    {
                        cloudFunctionConfiguration.Id = StringUnmarshaller.GetInstance().Unmarshall(context);

                        continue;
                    }
                    if (context.TestExpression("Event", targetDepth))
                    {
                        cloudFunctionConfiguration.Events.Add(StringUnmarshaller.GetInstance().Unmarshall(context));

                        continue;
                    }
                    if (context.TestExpression("CloudFunction", targetDepth))
                    {
                        cloudFunctionConfiguration.CloudFunction = StringUnmarshaller.GetInstance().Unmarshall(context);

                        continue;
                    }
                    if (context.TestExpression("InvocationRole", targetDepth))
                    {
                        cloudFunctionConfiguration.InvocationRole = StringUnmarshaller.GetInstance().Unmarshall(context);

                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return cloudFunctionConfiguration;
                }
            }

            return cloudFunctionConfiguration;
        }

        public CloudFunctionConfiguration Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static CloudFunctionConfigurationUnmarshaller _instance;

        public static CloudFunctionConfigurationUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CloudFunctionConfigurationUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
