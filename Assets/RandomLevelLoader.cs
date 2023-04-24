using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelLoader : MonoBehaviour
{
    public List<LevelObject> levels = new List<LevelObject>();

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, levels.Count);

        gameObject.GetComponent<SpriteRenderer>().sprite = levels[rand].background;
    }
}

[System.Serializable]
public class LevelObject
{
    public string name;
    public Sprite background;
}
