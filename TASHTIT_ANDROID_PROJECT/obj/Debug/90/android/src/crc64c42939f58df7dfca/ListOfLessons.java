package crc64c42939f58df7dfca;


public class ListOfLessons
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("TASHTIT_ANDROID_PROJECT.ACTIVITIES.ListOfLessons, TASHTIT_ANDROID_PROJECT", ListOfLessons.class, __md_methods);
	}


	public ListOfLessons ()
	{
		super ();
		if (getClass () == ListOfLessons.class)
			mono.android.TypeManager.Activate ("TASHTIT_ANDROID_PROJECT.ACTIVITIES.ListOfLessons, TASHTIT_ANDROID_PROJECT", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
