using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MethodInvoker {
    public class DictionaryPropertyDescriptor : PropertyDescriptor {
        ParameterInfo pInfo;
        Dictionary<ParameterInfo, object> dict;

        internal DictionaryPropertyDescriptor(ParameterInfo pInfo, Dictionary<ParameterInfo, object> dict) : base(pInfo.Name, null) => (this.pInfo, this.dict) = (pInfo, dict);
        public override Type PropertyType => pInfo.ParameterType;
        public override void SetValue(object component, object value) => dict[pInfo] = value;
        public override object GetValue(object component) => dict[pInfo];
        public override bool IsReadOnly => false;
        public override Type ComponentType => null;
        public override bool CanResetValue(object component) => false;
        public override void ResetValue(object component) { }
        public override bool ShouldSerializeValue(object component) => false;
    }
}
