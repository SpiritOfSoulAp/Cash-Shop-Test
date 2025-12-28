package controller

import (
	"cash_shop/db"
	"cash_shop/models"
	"cash_shop/response"
	"time"

	"github.com/gin-gonic/gin"
)

func GetInventory(ctx *gin.Context) {

	// user_id from query string
	userID := ctx.Query("user_id")
	if userID == "" {
		response.Error(ctx, 400, "INVALID_REQUEST", "user_id is required")
		return
	}

	var inventory []models.InventoryItem

	// preload user + shop item
	if err := db.DB.
		Preload("ShopItem").
		Preload("ShopItem.Durations").
		Where("user_id = ?", userID).
		Find(&inventory).Error; err != nil {

		response.Error(ctx, 500, "INTERNAL_ERROR", "Cannot load inventory")
		return
	}

	result := make([]gin.H, 0)

	for _, item := range inventory {

		if item.ExpireAt != nil && item.ExpireAt.Before(time.Now()) {
			continue
		}

		result = append(result, gin.H{
			"inventory_id": item.ID,
			"item_id":      item.ShopItemID,
			"item_name":    item.ShopItem.Name,
			"expire_at":    item.ExpireAt,
		})

	}

	response.OK(ctx, result)
}
