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
            if (cbxFunction.Items.Count > 0 ) {
                cbxFunction.SelectedIndex = 0;
            }
        }

        private void cbxFunction_SelectedIndexChanged(object sender, EventArgs e) {
            // 선택된 함수에 대한 오브젝트를 만들어서 프로퍼티그리드에 지정
            var mInfo = cbxFunction.SelectedItem as MethodInfo;
            var methodParamsObject = DictionaryTypeDescirptor.FromMethodInfo(mInfo);
            grdParameter.SelectedObject = methodParamsObject;
        }

        private void btnRun_Click(object sender, EventArgs e) {
            // 프로퍼티 그리드에 지정된 오브젝트로 함수정보를 얻어와서 함수와 파라미터로 호출
            var methodParamsObject = grdParameter.SelectedObject as DictionaryTypeDescirptor;
            if (methodParamsObject == null)
                return;
            var mInfo = methodParamsObject.GetMethodInfo();
            var prms = methodParamsObject.GetMethodParameters();
            object r = mInfo.Invoke(null, prms);
            tbxLog.AppendText($"Method : {mInfo}\r\n");
            tbxLog.AppendText($"Parameters : {string.Join(", ", prms)}\r\n");
            tbxLog.AppendText($"Return : {r}\r\n");
            tbxLog.AppendText($"=====================================\r\n");
        }
    }
}
