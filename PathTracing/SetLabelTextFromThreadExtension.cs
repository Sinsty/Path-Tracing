using System.Windows.Forms;

namespace PathTracing
{
    internal static class SetLabelTextFromThreadExtension
    {
        public static void SetText(this Label label, string text)
        {
            label.Invoke(() => label.Text = text);
        }
    }
}
