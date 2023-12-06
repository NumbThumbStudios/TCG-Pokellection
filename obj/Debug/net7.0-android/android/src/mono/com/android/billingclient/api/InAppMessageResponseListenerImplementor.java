package mono.com.android.billingclient.api;


public class InAppMessageResponseListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.InAppMessageResponseListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInAppMessageResponse:(Lcom/android/billingclient/api/InAppMessageResult;)V:GetOnInAppMessageResponse_Lcom_android_billingclient_api_InAppMessageResult_Handler:Android.BillingClient.Api.IInAppMessageResponseListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.IInAppMessageResponseListenerImplementor, Xamarin.Android.Google.BillingClient", InAppMessageResponseListenerImplementor.class, __md_methods);
	}


	public InAppMessageResponseListenerImplementor ()
	{
		super ();
		if (getClass () == InAppMessageResponseListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.IInAppMessageResponseListenerImplementor, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void onInAppMessageResponse (com.android.billingclient.api.InAppMessageResult p0)
	{
		n_onInAppMessageResponse (p0);
	}

	private native void n_onInAppMessageResponse (com.android.billingclient.api.InAppMessageResult p0);

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
