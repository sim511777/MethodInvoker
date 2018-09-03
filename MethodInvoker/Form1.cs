using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Dynamic;

namespace MethodInvoker {
   public partial class Form1 : Form {
      public Form1() {
         InitializeComponent();
         InitFunctionList();
      }

      private void InitFunctionList() {
         var type = typeof(Functions);
         MethodInfo[] mis = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);
         var list = mis.Select(m => new ListObject(m)).ToArray();
         this.cbxFunction.DisplayMember = "DisplayMember";
         this.cbxFunction.ValueMember = "ValueMember";
         this.cbxFunction.Items.AddRange(list);
      }

      private void btnRun_Click(object sender, EventArgs e) {
         var method = ((ListObject)this.cbxFunction.SelectedItem).ValueMember;
         if (method == null)
            return;
         var adapter = this.grdParameter.SelectedObject as DictionaryPropertyGridAdapter;
         var props = adapter.GetProperties(null).Cast<PropertyDescriptor>();
         object r = method.Invoke(null, props.Select(prop => prop.GetValue(null)).ToArray());
         this.lbxLog.Items.Add(r.ToString());
      }

      private void cbxFunction_SelectedIndexChanged(object sender, EventArgs e) {
         var methodInfo = ((ListObject)this.cbxFunction.SelectedItem).ValueMember;
         var paramInfos = methodInfo.GetParameters();
         Hashtable dict = new Hashtable();
         foreach (var pi in paramInfos) {
            dict[pi.Name] = Activator.CreateInstance(pi.ParameterType);
         }

         grdParameter.SelectedObject = new DictionaryPropertyGridAdapter(dict);
      }
   }

   public class ListObject {
      public string DisplayMember { get; set; }
      public MethodInfo ValueMember { get; set; }
      public ListObject(MethodInfo mi) {
         var paramDisp = string.Join(", ", mi.GetParameters().Select(prm => $"{prm.ParameterType.Name} {prm.Name}").ToArray());
         this.DisplayMember = $"{mi.ReturnType.Name} {mi.Name}({paramDisp})";
         this.ValueMember = mi;
      }
   }


   public class Functions {
      public static int Add(int a, int b) => a + b;
      public static int Sub(int a, int b) => a - b;
      public static int Mul(int a, int b) => a * b;
      public static int Div(int a, int b) => a / b;
      public static int Neg(int a) => -a;
   }

   class DictionaryPropertyGridAdapter : ICustomTypeDescriptor {
      IDictionary _dictionary;

      public DictionaryPropertyGridAdapter(IDictionary d) {
         _dictionary = d;
      }

      public string GetComponentName() {
         return TypeDescriptor.GetComponentName(this, true);
      }

      public EventDescriptor GetDefaultEvent() {
         return TypeDescriptor.GetDefaultEvent(this, true);
      }

      public string GetClassName() {
         return TypeDescriptor.GetClassName(this, true);
      }

      public EventDescriptorCollection GetEvents(Attribute[] attributes) {
         return TypeDescriptor.GetEvents(this, attributes, true);
      }

      EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents() {
         return TypeDescriptor.GetEvents(this, true);
      }

      public TypeConverter GetConverter() {
         return TypeDescriptor.GetConverter(this, true);
      }

      public object GetPropertyOwner(PropertyDescriptor pd) {
         return _dictionary;
      }

      public AttributeCollection GetAttributes() {
         return TypeDescriptor.GetAttributes(this, true);
      }

      public object GetEditor(Type editorBaseType) {
         return TypeDescriptor.GetEditor(this, editorBaseType, true);
      }

      public PropertyDescriptor GetDefaultProperty() {
         return null;
      }

      PropertyDescriptorCollection
          System.ComponentModel.ICustomTypeDescriptor.GetProperties() {
         return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
      }

      public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
         ArrayList properties = new ArrayList();
         foreach (DictionaryEntry e in _dictionary) {
            properties.Add(new DictionaryPropertyDescriptor(_dictionary, e.Key));
         }

         PropertyDescriptor[] props =
             (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

         return new PropertyDescriptorCollection(props);
      }
   }

   class DictionaryPropertyDescriptor : PropertyDescriptor {
      IDictionary _dictionary;
      object _key;

      internal DictionaryPropertyDescriptor(IDictionary d, object key)
          : base(key.ToString(), null) {
         _dictionary = d;
         _key = key;
      }

      public override Type PropertyType {
         get { return _dictionary[_key].GetType(); }
      }

      public override void SetValue(object component, object value) {
         _dictionary[_key] = value;
      }

      public override object GetValue(object component) {
         return _dictionary[_key];
      }

      public override bool IsReadOnly {
         get { return false; }
      }

      public override Type ComponentType {
         get { return null; }
      }

      public override bool CanResetValue(object component) {
         return false;
      }

      public override void ResetValue(object component) {
      }

      public override bool ShouldSerializeValue(object component) {
         return false;
      }
   }
}
