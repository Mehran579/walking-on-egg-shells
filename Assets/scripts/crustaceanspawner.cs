using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class crustaceanspawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int no_ofpillbugs;
    public static int no_ofturtles;
    public static int no_ofhermitcrabs;
    public static int no_ofpistolsrhimps;
    public static int no_ofsnail;
    public TMP_Text pillbugs;
    public TMP_Text turtels;
    public TMP_Text hermitcrabs;
    public TMP_Text pistolshrimps;
    public TMP_Text snails;
    public TMP_Text shopbuttontext;
    public GameObject notenoughtpoints;
    public GameObject shop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(no_ofpillbugs);
        pillbugs.text = "Pillbugs: " + no_ofpillbugs;
        turtels.text = "Turtles: " + no_ofturtles;
        hermitcrabs.text = "King Crabs: " + no_ofhermitcrabs;
        snails.text = "Cone Snails: " + no_ofsnail;
        pistolshrimps.text = "Pistol Shrimps: " + no_ofpistolsrhimps;
    }
    public void loweramount(int index)
    {
        switch (index)
        {
            case 0:
                no_ofpillbugs--;
                break;
            case 1:
                no_ofturtles--;
                break;
            case 2:
                no_ofsnail--;
                break;
            case 3:
                no_ofpistolsrhimps--;
                break;
            case 4:
                no_ofhermitcrabs--;
                break;
        }
    }
    public int checkavailablity(int index)
    {
        switch (index)
        {
            case 0:
                return no_ofpillbugs;
            case 1:
                return no_ofturtles;
            case 2:
                return no_ofsnail;
            case 3:
                return no_ofpistolsrhimps;
            case 4:
                return no_ofhermitcrabs;
            default:
                return 0;
        }
    }
    public void OnShop()
    {
        if (shop.activeSelf)
        {
            shopbuttontext.text = "shop";
            shop.SetActive(false);
        }
        else
        {
            shopbuttontext.text = "back";
            shop.SetActive(true);
        }
    }
    public void Boughtpillbug()
    {
        if (gridmanager.points >= 1)
        {
            no_ofpillbugs += 3;
            gridmanager.points -= 1;
        }
        else
        {
            StartCoroutine(nopointroutine());
        }
        //no_ofpillbugs += 5;
        //gridmanager.points -= 1;
    }
    public void boughtSnail()
    {
        if (gridmanager.points >= 3)
        {
            no_ofsnail += 1;
            gridmanager.points -= 3;
        }
        else
        {
            StartCoroutine(nopointroutine());
        }
        //no_ofpillbugs += 1;
        //gridmanager.points -= 1;
    }
    public void boughtpistolshrimp()
    {
        if (gridmanager.points >= 5)
        {
            no_ofpistolsrhimps += 1;
            gridmanager.points -= 5;
        }
        else
        {
            StartCoroutine(nopointroutine());
        }
        //no_ofpillbugs += 1;
        //gridmanager.points -= 5;
    }
    public void boughtturtles()
    {
        //no_ofpillbugs += 1;
        //gridmanager.points -= 5;
        if (gridmanager.points >= 5)
        {
            no_ofturtles += 1;
            gridmanager.points -= 5;
        }
        else
        {
            StartCoroutine(nopointroutine());
        }
    }
    public void Boughthermitcrab()
    {
        if (gridmanager.points >= 3)
        {
            no_ofhermitcrabs += 1;
            gridmanager.points -= 3;
        }
        else
        {
            StartCoroutine(nopointroutine());
        }
    }
    public IEnumerator nopointroutine()
    {
        notenoughtpoints.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        notenoughtpoints.SetActive(false);
    }
}
