using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReload : MonoBehaviour
{
    public int MagazineSize = 30;
    public int TotalAmmo;
    public float ReloadTime = 2f;
    public Text Ammo;
    public Text TotalAmmoTxt;
    public int currentAmmoInMagazine;
    public bool isReloading;

    void Start()
    {
    	currentAmmoInMagazine = MagazineSize;
    	Ammo.text = $"{currentAmmoInMagazine}";
    	TotalAmmoTxt.text = $"{TotalAmmo}";
    }

    public void Reload()
    {
        if (!isReloading && currentAmmoInMagazine < MagazineSize)
        {
        	StartCoroutine(ReloadWeapon());
        }
    }

    public void ReloadText()
    {
    	Ammo.text = $"{currentAmmoInMagazine}";
    	TotalAmmoTxt.text = $"{TotalAmmo}";
    }

    IEnumerator ReloadWeapon()
    {
    	isReloading = true;
    	yield return new WaitForSeconds(ReloadTime);
    	int ammoToLoad = Mathf.Min(TotalAmmo, MagazineSize - currentAmmoInMagazine);
    	TotalAmmo -= ammoToLoad;
    	currentAmmoInMagazine += ammoToLoad;
    	isReloading = false;
    	ReloadText();
    }
}
