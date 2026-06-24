using UnityEngine;
using UnityEngine.UI;

namespace TinyGarden.Rewards
{
    public class StickerBookController : MonoBehaviour
    {
        [SerializeField] private GameObject stickerBookPanel;
        [SerializeField] private Image starStickerImage;

        private void Start()
        {
            if (stickerBookPanel != null)
                stickerBookPanel.SetActive(false);
        }

        public void OpenStickerBook()
        {
            if (stickerBookPanel != null)
            {
                stickerBookPanel.SetActive(true);
                UpdateStickers();
            }
        }

        public void CloseStickerBook()
        {
            if (stickerBookPanel != null)
            {
                stickerBookPanel.SetActive(false);
            }
        }

        private void UpdateStickers()
        {
            bool hasStar = RewardSystem.Instance.HasReward("sticker_garden_helper_star");
            
            if (starStickerImage != null)
            {
                // Silhouette if locked, full color if unlocked
                starStickerImage.color = hasStar ? Color.white : new Color(0, 0, 0, 0.2f);
            }
        }
    }
}
