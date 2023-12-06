package mono.com.google.android.play.core.install;


public class InstallStateUpdatedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.play.core.install.InstallStateUpdatedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onStateUpdate:(Lcom/google/android/play/core/install/InstallState;)V:GetOnStateUpdate_Lcom_google_android_play_core_install_InstallState_Handler:Xamarin.Google.Android.Play.Core.Install.IInstallStateUpdatedListenerInvoker, Xamarin.Google.Android.Play.Core\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Google.Android.Play.Core.Install.IInstallStateUpdatedListenerImplementor, Xamarin.Google.Android.Play.Core", InstallStateUpdatedListenerImplementor.class, __md_methods);
	}


	public InstallStateUpdatedListenerImplementor ()
	{
		super ();
		if (getClass () == InstallStateUpdatedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Xamarin.Google.Android.Play.Core.Install.IInstallStateUpdatedListenerImplementor, Xamarin.Google.Android.Play.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onStateUpdate (com.google.android.play.core.install.InstallState p0)
	{
		n_onStateUpdate (p0);
	}

	private native void n_onStateUpdate (com.google.android.play.core.install.InstallState p0);

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
