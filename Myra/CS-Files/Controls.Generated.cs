/* Generated by MyraPad at Tue, 09. 05. 2023 02  :  23  :  37 */
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI.Properties;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANXY
{
	partial class Controls: Grid
	{
		private void BuildUI()
		{
			var label1 = new Label();
			label1.Text = "Main Key";
			label1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label1.GridColumn = 2;

			var label2 = new Label();
			label2.Text = "Alternate Key";
			label2.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label2.GridColumn = 3;

			var label3 = new Label();
			label3.Text = "Movement";
			label3.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label3.GridRow = 1;

			var label4 = new Label();
			label4.Text = "Move Left";
			label4.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label4.GridColumn = 1;
			label4.GridRow = 2;

			btnMoveLeft = new TextButton();
			btnMoveLeft.Text = "A";
			btnMoveLeft.MinWidth = 100;
			btnMoveLeft.Padding = new Thickness(10);
			btnMoveLeft.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnMoveLeft.GridColumn = 2;
			btnMoveLeft.GridRow = 2;
			btnMoveLeft.Id = "btnMoveLeft";

			btnMoveLeftAlternate = new TextButton();
			btnMoveLeftAlternate.Text = "Left";
			btnMoveLeftAlternate.MinWidth = 100;
			btnMoveLeftAlternate.Padding = new Thickness(10);
			btnMoveLeftAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnMoveLeftAlternate.GridColumn = 3;
			btnMoveLeftAlternate.GridRow = 2;
			btnMoveLeftAlternate.Id = "btnMoveLeftAlternate";

			var label5 = new Label();
			label5.Text = "Move Right";
			label5.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label5.GridColumn = 1;
			label5.GridRow = 3;

			btnMoveRight = new TextButton();
			btnMoveRight.Text = "D";
			btnMoveRight.MinWidth = 100;
			btnMoveRight.Padding = new Thickness(10);
			btnMoveRight.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnMoveRight.GridColumn = 2;
			btnMoveRight.GridRow = 3;
			btnMoveRight.Id = "btnMoveRight";

			btnMoveRightAlternate = new TextButton();
			btnMoveRightAlternate.Text = "Right";
			btnMoveRightAlternate.MinWidth = 100;
			btnMoveRightAlternate.Padding = new Thickness(10);
			btnMoveRightAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnMoveRightAlternate.GridColumn = 3;
			btnMoveRightAlternate.GridRow = 3;
			btnMoveRightAlternate.Id = "btnMoveRightAlternate";

			var label6 = new Label();
			label6.Text = "Jump";
			label6.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label6.GridColumn = 1;
			label6.GridRow = 4;

			btnJump = new TextButton();
			btnJump.Text = "Space";
			btnJump.MinWidth = 100;
			btnJump.Padding = new Thickness(10);
			btnJump.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnJump.GridColumn = 2;
			btnJump.GridRow = 4;
			btnJump.Id = "btnJump";

			btnJumpAlternate = new TextButton();
			btnJumpAlternate.Text = "W";
			btnJumpAlternate.MinWidth = 100;
			btnJumpAlternate.Padding = new Thickness(10);
			btnJumpAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnJumpAlternate.GridColumn = 3;
			btnJumpAlternate.GridRow = 4;
			btnJumpAlternate.Id = "btnJumpAlternate";

			var label7 = new Label();
			label7.Text = "General";
			label7.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label7.GridRow = 5;

			var label8 = new Label();
			label8.Text = "Menu";
			label8.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label8.GridColumn = 1;
			label8.GridRow = 6;

			btnMenu = new TextButton();
			btnMenu.Text = "ESC";
			btnMenu.MinWidth = 100;
			btnMenu.Padding = new Thickness(10);
			btnMenu.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnMenu.GridColumn = 2;
			btnMenu.GridRow = 6;
			btnMenu.Id = "btnMenu";

			btnMenuAlternate = new TextButton();
			btnMenuAlternate.MinWidth = 100;
			btnMenuAlternate.Padding = new Thickness(10);
			btnMenuAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnMenuAlternate.GridColumn = 3;
			btnMenuAlternate.GridRow = 6;
			btnMenuAlternate.Id = "btnMenuAlternate";

			var label9 = new Label();
			label9.Text = "Show FPS";
			label9.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label9.GridColumn = 1;
			label9.GridRow = 7;

			btnShowFps = new TextButton();
			btnShowFps.Text = "F1";
			btnShowFps.MinWidth = 100;
			btnShowFps.Padding = new Thickness(10);
			btnShowFps.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnShowFps.GridColumn = 2;
			btnShowFps.GridRow = 7;
			btnShowFps.Id = "btnShowFps";

			btnShowFpsAlternate = new TextButton();
			btnShowFpsAlternate.MinWidth = 100;
			btnShowFpsAlternate.Padding = new Thickness(10);
			btnShowFpsAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnShowFpsAlternate.GridColumn = 3;
			btnShowFpsAlternate.GridRow = 7;
			btnShowFpsAlternate.Id = "btnShowFpsAlternate";

			var label10 = new Label();
			label10.Text = "V Sync (FPS cap)";
			label10.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			label10.GridColumn = 1;
			label10.GridRow = 8;

			btnCapFPS = new TextButton();
			btnCapFPS.Text = "F4";
			btnCapFPS.MinWidth = 100;
			btnCapFPS.Padding = new Thickness(10);
			btnCapFPS.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnCapFPS.GridColumn = 2;
			btnCapFPS.GridRow = 8;
			btnCapFPS.Id = "btnCapFPS";

			btnCapFPSAlternate = new TextButton();
			btnCapFPSAlternate.MinWidth = 100;
			btnCapFPSAlternate.Padding = new Thickness(10);
			btnCapFPSAlternate.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnCapFPSAlternate.GridColumn = 3;
			btnCapFPSAlternate.GridRow = 8;
			btnCapFPSAlternate.Id = "btnCapFPSAlternate";

			btnReturnFromControls = new TextButton();
			btnReturnFromControls.Text = "Return";
			btnReturnFromControls.MinWidth = 100;
			btnReturnFromControls.Padding = new Thickness(10);
			btnReturnFromControls.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnReturnFromControls.GridRow = 10;
			btnReturnFromControls.Id = "btnReturnFromControls";

			btnLoadDefaults = new TextButton();
			btnLoadDefaults.Text = "Load Defaults";
			btnLoadDefaults.MinWidth = 100;
			btnLoadDefaults.Padding = new Thickness(10);
			btnLoadDefaults.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnLoadDefaults.GridColumn = 2;
			btnLoadDefaults.GridRow = 10;
			btnLoadDefaults.Id = "btnLoadDefaults";

			btnSaveChanges = new TextButton();
			btnSaveChanges.Text = "Save Changes";
			btnSaveChanges.MinWidth = 100;
			btnSaveChanges.Padding = new Thickness(10);
			btnSaveChanges.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			btnSaveChanges.GridColumn = 3;
			btnSaveChanges.GridRow = 10;
			btnSaveChanges.Id = "btnSaveChanges";

			
			ColumnSpacing = 10;
			RowSpacing = 5;
			ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			Margin = new Thickness(10);
			Padding = new Thickness(10);
			Background = new SolidBrush("#0000FF22");
			Widgets.Add(label1);
			Widgets.Add(label2);
			Widgets.Add(label3);
			Widgets.Add(label4);
			Widgets.Add(btnMoveLeft);
			Widgets.Add(btnMoveLeftAlternate);
			Widgets.Add(label5);
			Widgets.Add(btnMoveRight);
			Widgets.Add(btnMoveRightAlternate);
			Widgets.Add(label6);
			Widgets.Add(btnJump);
			Widgets.Add(btnJumpAlternate);
			Widgets.Add(label7);
			Widgets.Add(label8);
			Widgets.Add(btnMenu);
			Widgets.Add(btnMenuAlternate);
			Widgets.Add(label9);
			Widgets.Add(btnShowFps);
			Widgets.Add(btnShowFpsAlternate);
			Widgets.Add(label10);
			Widgets.Add(btnCapFPS);
			Widgets.Add(btnCapFPSAlternate);
			Widgets.Add(btnReturnFromControls);
			Widgets.Add(btnLoadDefaults);
			Widgets.Add(btnSaveChanges);
		}

		
		public TextButton btnMoveLeft;
		public TextButton btnMoveLeftAlternate;
		public TextButton btnMoveRight;
		public TextButton btnMoveRightAlternate;
		public TextButton btnJump;
		public TextButton btnJumpAlternate;
		public TextButton btnMenu;
		public TextButton btnMenuAlternate;
		public TextButton btnShowFps;
		public TextButton btnShowFpsAlternate;
		public TextButton btnCapFPS;
		public TextButton btnCapFPSAlternate;
		public TextButton btnReturnFromControls;
		public TextButton btnLoadDefaults;
		public TextButton btnSaveChanges;
	}
}