# JLocalizer

[![NuGet](https://img.shields.io/nuget/v/JLocalizer.svg)](https://www.nuget.org/packages/JLocalizer)
[![NuGet Downloads](https://img.shields.io/nuget/dt/JLocalizer.svg)](https://www.nuget.org/packages/JLocalizer)

JLocalizer is a JSON-backed localization package for `Microsoft.Extensions.Localization`.
It lets ASP.NET Core and worker-service applications use embedded JSON resources through the familiar `IStringLocalizer` and `IStringLocalizer<T>` APIs.

## Supported frameworks

- `netstandard2.0`
- `.NET 8`
- `.NET 9`
- `.NET 10`

## Install

```bash
dotnet add package JLocalizer
```

## Quick start

Create a marker resource type:

```csharp
using JLocalizer;

namespace MyApp.Localization;

[JLocalizer("Localization")]
public sealed class AppResources
{
}
```

Add embedded JSON resources to your project:

```xml
<ItemGroup>
  <EmbeddedResource Include="Localization\en-US.json" />
  <EmbeddedResource Include="Localization\es-ES.json" />
</ItemGroup>
```

Register JLocalizer:

```csharp
using JLocalizer;

builder.Services.AddJLocalizer(localizer =>
{
    localizer.DefaultCulture("en-US");
    localizer.AddLocalizedResource<AppResources>();
});
```

Inject and use the localizer:

```csharp
public sealed class GreetingService
{
    private readonly IStringLocalizer<AppResources> _localizer;

    public GreetingService(IStringLocalizer<AppResources> localizer)
    {
        _localizer = localizer;
    }

    public string GetGreeting(string name)
    {
        return _localizer["HelloUser", name];
    }
}
```

Example `en-US.json`:

```json
{
  "HelloUser": "Hello, {0}!"
}
```

## Resource fallback

JLocalizer resolves strings in this order:

1. Current UI culture, for example `en-IN`.
2. Parent cultures, for example `en`.
3. Configured default culture.
4. The key itself when no value is found.

## Reload resources

Use `Reset()` when a custom binder reads from a mutable source and the localization store should be rebuilt:

```csharp
localizer.Reset();
```
