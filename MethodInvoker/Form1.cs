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
            cbxFunction.DisplayMember = "Name";
            InitMethodList();
        }

        private void InitMethodList() {
            // 함수 리스트를 콤보박스에 추가
            var type = typeof(Functions);
            var mInfos = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);
            cbxFunction.Items.AddRange(mInfos);
        }

        private void cbxFunction_SelectedIndexChanged(object sender, EventArgs e) {
            // 콤보박스가 선택되면
            // 1. 선택된 함수를 태그에 달아줌
            // 2. 파라미터들을 딕셔너리로 만들고 그것을 딕셔너리 아답터로 만들어서 프로퍼티 그리드에 선택해 줌
            var mInfo = cbxFunction.SelectedItem as MethodInfo;
            grdParameter.Tag = mInfo;
            if (mInfo != null) {
                var pInfos = mInfo.GetParameters();
                var dict = pInfos.ToDictionary(pInfo => pInfo.Name, pInfo => pInfo.HasDefaultValue ? pInfo.DefaultValue : Activator.CreateInstance(pInfo.ParameterType));
                var dictAdapter = new DictionaryPropertyGridAdapter(dict);
                grdParameter.SelectedObject = dictAdapter;
            } else {
                grdParameter.SelectedObject = null;
            }
        }

        private void btnRun_Click(object sender, EventArgs e) {
            // 런 버튼을 누르면
            // 1. 태그로 부터 함수정보와
            // 2. 프로퍼티 그리드로 부터 파라미터들을 구하고
            // 3. 호출
            var mInfo = grdParameter.Tag as MethodInfo;
            if (mInfo == null)
                return;
            var adapter = grdParameter.SelectedObject as DictionaryPropertyGridAdapter;
            var dict = adapter.GetPropertyOwner(null) as Dictionary<string, object>;
            var prms = dict.Values.Cast<object>();
            object r = mInfo.Invoke(null, prms.ToArray());
            lbxLog.Items.Add(r.ToString());
        }
    }
}
