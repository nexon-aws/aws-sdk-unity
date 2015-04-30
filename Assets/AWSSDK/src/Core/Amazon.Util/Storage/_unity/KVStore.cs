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
namespace Amazon.Util.Storage
{
    public abstract class KVStore
    {
        public abstract void Clear(string key);
        public abstract void Put(string key, string value);
        public abstract string Get(string key);
    }
}

