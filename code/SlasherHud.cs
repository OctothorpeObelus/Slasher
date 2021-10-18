using Sandbox;
using Sandbox.UI;

[Library]
public partial class SlasherHud : HudEntity<RootPanel>
{
	public SlasherHud()
	{
		if (!IsClient)
			return;

		RootPanel.StyleSheet.Load("/ui/SlasherUI.scss");

		RootPanel.AddChild<Crosshair>();
		RootPanel.AddChild<SlasherCrosshair>();
		RootPanel.AddChild<PickedUpText>();
		RootPanel.AddChild<SlasherInfo>();
		RootPanel.AddChild<Menu>();
	}
}
