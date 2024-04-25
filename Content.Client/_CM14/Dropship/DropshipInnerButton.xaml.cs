﻿using Content.Client.Message;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Timing;

namespace Content.Client._CM14.Dropship;

[GenerateTypedNameReferences]
public sealed partial class DropshipInnerButton : Button
{
    public static readonly Color DefaultHoveredColor = Color.FromHex("#00FF54");
    public static readonly Color DefaultUnhoveredColor = Color.FromHex("#02E24C");
    public static readonly Color DefaultTextColor = Color.Black;
    public static readonly Color DefaultDisabledColor = Color.FromHex("#004C24");
    public static readonly Color DefaultDisabledTextColor = Color.FromHex("#0BDC49");

    private bool _initialized;
    private string? _text;

    public Color HoveredColor { get; set; } = DefaultHoveredColor;

    public Color UnhoveredColor { get; set; } = DefaultUnhoveredColor;

    public Color TextColor { get; set; } = DefaultTextColor;

    public Color DisabledColor { get; set; } = DefaultDisabledColor;

    public Color DisabledTextColor { get; set; } = DefaultDisabledTextColor;

    public string? LabelText
    {
        get => _text;
        set
        {
            _text = value;
            UpdateText();
        }
    }

    public DropshipInnerButton()
    {
        RobustXamlLoader.Load(this);
        UpdateColor();
    }

    protected override void DrawModeChanged()
    {
        base.DrawModeChanged();
        UpdateColor();
    }

    private void UpdateColor()
    {
        ModulateSelfOverride = (Disabled, IsHovered) switch
        {
            (true, _) => DisabledColor,
            (_, true) => HoveredColor,
            _ => UnhoveredColor
        };

        UpdateText();
    }

    private void UpdateText()
    {
        if (NameScope != null)
        {
            var color = Disabled ? DisabledTextColor : TextColor;
            Label.SetMarkup($"[color={color.ToHex()}][bold]{LabelText ?? string.Empty}[/bold][/color]");
        }
    }

    protected override void FrameUpdate(FrameEventArgs args)
    {
        base.FrameUpdate(args);

        if (_initialized)
            return;

        _initialized = true;
        UpdateColor();
    }
}

