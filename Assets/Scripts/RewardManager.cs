using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public int rewardScore = 0;
    public Text rewardText; // VR display

    public void AddReward(int amount)
    {
        rewardScore += amount;
        Debug.Log("Reward Earned! Total: " + rewardScore);

        if (rewardText != null)
        {
            rewardText.text = "Reward: " + rewardScore;
        }
    }
}
