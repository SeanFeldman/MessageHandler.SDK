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
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

// namespace modified to prevent naming colisions
namespace MessageHandler
{
    /// <summary>
    /// Converter that knows how to get the member values from a dynamic object.
    /// </summary>
    internal class DynamicJavaScriptConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                // REVIEW: For some reason the converters don't pick up interfaces
                yield return typeof(IDynamicMetaObjectProvider);
                yield return typeof(DynamicObject);
            }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            var memberNames = DynamicHelper.GetMemberNames(obj);

            // This should never happen
            Debug.Assert(memberNames != null);

            // Get the value for each member in the dynamic object
            foreach (string memberName in memberNames)
            {
                object value = DynamicHelper.GetMemberValue(obj, memberName);

                var jsonValue = value as DynamicJsonArray;
                if (jsonValue != null)
                {
                    value = (object[])jsonValue;
                }

                var s = value as string;
                if (s != null && IsIso8601DateTimeString(s))
                {
                    value = DateTime.Parse(s, null, System.Globalization.DateTimeStyles.RoundtripKind); ;
                }

                values[memberName] = value;
            }

            return values;
        }

        bool IsIso8601DateTimeString(string s)
        {
            var regex = new Regex(@"^([\+-]?\d{4}(?!\d{2}\b))((-?)((0[1-9]|1[0-2])(\3([12]\d|0[1-9]|3[01]))?|W([0-4]\d|5[0-2])(-?[1-7])?|(00[1-9]|0[1-9]\d|[12]\d{2}|3([0-5]\d|6[1-6])))([T\s]((([01]\d|2[0-3])((:?)[0-5]\d)?|24\:?00)([\.,]\d+(?!:))?)?(\17[0-5]\d([\.,]\d+)?)?([zZ]|([\+-])([01]\d|2[0-3]):?([0-5]\d)?)?)?)?$");

            return regex.IsMatch(s);
        }
    }
}