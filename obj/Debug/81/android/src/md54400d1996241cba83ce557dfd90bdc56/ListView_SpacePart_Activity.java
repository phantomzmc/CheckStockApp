package md54400d1996241cba83ce557dfd90bdc56;


public class ListView_SpacePart_Activity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CheckStockApp.ListView_SpacePart_Activity, CheckStockApp", ListView_SpacePart_Activity.class, __md_methods);
	}


	public ListView_SpacePart_Activity ()
	{
		super ();
		if (getClass () == ListView_SpacePart_Activity.class)
			mono.android.TypeManager.Activate ("CheckStockApp.ListView_SpacePart_Activity, CheckStockApp", "", this, new java.lang.Object[] {  });
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
