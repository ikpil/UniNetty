using ImGuiNET;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class MenuView : IView
{
    public void Bind(Canvas canvas)
    {
    }

    public void Update(double dt)
    {
    }

    public void Draw(double dt)
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Help"))
            {
                if (ImGui.MenuItem("Repository"))
                {
                    DemoSupport.Shared.OpenUrl("https://github.com/ikpil/UniNetty");
                }

                if (ImGui.MenuItem("Nuget"))
                {
                    DemoSupport.Shared.OpenUrl("https://www.nuget.org/packages/UniNetty.Common/");
                }

                ImGui.Separator();
                if (ImGui.MenuItem("Issue Tracker"))
                {
                    DemoSupport.Shared.OpenUrl("https://github.com/ikpil/UniNetty/issues");
                }

                if (ImGui.MenuItem("Release Notes"))
                {
                    DemoSupport.Shared.OpenUrl("https://github.com/ikpil/UniNetty/blob/main/CHANGELOG.md");
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }
}