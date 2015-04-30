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
using System.IO;
using System.Collections.Generic;
using System.Text;

using ThirdParty.Json.LitJson;

namespace Amazon.Runtime.Internal.Settings
{
    public class SettingsCollection : IEnumerable<SettingsCollection.ObjectSettings>
    {
        Dictionary<string, Dictionary<string, object>> _values;
        public SettingsCollection()
        {
            this._values = new Dictionary<string, Dictionary<string, object>>();
            this.InitializedEmpty = true;
        }

        public SettingsCollection(Dictionary<string, Dictionary<string, object>> values)
        {
            this._values = values;
            this.InitializedEmpty = false;
        }

        public int Count
        {
            get { return this._values.Count; }
        }

        public bool InitializedEmpty { get; private set; }

        internal void Persist(StreamWriter writer)
        {
            JsonWriter jsonWriter = new JsonWriter();
            jsonWriter.PrettyPrint = true;

            jsonWriter.WriteObjectStart();
            foreach (var key in this._values.Keys)
            {
                ObjectSettings os = this[key];
                jsonWriter.WritePropertyName(key);
                os.WriteToJson(jsonWriter);
            }
            jsonWriter.WriteObjectEnd();

            string content = jsonWriter.ToString();
            writer.Write(content);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<SettingsCollection.ObjectSettings> GetEnumerator()
        {
            foreach (var key in this._values.Keys)
            {
                ObjectSettings os = this[key];
                yield return os;
            }
        }

        public ObjectSettings this[string key]
        {
            get 
            {
                Dictionary<string, object> values;
                if (!this._values.TryGetValue(key, out values))
                {
                    return NewObjectSettings(key);
                }

                return new ObjectSettings(key, values);
            }
        }

        public ObjectSettings NewObjectSettings()
        {
            string uniqueKey = Guid.NewGuid().ToString();
            return NewObjectSettings(uniqueKey);
        }

        public ObjectSettings NewObjectSettings(string uniqueKey)
        {
            Dictionary<string, object> backStore = new Dictionary<string, object>();
            ObjectSettings settings = new ObjectSettings(uniqueKey, backStore);
            this._values[uniqueKey] = backStore;
            return settings;
        }

        public void Remove(string uniqueKey)
        {
            this._values.Remove(uniqueKey);
        }

        public void Clear()
        {
            this._values.Clear();
        }

        public class ObjectSettings
        {
            string _uniqueKey;
            Dictionary<string, object> _values;

            internal ObjectSettings(string uniqueKey, Dictionary<string, object> values)
            {
                this._uniqueKey = uniqueKey;
                this._values = values;
            }

            public string UniqueKey
            {
                get { return this._uniqueKey; }
            }

            public string this[string key]
            {
                get 
                {
                    object o;
                    this._values.TryGetValue(key, out o);
                    return o as string; 
                }
                set { this._values[key] = value; }
            }

            public string GetValueOrDefault(string key, string defaultValue)
            {
                var value = this[key];
                if (value == null)
                    return defaultValue;

                return value;
            }

            public void Remove(string key)
            {
                this._values.Remove(key);
            }

            public bool IsEmpty
            {
                get { return this._values == null || (this._values != null && this._values.Count == 0); }
            }

            public IEnumerable<string> Keys
            {
                get
                {
                    Dictionary<string, object>.KeyCollection keys = this._values.Keys;
                    string[] k = new string[keys.Count];
                    this._values.Keys.CopyTo(k,0);
                    return k;
                }
            }

            internal void WriteToJson(JsonWriter writer)
            {
                writer.WriteObjectStart();
                foreach (var kvp in this._values)
                {
                    string value = kvp.Value as string;
                    if (value == null)
                        continue;

                    writer.WritePropertyName(kvp.Key);

                    if (PersistenceManager.Instance.IsEncrypted(kvp.Key) || PersistenceManager.Instance.IsEncrypted(this._uniqueKey))
                        value = UserCrypto.Encrypt(value);

                    writer.Write(value);
                }
                writer.WriteObjectEnd();
            }
        }
    }
}
