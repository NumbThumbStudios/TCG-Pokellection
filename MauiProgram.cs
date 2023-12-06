using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;
using Plugin.Maui.AppRating;

namespace TCGPokellection;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{ 
        // OVERRIDE: ENTRY
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) => 
		{
			#if ANDROID
			handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
			#endif
		});

        // OVERRIDE: PICKER
        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
        {
			#if ANDROID
			handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
			#endif
        });

        var builder = MauiApp.CreateBuilder();

		builder.Services.AddSingleton<IAppRating>(AppRating.Default);

        builder.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiMTAdmob()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
