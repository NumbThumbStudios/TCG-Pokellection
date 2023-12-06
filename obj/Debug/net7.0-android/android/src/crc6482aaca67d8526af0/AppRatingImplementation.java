package crc6482aaca67d8526af0;


public class AppRatingImplementation
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
		mono.android.Runtime.register ("Plugin.Maui.AppRating.AppRatingImplementation, Plugin.Maui.AppRating", AppRatingImplementation.class, __md_methods);
	}


	public AppRatingImplementation ()
	{
		super ();
		if (getClass () == AppRatingImplementation.class) {
			mono.android.TypeManager.Activate ("Plugin.Maui.AppRating.AppRatingImplementation, Plugin.Maui.AppRating", "", this, new java.lang.Object[] {  });
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
