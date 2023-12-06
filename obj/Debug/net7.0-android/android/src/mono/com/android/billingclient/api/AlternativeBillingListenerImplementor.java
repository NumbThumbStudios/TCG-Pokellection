package mono.com.android.billingclient.api;


public class AlternativeBillingListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.AlternativeBillingListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_userSelectedAlternativeBilling:(Lcom/android/billingclient/api/AlternativeChoiceDetails;)V:GetUserSelectedAlternativeBilling_Lcom_android_billingclient_api_AlternativeChoiceDetails_Handler:Android.BillingClient.Api.IAlternativeBillingListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.IAlternativeBillingListenerImplementor, Xamarin.Android.Google.BillingClient", AlternativeBillingListenerImplementor.class, __md_methods);
	}


	public AlternativeBillingListenerImplementor ()
	{
		super ();
		if (getClass () == AlternativeBillingListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.IAlternativeBillingListenerImplementor, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void userSelectedAlternativeBilling (com.android.billingclient.api.AlternativeChoiceDetails p0)
	{
		n_userSelectedAlternativeBilling (p0);
	}

	private native void n_userSelectedAlternativeBilling (com.android.billingclient.api.AlternativeChoiceDetails p0);

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
