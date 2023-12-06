package mono.com.android.billingclient.api;


public class AcknowledgePurchaseResponseListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.AcknowledgePurchaseResponseListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAcknowledgePurchaseResponse:(Lcom/android/billingclient/api/BillingResult;)V:GetOnAcknowledgePurchaseResponse_Lcom_android_billingclient_api_BillingResult_Handler:Android.BillingClient.Api.IAcknowledgePurchaseResponseListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.IAcknowledgePurchaseResponseListenerImplementor, Xamarin.Android.Google.BillingClient", AcknowledgePurchaseResponseListenerImplementor.class, __md_methods);
	}


	public AcknowledgePurchaseResponseListenerImplementor ()
	{
		super ();
		if (getClass () == AcknowledgePurchaseResponseListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.IAcknowledgePurchaseResponseListenerImplementor, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void onAcknowledgePurchaseResponse (com.android.billingclient.api.BillingResult p0)
	{
		n_onAcknowledgePurchaseResponse (p0);
	}

	private native void n_onAcknowledgePurchaseResponse (com.android.billingclient.api.BillingResult p0);

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
