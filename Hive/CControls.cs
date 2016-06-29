using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Runtime.InteropServices;

/*
 * 
 * NOTE : These classes and logic will work only and only if the
 * following key in the registry is set
 * HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\EnableToolTips\
 * 
 * VSNET ver1 : Provide a manifest file in the executable output firectory;
 * VSNET ver2 : CAll Application.EnableVisualStyles(), before InitializeComponent()
*/

namespace Hive
{
    /// <summary>
    /// TooltipIcon类型枚举
    /// </summary>
	public enum TooltipIcon : int
	{
		None,
		Info,
		Warning,
		Error
	}

	/// <summary>
	/// A sample class to manipulate ballon tooltips.
	/// Windows XP balloon-tips if used properly can 
	/// be very helpful.
	/// This class creates a balloon tooltip in the form of a message.
	/// This becomes useful for showing important information 
	/// quickly to the user.
	/// Ever so often we need to avoid certain invalid characters in 
	/// input textboxes. Though we do a good job of filtering the characters,
	/// we dont necessarily do a good job of informing the user.
	/// This class helps immensely under such scenarios.
	/// This also helps in a shorter learning cycle of the 
	/// application.
	/// NOTE: The difference between this and other balloon classes is 
	/// that this is made specifically for the edit control and 
	/// displays the balloon at the caret position within the textbox.
	/// </summary>
	public class EditBalloon 
	{
		private Control m_parent;

		private string m_text = "FMS Balloon Tooltip Control Display Message";
		private string m_title = "FMS Balloon Tooltip Message";
		private TooltipIcon m_titleIcon = TooltipIcon.None;

		private const int ECM_FIRST = 0x1500;
		private const int EM_SHOWBALLOONTIP = ECM_FIRST + 3;

		[DllImport("User32", SetLastError=true)]
		private static extern int SendMessage(
			IntPtr hWnd,
			int Msg,
			int wParam,
			IntPtr lParam);

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		private struct EDITBALLOONTIP
		{
			public int cbStruct;
			public string pszTitle;
			public string pszText;
			public int ttiIcon;
		}

		public EditBalloon()
		{
		}

		public EditBalloon(Control parent)
		{
			m_parent = parent;
		}

		/// <summary>
		/// Show a balloon tooltip for edit control.
		/// </summary>
		public void Show()
		{
            try//曾经发生过异常，所以就加了
            {
                EDITBALLOONTIP ebt = new EDITBALLOONTIP();

                ebt.cbStruct = Marshal.SizeOf(ebt);
                ebt.pszText = m_text;
                ebt.pszTitle = m_title;
                ebt.ttiIcon = (int)m_titleIcon;

                IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ebt));
                Marshal.StructureToPtr(ebt, ptrStruct, true);

                System.Diagnostics.Debug.Assert(m_parent != null, "Parent control is null", "Set parent before calling Show");

                int ret = SendMessage(m_parent.Handle,
                    EM_SHOWBALLOONTIP,
                    0, ptrStruct);

                Marshal.FreeHGlobal(ptrStruct);
            }
            catch 
            {
            }
		}

		/// <summary>
		/// Sets or gets the Title.
		/// </summary>
		public string Title
		{
			get
			{
				return m_title;
			}
			set
			{
				m_title = value;
			}
		}

		/// <summary>
		/// Sets or gets the display icon.
		/// </summary>
		public TooltipIcon TitleIcon
		{
			get
			{
				return m_titleIcon;
			}
			set
			{
				m_titleIcon = value;
			}
		}

		/// <summary>
		/// Sets or gets the display text.
		/// </summary>
		public string Text
		{
			get
			{
				return m_text;
			}
			set
			{
				m_text = value;
			}
		}

		/// <summary>
		/// Sets or gets the parent.
		/// </summary>
		public Control Parent
		{
			get
			{
				return m_parent;
			}
			set
			{
				m_parent = value;
			}
		}

	}
    //EditBalloon---------------------------------------END
}
