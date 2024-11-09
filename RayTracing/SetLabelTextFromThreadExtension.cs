using System.Windows.Forms;

namespace RayTracing
{
    internal static class SetLabelTextFromThreadExtension
    {
        public static void SetText(this Label label, string text)
        {
            label.Invoke(() => label.Text = text);
        }
    }
}
