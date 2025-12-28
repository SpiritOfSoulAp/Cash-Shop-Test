package controller

import (
	"cash_shop/db"
	"cash_shop/models"
	"cash_shop/response"
	"fmt"

	"github.com/gin-gonic/gin"
)

func GetItems(ctx *gin.Context) {

	var items []models.ShopItem

	// preload durations item
	if err := db.DB.
		Preload("Durations").
		Find(&items).Error; err != nil {

		response.Error(ctx, 500, "INTERNAL_ERROR", "Cannot load shop items")
		return
	}

	// Convert to a response according to the Canonical API
	result := make([]gin.H, 0)

	for _, item := range items {

		durations := make([]gin.H, 0)
		for _, d := range item.Durations {
			durations = append(durations, gin.H{
				"key":     d.DurationKey,
				"seconds": d.Seconds,
				"price":   d.Price,
			})
		}

		result = append(result, gin.H{
			"id":         item.ID,
			"code":       item.Code,
			"name":       item.Name,
			"type":       item.Type,
			"base_price": item.BasePrice,
			"durations":  durations,
		})
	}
	fmt.Println(result)

	response.OK(ctx, result)
}
