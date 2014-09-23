//Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//Microsoft Open Technologies would like to thank its contributors, a list
//of whom are at http://aspnetwebstack.codeplex.com/wikipage?title=Contributors.

//Licensed under the Apache License, Version 2.0 (the "License"); you
//may not use this file except in compliance with the License. You may
//obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//implied. See the License for the specific language governing permissions
//and limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;

// namespace modified to prevent naming colisions
namespace MessageHandler
{
    public class DynamicJsonObject : DynamicObject
    {
        private readonly IDictionary<string, object> _values;

        public DynamicJsonObject(IDictionary<string, object> values)
        {
            Debug.Assert(values != null);
            _values = values.ToDictionary(p => p.Key, p => Json.WrapObject(p.Value),
                StringComparer.OrdinalIgnoreCase);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = null;
            if (binder.Type.IsAssignableFrom(_values.GetType()))
            {
                result = _values;
            }
            else
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Unable to convert to \"{0}\". Use Json.Decode<T> instead.", binder.Type));
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetValue(binder.Name);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _values[binder.Name] = Json.WrapObject(value);
            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            string key = GetKey(indexes);
            if (!String.IsNullOrEmpty(key))
            {
                _values[key] = Json.WrapObject(value);
            }
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            string key = GetKey(indexes);
            result = null;
            if (!String.IsNullOrEmpty(key))
            {
                result = GetValue(key);
            }
            return true;
        }

        private static string GetKey(object[] indexes)
        {
            if (indexes.Length == 1)
            {
                return (string)indexes[0];
            }
            // REVIEW: Should this throw?
            return null;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _values.Keys;
        }

        private object GetValue(string name)
        {
            object result;
            if (_values.TryGetValue(name, out result))
            {
                return result;
            }
            return null;
        }
    }
}