using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MethodInvoker {
    public class DictionaryTypeDescirptor : ICustomTypeDescriptor {
        MethodInfo mInfo;
        Dictionary<ParameterInfo, object> dict;

        public static DictionaryTypeDescirptor FromMethodInfo(MethodInfo mInfo) {
            if (mInfo == null)
                return null;
            var pInfos = mInfo.GetParameters();
            var pValues = pInfos.Select(pInfo => {
                if (pInfo.HasDefaultValue) {
                    return pInfo.DefaultValue;
                } else {
                    var parameterType = pInfo.ParameterType;
                    if (parameterType.IsValueType) {
                        return Activator.CreateInstance(parameterType);
                    } else {
                        return null;
                    }
                }
            });
            var dict = pInfos.Zip(pValues, (k, v) => (k, v)).ToDictionary(item => item.k, item => item.v);
            return new DictionaryTypeDescirptor(dict) { mInfo = mInfo };
        }

        public MethodInfo GetMethodInfo() => mInfo;
        public object[] GetMethodParameters() => dict.Values.ToArray();

        public DictionaryTypeDescirptor(Dictionary<ParameterInfo, object> dict) => this.dict = dict;
        public string GetComponentName() => TypeDescriptor.GetComponentName(this, true);
        public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(this, true);
        public string GetClassName() => TypeDescriptor.GetClassName(this, true);
        public EventDescriptorCollection GetEvents(Attribute[] attributes) => TypeDescriptor.GetEvents(this, attributes, true);
        public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents(this, true);
        public TypeConverter GetConverter() => TypeDescriptor.GetConverter(this, true);
        public object GetPropertyOwner(PropertyDescriptor pd) => dict;
        public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(this, true);
        public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor(this, editorBaseType, true);
        public PropertyDescriptor GetDefaultProperty() => null;
        public PropertyDescriptorCollection GetProperties() => GetProperties(null);
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => new PropertyDescriptorCollection(dict.Keys.Select(key => new DictionaryPropertyDescriptor(key, dict)).ToArray());
    }
}
