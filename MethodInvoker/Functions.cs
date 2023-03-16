using System.Drawing;
using System.ComponentModel;

namespace MethodInvoker {
    public class Functions {
        public static int Add(int a = 12, int b = 4) {
            return a + b;
        }

        public static int Sub(int a = 12, int b = 4) {
            return a - b;
        }

        public static int Mul(int a = 12, int b = 4) {
            return a * b;
        }

        public static int Div(int a = 12, int b = 4) {
            return a / b;
        }

        public static int Neg(int a = 12) {
            return -a;
        }

        public static string PrintColor([Description("aaa")] Color color) {
            return color.ToString();
        }
    }
}
