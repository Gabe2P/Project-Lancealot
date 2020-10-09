using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private List<Enemy> enemies = new List<Enemy>();

    private Player player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

        // Start is called before the first frame update
    public void Start()
    {

        player = FindObjectOfType<Player>();

        AudioManager.instance.Play("Forest");

        foreach (Enemy e in enemies)
        {
            e.SetTarget(player.gameObject);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
