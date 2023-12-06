package mono.xamarin.google.android.play.core.splitInstall;


public class SplitInstallStateUpdatedListenerWrapper_StateUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		xamarin.google.android.play.core.splitInstall.SplitInstallStateUpdatedListenerWrapper.StateUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_OnStateUpdate:(Lcom/google/android/play/core/splitinstall/SplitInstallSessionState;)V:GetOnStateUpdate_Lcom_google_android_play_core_splitinstall_SplitInstallSessionState_Handler:Xamarin.Google.Android.Play.Core.SplitInstall.SplitInstallStateUpdatedListenerWrapper/IStateUpdateListenerInvoker, Xamarin.Google.Android.Play.Core\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Google.Android.Play.Core.SplitInstall.SplitInstallStateUpdatedListenerWrapper+IStateUpdateListenerImplementor, Xamarin.Google.Android.Play.Core", SplitInstallStateUpdatedListenerWrapper_StateUpdateListenerImplementor.class, __md_methods);
	}


	public SplitInstallStateUpdatedListenerWrapper_StateUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == SplitInstallStateUpdatedListenerWrapper_StateUpdateListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Xamarin.Google.Android.Play.Core.SplitInstall.SplitInstallStateUpdatedListenerWrapper+IStateUpdateListenerImplementor, Xamarin.Google.Android.Play.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void OnStateUpdate (com.google.android.play.core.splitinstall.SplitInstallSessionState p0)
	{
		n_OnStateUpdate (p0);
	}

	private native void n_OnStateUpdate (com.google.android.play.core.splitinstall.SplitInstallSessionState p0);

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
