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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;

// namespace modified to prevent naming colisions
namespace MessageHandler
{
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This class isn't meant to be used directly")]
    public class DynamicJsonArray : DynamicObject, IEnumerable<object>
    {
        private readonly object[] _arrayValues;

        public DynamicJsonArray(object[] arrayValues)
        {
            Debug.Assert(arrayValues != null);
            _arrayValues = arrayValues.Select(Json.WrapObject).ToArray();
        }

        public int Length
        {
            get { return _arrayValues.Length; }
        }

        public dynamic this[int index]
        {
            get { return _arrayValues[index]; }
            set { _arrayValues[index] = Json.WrapObject(value); }
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (_arrayValues.GetType().IsAssignableFrom(binder.Type))
            {
                result = _arrayValues;
                return true;
            }
            return base.TryConvert(binder, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Testing for members should never throw. This is important when dealing with
            // services that return different json results. Testing for a member shouldn't throw,
            // it should just return null (or undefined)
            result = null;
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            return _arrayValues.GetEnumerator();
        }

        private IEnumerable<object> GetEnumerable()
        {
            return _arrayValues.AsEnumerable();
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This class isn't meant to be used directly")]
        public static implicit operator object[](DynamicJsonArray obj)
        {
            return obj._arrayValues;
        }

        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "This class isn't meant to be used directly")]
        public static implicit operator Array(DynamicJsonArray obj)
        {
            return obj._arrayValues;
        }
    }
}