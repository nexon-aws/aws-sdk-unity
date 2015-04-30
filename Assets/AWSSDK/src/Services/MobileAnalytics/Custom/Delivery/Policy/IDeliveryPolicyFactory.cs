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

namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    internal interface IDeliveryPolicyFactory
    {
        /// <summary>
        /// returns a new connectivity policy.
        /// </summary>
        /// <returns>instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicy"/>, which checks for network connectivity</returns>
        IDeliveryPolicy NewConnectivityPolicy();
        
        
        /// <summary>
        /// returns a new force submission time policy
        /// </summary>
        /// <returns>instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicy"/></returns>
        IDeliveryPolicy NewForceSubmissionPolicy();
        
        
        /// <summary>
        /// returns a new force submission time policy
        /// </summary>
        /// <returns>instance of <see cref="Amazon.MobileAnalytics.MobileAnalyticsManager.Internal.IDeliveryPolicy"/></returns>
        IDeliveryPolicy NewBackgroundSubmissionPolicy();
        
    }
}

