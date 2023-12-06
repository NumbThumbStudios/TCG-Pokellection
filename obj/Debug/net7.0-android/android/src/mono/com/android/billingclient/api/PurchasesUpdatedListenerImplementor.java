package mono.com.android.billingclient.api;


public class PurchasesUpdatedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.android.billingclient.api.PurchasesUpdatedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPurchasesUpdated:(Lcom/android/billingclient/api/BillingResult;Ljava/util/List;)V:GetOnPurchasesUpdated_Lcom_android_billingclient_api_BillingResult_Ljava_util_List_Handler:Android.BillingClient.Api.IPurchasesUpdatedListenerInvoker, Xamarin.Android.Google.BillingClient\n" +
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.IPurchasesUpdatedListenerImplementor, Xamarin.Android.Google.BillingClient", PurchasesUpdatedListenerImplementor.class, __md_methods);
	}


	public PurchasesUpdatedListenerImplementor ()
	{
		super ();
		if (getClass () == PurchasesUpdatedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.IPurchasesUpdatedListenerImplementor, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}


	public void onPurchasesUpdated (com.android.billingclient.api.BillingResult p0, java.util.List p1)
	{
		n_onPurchasesUpdated (p0, p1);
	}

	private native void n_onPurchasesUpdated (com.android.billingclient.api.BillingResult p0, java.util.List p1);

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
