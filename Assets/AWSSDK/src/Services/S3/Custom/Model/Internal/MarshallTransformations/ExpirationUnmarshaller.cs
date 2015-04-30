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

using System.Collections.Generic;

using Amazon.S3.Model;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.S3.Model.Internal.MarshallTransformations
{
     /// <summary>
     ///   Expiration Unmarshaller
     /// </summary>
    internal class ExpirationUnmarshaller : IUnmarshaller<LifecycleRuleExpiration, XmlUnmarshallerContext>, IUnmarshaller<LifecycleRuleExpiration, JsonUnmarshallerContext> 
    {
        public LifecycleRuleExpiration Unmarshall(XmlUnmarshallerContext context) 
        {
            LifecycleRuleExpiration expiration = new LifecycleRuleExpiration();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Date", targetDepth))
                    {
                        expiration.Date = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                            
                        continue;
                    }
                    if (context.TestExpression("Days", targetDepth))
                    {
                        expiration.Days = IntUnmarshaller.GetInstance().Unmarshall(context);
                            
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return expiration;
                }
            }
                        


            return expiration;
        }

        public LifecycleRuleExpiration Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static ExpirationUnmarshaller _instance;

        public static ExpirationUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExpirationUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
