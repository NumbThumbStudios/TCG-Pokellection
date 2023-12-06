package crc64f0146600faa7a777;


public class InternalPriceChangeConfirmationListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Android.BillingClient.Api.InternalPriceChangeConfirmationListener, Xamarin.Android.Google.BillingClient", InternalPriceChangeConfirmationListener.class, __md_methods);
	}


	public InternalPriceChangeConfirmationListener ()
	{
		super ();
		if (getClass () == InternalPriceChangeConfirmationListener.class) {
			mono.android.TypeManager.Activate ("Android.BillingClient.Api.InternalPriceChangeConfirmationListener, Xamarin.Android.Google.BillingClient", "", this, new java.lang.Object[] {  });
		}
	}

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
