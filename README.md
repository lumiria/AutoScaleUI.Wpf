# AutoScaleUI.Wpf
This provides a function to automatically adjust the scale of the UI.

## Install
~~~
PM > Install-Package AutoScaleUI.Wpf
~~~

## Brief Introduction
Framework elements with AutoScaleBehavior will now maintain their relative size as the target parent element is resized.

![screenshot-01](https://github.com/lumiria/AutoScaleUI.Wpf/blob/images/AutoScaleUI-screenshot-01.png)

To use it, simply specify the behavior for the emement you want to apply.
```csharp
<Grid>
    <local:ChildContent />

    <i:Interaction.Behaviors>
        <wpf:AutoScaleBehavior MaintainsAspectRaito="true" TargetParent="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type Window}}}" />
    </i:Interaction.Behaviors>
</Grid>
```

## Lisence
This library is under the MIT License.

