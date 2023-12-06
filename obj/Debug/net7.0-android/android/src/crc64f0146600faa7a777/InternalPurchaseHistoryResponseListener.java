package crc64f0146600faa7a777;


public class InternalPurchaseHistoryResponseListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.PurchaseHistoryResponseListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPurchaseHistoryResponse:(Lcom/android/billingclient/api/BillingResult;Ljava/util/List;)V:GetOnPurchaseHistoryResponse_Lcom_android_billingclient_api_BillingResult_Ljava_util_List_Handler:Android.BillingClient.Api.IPurchaseHistoryResponseListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.InternalPurchaseHistoryResponseListener, Xamarin.Android.Google.BillingClient", InternalPurchaseHistoryResponseListener.class, __md_methods);
	}


	public InternalPurchaseHistoryResponseListener ()
	{
		super ();
		if (getClass () == InternalPurchaseHistoryResponseListener.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.InternalPurchaseHistoryResponseListener, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void onPurchaseHistoryResponse (com.android.billingclient.api.BillingResult p0, java.util.List p1)
	{
		n_onPurchaseHistoryResponse (p0, p1);
	}

	private native void n_onPurchaseHistoryResponse (com.android.billingclient.api.BillingResult p0, java.util.List p1);

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
