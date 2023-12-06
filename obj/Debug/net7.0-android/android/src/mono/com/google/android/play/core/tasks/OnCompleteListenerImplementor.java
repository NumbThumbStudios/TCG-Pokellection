package mono.com.google.android.play.core.tasks;


public class OnCompleteListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.play.core.tasks.OnCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onComplete:(Lcom/google/android/play/core/tasks/Task;)V:GetOnComplete_Lcom_google_android_play_core_tasks_Task_Handler:Xamarin.Google.Android.Play.Core.Tasks.IOnCompleteListenerInvoker, Xamarin.Google.Android.Play.Core\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Google.Android.Play.Core.Tasks.IOnCompleteListenerImplementor, Xamarin.Google.Android.Play.Core", OnCompleteListenerImplementor.class, __md_methods);
	}


	public OnCompleteListenerImplementor ()
	{
		super ();
		if (getClass () == OnCompleteListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Xamarin.Google.Android.Play.Core.Tasks.IOnCompleteListenerImplementor, Xamarin.Google.Android.Play.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onComplete (com.google.android.play.core.tasks.Task p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (com.google.android.play.core.tasks.Task p0);

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
