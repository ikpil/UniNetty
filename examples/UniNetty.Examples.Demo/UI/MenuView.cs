using System.IO;
using ImGuiNET;
using Serilog;
using UniNetty.Examples.DemoSupports;

namespace UniNetty.Examples.Demo.UI;

public class MenuView : IView
{
    private static readonly ILogger Logger = Log.ForContext<UniNettyDemo>();

    private readonly Canvas _canvas;

    public MenuView(Canvas canvas)
    {
        _canvas = canvas;
    }

    public void Draw(double dt)
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Open In File Explorer"))
                {
                    var currentDirectory = Directory.GetCurrentDirectory();
                    DirectoryUtils.OpenDirectory(currentDirectory);
                }
                
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("Help"))
            {
                if (ImGui.MenuItem("Repository"))
                {
                    ExampleSupport.Shared.OpenUrl("https://github.com/ikpil/UniNetty");
                }

                if (ImGui.MenuItem("Nuget"))
                {
                    ExampleSupport.Shared.OpenUrl("https://www.nuget.org/packages/UniNetty.Common/");
                }

                ImGui.Separator();
                if (ImGui.MenuItem("Issue Tracker"))
                {
                    ExampleSupport.Shared.OpenUrl("https://github.com/ikpil/UniNetty/issues");
                }

                if (ImGui.MenuItem("Release Notes"))
                {
                    ExampleSupport.Shared.OpenUrl("https://github.com/ikpil/UniNetty/blob/main/CHANGELOG.md");
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }

    public void Update(double dt)
    {
    }
}