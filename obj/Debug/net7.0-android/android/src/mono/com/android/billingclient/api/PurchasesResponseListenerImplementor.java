package mono.com.android.billingclient.api;


public class PurchasesResponseListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.PurchasesResponseListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onQueryPurchasesResponse:(Lcom/android/billingclient/api/BillingResult;Ljava/util/List;)V:GetOnQueryPurchasesResponse_Lcom_android_billingclient_api_BillingResult_Ljava_util_List_Handler:Android.BillingClient.Api.IPurchasesResponseListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.IPurchasesResponseListenerImplementor, Xamarin.Android.Google.BillingClient", PurchasesResponseListenerImplementor.class, __md_methods);
	}


	public PurchasesResponseListenerImplementor ()
	{
		super ();
		if (getClass () == PurchasesResponseListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.IPurchasesResponseListenerImplementor, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void onQueryPurchasesResponse (com.android.billingclient.api.BillingResult p0, java.util.List p1)
	{
		n_onQueryPurchasesResponse (p0, p1);
	}

	private native void n_onQueryPurchasesResponse (com.android.billingclient.api.BillingResult p0, java.util.List p1);

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
