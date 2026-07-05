# Changelog

## 1.1.0 - 2026-07-05

### Added

- Added NuGet version and download badges to the README.
- Added NuGet package metadata, README packaging support, and repository metadata.
- Added support for `net8.0`, `net9.0`, and `net10.0` while keeping `netstandard2.0`.
- Added a GitHub Actions workflow to publish NuGet packages from tags, releases, or manual dispatch.
- Added generic resource registration with `AddLocalizedResource<TResource>()`.
- Added a binder registration overload for `AddLocalizedResource<TResource>(JLocalizationResourceBinder resourceBinder)`.

### Changed

- Upgraded package dependencies to modern versions:
  - `Microsoft.Extensions.DependencyInjection.Abstractions` 8.0.0
  - `Microsoft.Extensions.Localization.Abstractions` 8.0.0
  - `Microsoft.Extensions.Options` 8.0.0
  - `Newtonsoft.Json` 13.0.3
- Modernized sample web projects to `net10.0`.
- Improved localization lookup performance by avoiding regex, LINQ, iterator allocations, and repeated store reads on the hot path.
- Switched localization key dictionaries to ordinal string comparers.
- Cached resource names to avoid repeated attribute reflection.
- Made initial resource loading lazy and thread-safe.
- Improved culture fallback order:
  1. Exact culture.
  2. Parent cultures.
  3. Configured default culture.
  4. Key fallback when no value is found.

### Fixed

- Fixed default `IStringLocalizer` registration so the service descriptor is actually added.
- Fixed `Reset()` so it reloads JLocalizer resources and fails clearly for non-JLocalizer instances.
- Fixed resource stream disposal after embedded JSON resource loading.
- Fixed duplicate-key handling when building localization stores.
- Fixed sample projects to remove unsupported ASP.NET Core 2.1 references.

### Published

- NuGet package: `JLocalizer` `1.1.0`
- Package URL: https://www.nuget.org/packages/JLocalizer/1.1.0
