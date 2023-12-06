package crc64f0146600faa7a777;


public class InternalProductDetailsResponseListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.ProductDetailsResponseListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onProductDetailsResponse:(Lcom/android/billingclient/api/BillingResult;Ljava/util/List;)V:GetOnProductDetailsResponse_Lcom_android_billingclient_api_BillingResult_Ljava_util_List_Handler:Android.BillingClient.Api.IProductDetailsResponseListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.InternalProductDetailsResponseListener, Xamarin.Android.Google.BillingClient", InternalProductDetailsResponseListener.class, __md_methods);
	}


	public InternalProductDetailsResponseListener ()
	{
		super ();
		if (getClass () == InternalProductDetailsResponseListener.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.InternalProductDetailsResponseListener, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void onProductDetailsResponse (com.android.billingclient.api.BillingResult p0, java.util.List p1)
	{
		n_onProductDetailsResponse (p0, p1);
	}

	private native void n_onProductDetailsResponse (com.android.billingclient.api.BillingResult p0, java.util.List p1);

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
