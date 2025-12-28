package controller

import (
	"cash_shop/db"
	"cash_shop/models"
	"cash_shop/response"
	"cash_shop/service"

	"github.com/gin-gonic/gin"
)

func Buy(ctx *gin.Context) {

	var req struct {
		UserID      uint64 `json:"user_id"`
		ItemID      uint64 `json:"item_id"`
		DurationKey string `json:"duration_key"`
		RequestID   string `json:"request_id"`
	}

	if err := ctx.ShouldBindJSON(&req); err != nil {
		response.Error(ctx, 400, "INVALID_REQUEST", "Invalid request body")
		return
	}

	result, err := service.Buy(service.BuyInput{
		UserID:      req.UserID,
		ItemID:      req.ItemID,
		DurationKey: req.DurationKey,
		RequestID:   req.RequestID,
	})

	// Check duplicate request
	var existing models.PurchaseLog
	if err := db.DB.
		Where("request_id = ?", req.RequestID).
		First(&existing).Error; err == nil {

		// duplicate request_id -> return old success 
		response.Success(ctx, "DUPLICATE_REQUEST", "Duplicate request", gin.H{
			"cash_before":  existing.CashBefore,
			"cash_after":   existing.CashAfter,
			"inventory_id": existing.ID,
			"expire_at":    nil,
		})
		return
	}

	if err != nil {
		switch err.Error() {
		case "USER_NOT_FOUND":
			response.Error(ctx, 404, "USER_NOT_FOUND", "User not found")
		case "ITEM_NOT_FOUND":
			response.Error(ctx, 404, "ITEM_NOT_FOUND", "Item not found")
		case "DURATION_NOT_FOUND":
			response.Error(ctx, 404, "DURATION_NOT_FOUND", "Duration not found")
		case "INSUFFICIENT_CASH":
			response.Error(ctx, 400, "INSUFFICIENT_CASH", "Not enough cash")
		default:
			response.Error(ctx, 500, "INTERNAL_ERROR", "Purchase failed")
		}
		return
	}

	response.Success(ctx, "PURCHASE_SUCCESS", "Purchase completed", result)
}
