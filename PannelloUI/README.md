# AboutForm Integration Guide

This guide explains how to integrate the `AboutForm` component from the PannelloUI project into your own Windows Forms applications.

## Overview

`AboutForm` is a comprehensive "About" dialog that combines:
- Version information display
- Embedded `FireworksPanel` control for visual effects
- Integrated `FireworksSettingsPanel` for live configuration
- GPU information display
- Repository link
- Burst effect button

## Prerequisites

1. **FireworksDX Library**: Reference the `FireworksDX.csproj` or DLL
2. **.NET 9.0** or higher (Windows Forms)
3. Required dependencies (automatically resolved via FireworksDX):
   - SharpDX libraries
   - NAudio

## Files Required

To integrate `AboutForm` into your project, you need these files from the PannelloUI project:

```
PannelloUI/
├── AboutForm.cs                        # Main form code
├── AboutForm.Designer.cs               # Form designer code
├── AboutForm.resx                      # Form resources
├── FireworksSettingsPanel.cs           # Settings panel code
├── FireworksSettingsPanel.Designer.cs  # Settings designer code
└── FireworksSettingsPanel.resx         # Settings resources
```

## Integration Steps

### Step 1: Add Project Reference

Add a reference to the FireworksDX library in your project file:

```xml
<ItemGroup>
  <ProjectReference Include="..\FireworksDX\FireworksDX.csproj" />
</ItemGroup>
```

Or reference the compiled DLL:

```xml
<ItemGroup>
  <Reference Include="FireworksDX">
    <HintPath>..\path\to\FireworksDX.dll</HintPath>
  </Reference>
</ItemGroup>
```

### Step 2: Copy Files

1. Create a folder in your project (e.g., `AboutUI` or `Dialogs`)
2. Copy all six files listed above into that folder
3. Add them to your project in Visual Studio

### Step 3: Update Namespaces

Update the namespace in the copied files to match your project:

**Original** (PannelloUI):
```csharp
namespace PannelloUI
{
    public partial class AboutForm : Form
    {
        // ...
    }
}
```

**Updated** (YourProject):
```csharp
namespace YourProject.Dialogs
{
    public partial class AboutForm : Form
    {
        // ...
    }
}
```

Do this for both `AboutForm.cs` and `FireworksSettingsPanel.cs`.

### Step 4: Update Designer Files

Update the namespace in both Designer files:
- `AboutForm.Designer.cs`
- `FireworksSettingsPanel.Designer.cs`

```csharp
namespace YourProject.Dialogs
{
    partial class AboutForm
    {
        // ...
    }
}
```

### Step 5: Show the Form

Display the AboutForm from your application:

```csharp
using YourProject.Dialogs;

// In your menu click handler or help button
private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
{
    using var aboutForm = new AboutForm();
    aboutForm.ShowDialog(this);
}
```

## Customization

### Change Version Display

The version is automatically read from the entry assembly. To customize:

```csharp
// In AboutForm.cs constructor
lblVersion.Text = $"Version: {Assembly.GetEntryAssembly()?.GetName().Version?.ToString() 
                             ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString()}";

// Custom version string
lblVersion.Text = "Version: 1.0.0 Beta";
```

### Change Repository Link

Modify the link in `AboutForm.cs`:

```csharp
private void lnkRepo_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/yourusername/yourproject", // Change this
            UseShellExecute = true
        });
    }
    catch
    {
        // Handle errors silently
    }
}
```

### Customize Burst Count

Change the number of rockets launched by the burst button:

```csharp
private void btnBurst_Click(object? sender, EventArgs e)
{
    LaunchBurst(10); // Change from 5 to 10 rockets
}
```

### Localization

The original AboutForm contains Italian text. Update labels in the Designer:

**Key controls to translate**:
- `lblVersion`: "Versione:" → "Version:"
- GPU button text: Update in designer
- Message boxes: Update in code-behind

```csharp
// In btnGpuInfo_Click
MessageBox.Show(
    $"{info}\n\n{gpuStatus}", 
    "GPU DirectX Information",  // Translated title
    MessageBoxButtons.OK, 
    MessageBoxIcon.Information
);
```

## Advanced Usage

### Access Fireworks Engine

The AboutForm uses reflection to access the private `engine` field in `FireworksPanel`:

```csharp
var panelType = fireworksPanel.GetType();
var engineField = panelType.GetField("engine", BindingFlags.NonPublic | BindingFlags.Instance);
var engine = engineField?.GetValue(fireworksPanel);

if (engine != null)
{
    var engineType = engine.GetType();
    var launchMethod = engineType.GetMethod("LaunchRocket", 
        BindingFlags.NonPublic | BindingFlags.Instance);
    
    // Launch a rocket
    launchMethod.Invoke(engine, new object[] { width, height });
}
```

### Custom Settings Panel Integration

You can host `FireworksSettingsPanel` in any container:

```csharp
var settingsPanel = new FireworksSettingsPanel
{
    TopLevel = false,
    FormBorderStyle = FormBorderStyle.None,
    Dock = DockStyle.Fill
};

// Add to any Panel or container
yourPanel.Controls.Add(settingsPanel);
settingsPanel.Show();
```

### Programmatic Profile Selection

Control fireworks profiles programmatically:

```csharp
using FireworksDX.Config;

// Load a specific profile
FireworksConfig.LoadNewYearShow();
FireworksConfig.EnableGlow = true;
FireworksConfig.EnableTwinkle = true;
FireworksConfig.EnableAudio = false;

// Changes take effect immediately on all active FireworksPanel controls
```

## Designer Considerations

### Required Controls in AboutForm

The `AboutForm.Designer.cs` expects these controls:
- `lblVersion`: Label for version display
- `lnkRepo`: LinkLabel for repository link
- `btnBurst`: Button to trigger burst effect
- `btnGpuInfo`: Button to show GPU information
- `fireworksPanel`: Instance of `FireworksPanel` control
- `panelSettingsHost`: Panel to host the settings form

Ensure these controls exist in your designer file, or adjust the code accordingly.

### Layout Tips

- Use `DockStyle.Fill` for the fireworks panel to maximize visual impact
- Place settings panel in a sidebar (e.g., `DockStyle.Right` with fixed width)
- Consider making the form resizable for better user experience

## Troubleshooting

### "FireworksPanel not found"

Ensure the FireworksDX assembly is properly referenced and the namespace is imported:

```csharp
using FireworksDX.WinForms;
```

### "FireworksSettingsPanel not visible"

Make sure you call `.Show()` after adding to the host panel:

```csharp
panelSettingsHost.Controls.Add(_settingsForm);
_settingsForm.Show();
```

### Audio not playing

Check these items:
1. `Audio\explosion.wav` is copied to output directory
2. `FireworksConfig.EnableAudio = true`
3. `engine.EnableSound = true`

### GPU information shows software rendering

DirectX hardware acceleration may not be available. Possible causes:
- Running in a virtual machine
- Outdated graphics drivers
- Remote desktop session
- Graphics card doesn't support Direct3D 11

## Example: Minimal Integration

Here's a minimal example showing AboutForm in a new project:

```csharp
using System;
using System.Windows.Forms;

namespace MyApplication
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "My Application";
            
            var menuStrip = new MenuStrip();
            var helpMenu = new ToolStripMenuItem("Help");
            var aboutItem = new ToolStripMenuItem("About...");
            
            aboutItem.Click += (s, e) =>
            {
                using var about = new AboutForm();
                about.ShowDialog(this);
            };
            
            helpMenu.DropDownItems.Add(aboutItem);
            menuStrip.Items.Add(helpMenu);
            
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
```

## Performance Notes

- The fireworks animation runs at ~60 FPS
- Hardware acceleration significantly reduces CPU usage
- Consider pausing animation when form is minimized (implement form state events)
- Multiple instances can run simultaneously but will consume more resources

## Further Customization

The AboutForm is designed to be modular. You can:
- Replace the `FireworksPanel` with any other control
- Remove the burst button if not needed
- Hide GPU information button for simpler deployments
- Customize colors, fonts, and layout in the Designer

## Related Documentation

For more information about the FireworksDX library features and configuration options, see:
- [FireworksDX README](../FireworksDX/README.md)
- FireworksDX source code documentation

## Support

[Add your support information or links here]

## License

LICENSED UNDER THE MIT LICENSE. SEE LICENSE FILE FOR DETAILS.

## Author

G.L. aka Firefox_1998