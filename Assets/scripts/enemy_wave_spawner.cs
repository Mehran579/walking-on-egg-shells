using System.Collections;
using TMPro;
//using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class enemy_wave_spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int wavenumber;
    public GameObject enemy;
    public float maxtime;
    public float mintime;
    public LayerMask shell;
    public Transform min;
    public Transform max;
    public TMP_Text currentwave;
    public TMP_Text points_text;
    void Start()
    {
        wavenumber = 1;
        StartCoroutine(Wave());
    }
    public void Update()
    {
        currentwave.text = "Current Wave: " + wavenumber;
        points_text.text = "Point: " + gridmanager.points;
    }
    IEnumerator Wave()
    {
        for (int i = 0; i < EnemyCount(wavenumber); i++)
        {
            Vector2 spawnpos = new Vector2(Random.Range(min.position.x, max.position.x), Random.Range(min.position.y, max.position.y));
            if( !((spawnpos.x < 255 && spawnpos.x > 172) && (spawnpos.y < 251 && spawnpos.y > 203)))
            {
                if (Physics2D.OverlapCircle(spawnpos, 2f, shell) == null) 
                { 
                    Instantiate(enemy, spawnpos, Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(mintime, maxtime));
                    // empty → spawn
                }
                else
                {
                    i--;
                }
            }
        }
        yield return new WaitForSeconds(20);
        StartCoroutine(Wave());
        wavenumber++;
    }
    float enemynumber(int wave)
    {
        return Mathf.RoundToInt(4 + 0.05f * wave * wave * wave + 0.95f * wave);
    }
    int EnemyCount(int wave) => 4 + Mathf.RoundToInt(0.8f * wave + 0.15f * wave * wave);
}
