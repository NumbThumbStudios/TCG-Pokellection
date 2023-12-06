package crc6424817b6e85a95e78;


public class MauiMTAdmob
	extends com.google.android.gms.ads.interstitial.InterstitialAdLoadCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Plugin.MauiMTAdmob.MauiMTAdmob, Plugin.MauiMtAdmob", MauiMTAdmob.class, __md_methods);
	}


	public MauiMTAdmob ()
	{
		super ();
		if (getClass () == MauiMTAdmob.class) {
			mono.android.TypeManager.Activate ("Plugin.MauiMTAdmob.MauiMTAdmob, Plugin.MauiMtAdmob", "", this, new java.lang.Object[] {  });
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
