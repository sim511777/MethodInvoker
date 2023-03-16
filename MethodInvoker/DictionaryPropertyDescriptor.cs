using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MethodInvoker {
    class DictionaryPropertyDescriptor : PropertyDescriptor {
        string key;
        Dictionary<string, object> dict;

        internal DictionaryPropertyDescriptor(string key, Dictionary<string, object> dict) : base(key, null) {
            this.key = key;
            this.dict = dict;
        }

        public override Type PropertyType => dict[key].GetType();

        public override void SetValue(object component, object value) => dict[key] = value;

        public override object GetValue(object component) => dict[key];

        public override bool IsReadOnly => false;

        public override Type ComponentType => null;

        public override bool CanResetValue(object component) => false;

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) => false;
    }
}
