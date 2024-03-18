using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Text playerScoreText;
    [SerializeField]
    private Transform lifeIcons;
    [SerializeField]
    private Transform bombIcons;

    public int smartBombs;

    private Transform[] lifeIconsTransforms;
    private Transform[] bombIconsTransforms;
    private int playerScore;
    private int playerLives;
    private int liveBombsAwarded;

    void Start ()
    {
        liveBombsAwarded = 1;
        playerScoreText.text = playerScore.ToString();
        smartBombs = 3;
        playerLives = 3;
        lifeIconsTransforms = new Transform[lifeIcons.childCount];
        for (int i = 0; i < lifeIcons.childCount; i++)
        {
            lifeIconsTransforms[i] = lifeIcons.GetChild(i);
        }
        bombIconsTransforms = new Transform[bombIcons.childCount];
        for (int i = 0; i < bombIcons.childCount; i++)
        {
            bombIconsTransforms[i] = bombIcons.GetChild(i);
        }
    }

    public void addScore(int score)
    {
        playerScore += score;
        playerScoreText.text = playerScore.ToString();
        if(playerScore/1000 >= liveBombsAwarded)
        {
            addBomb(1);
            addLife(1);
            liveBombsAwarded += 1;
        }
    }

    public void addBomb(int bomb)
    {
        smartBombs += bomb;
        if (smartBombs < 0)
        {
            smartBombs = 0;
        }
        else if (smartBombs > 3)
        {
            smartBombs = 3;
        }
        if (bomb < 0)
        {
            var tempColor = bombIconsTransforms[smartBombs].GetComponent<Image>().color;
            tempColor.a = 0f;
            bombIconsTransforms[smartBombs].GetComponent<Image>().color = tempColor;
        }
        else
        {
            var tempColor = bombIconsTransforms[smartBombs - 1].GetComponent<Image>().color;
            tempColor.a = 1f;
            bombIconsTransforms[smartBombs - 1].GetComponent<Image>().color = tempColor;
        }
    }

    public void addLife(int life)
    {
        playerLives += life;
        if(playerLives < 0)
        {
            playerLives = 0;
        }
        else if(playerLives > 5)
        {
            playerLives = 5;
        }
        if(life < 0)
        {
            var tempColor = lifeIconsTransforms[playerLives].GetComponent<Image>().color;
            tempColor.a = 0f;
            lifeIconsTransforms[playerLives].GetComponent<Image>().color = tempColor;
        }
        else
        {
            var tempColor = lifeIconsTransforms[playerLives-1].GetComponent<Image>().color;
            tempColor.a = 1f;
            lifeIconsTransforms[playerLives-1].GetComponent<Image>().color = tempColor;
        }
    }

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            addLife(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            addBomb(1);
        }
    }
}
