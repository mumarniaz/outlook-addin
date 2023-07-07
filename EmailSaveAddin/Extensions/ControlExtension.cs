using System;
using System.Windows.Forms;

namespace EmailSaveAddin.Extensions
{
    public static class ControlExtension
    {
        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (!control.IsHandleCreated)
            {
                control.CreateControl();
            }

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
