package mono.xamarin.google.android.play.core.assetpacks;


public class AssetPackStateUpdateListenerWrapper_AssetPackStateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		xamarin.google.android.play.core.assetpacks.AssetPackStateUpdateListenerWrapper.AssetPackStateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_OnStateUpdate:(Lcom/google/android/play/core/assetpacks/AssetPackState;)V:GetOnStateUpdate_Lcom_google_android_play_core_assetpacks_AssetPackState_Handler:Xamarin.Google.Android.Play.Core.AssetPacks.AssetPackStateUpdateListenerWrapper/IAssetPackStateListenerInvoker, Xamarin.Google.Android.Play.Core\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Google.Android.Play.Core.AssetPacks.AssetPackStateUpdateListenerWrapper+IAssetPackStateListenerImplementor, Xamarin.Google.Android.Play.Core", AssetPackStateUpdateListenerWrapper_AssetPackStateListenerImplementor.class, __md_methods);
	}


	public AssetPackStateUpdateListenerWrapper_AssetPackStateListenerImplementor ()
	{
		super ();
		if (getClass () == AssetPackStateUpdateListenerWrapper_AssetPackStateListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Xamarin.Google.Android.Play.Core.AssetPacks.AssetPackStateUpdateListenerWrapper+IAssetPackStateListenerImplementor, Xamarin.Google.Android.Play.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void OnStateUpdate (com.google.android.play.core.assetpacks.AssetPackState p0)
	{
		n_OnStateUpdate (p0);
	}

	private native void n_OnStateUpdate (com.google.android.play.core.assetpacks.AssetPackState p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
