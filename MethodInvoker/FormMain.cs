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
    public partial class FormMain : Form {
        public FormMain() {
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
            grdParameter.SelectedObject = GetDictTypeDescriptor(mInfo);
        }

        private DictionaryTypeDescirptor GetDictTypeDescriptor(MethodInfo mInfo) {
            if (mInfo == null)
                return null;
            var pInfos = mInfo.GetParameters();
            var dict = pInfos.ToDictionary(pInfo => pInfo, pInfo => {
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
            return new DictionaryTypeDescirptor(dict);
        }

        private void btnRun_Click(object sender, EventArgs e) {
            // 런 버튼을 누르면
            // 1. 태그로 부터 함수정보와
            // 2. 프로퍼티 그리드로 부터 파라미터들을 구하고
            // 3. 호출
            var mInfo = grdParameter.Tag as MethodInfo;
            if (mInfo == null)
                return;
            var prms = GetParameters(grdParameter.SelectedObject as DictionaryTypeDescirptor);
            object r = mInfo.Invoke(null, prms);
            lbxLog.Items.Add($"Method : {mInfo}");
            lbxLog.Items.Add($"Parameters : {string.Join(", ", prms)}");
            lbxLog.Items.Add($"Return : {r}");
            lbxLog.Items.Add($"=====================================");
        }

        private static object[] GetParameters(DictionaryTypeDescirptor typeDescriptor) {
            var dict = typeDescriptor.GetPropertyOwner(null) as Dictionary<ParameterInfo, object>;
            return dict.Values.Cast<object>().ToArray();
        }
    }
}
