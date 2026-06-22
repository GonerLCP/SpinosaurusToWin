using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite BaseSpinoWoSkate;
    public Sprite JumpWindup;
    public Sprite DuringJump;
    public Sprite OllieWindup;
    public Sprite DuringOllie;
    public Sprite Dead;
    public Sprite Scream;
    public Sprite Speeding;

    public Sprite BaseSkate;
    public Sprite SkateOllieWindup;

    public GameObject Player;
    SpriteRenderer PlayerSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerSprite = Player.GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.BaseSpinoWoSkate:
                PlayerSprite.sprite = BaseSpinoWoSkate;
                break;
            case PlayerState.JumpWindup:
                PlayerSprite.sprite = JumpWindup;
                break;
            case PlayerState.DuringJump:
                PlayerSprite.sprite =DuringJump;
                break;
            case PlayerState.OllieWindup:
                PlayerSprite.sprite = OllieWindup;
                break;
            case PlayerState.DuringOllie:
                PlayerSprite.sprite = DuringOllie;
                break;
            case PlayerState.Dead:
                PlayerSprite.sprite =Dead;
                break;
            case PlayerState.Speeding:
                PlayerSprite.sprite =Speeding;
                break;
            case PlayerState.Screaming:
                PlayerSprite.sprite = Scream;
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
